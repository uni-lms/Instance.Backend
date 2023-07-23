using System.Net.Mime;
using Backend.Data;
using Backend.Modules.Auth.Services;
using Backend.Modules.Groups.Contract;
using Backend.Modules.Users.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Backend.Modules.Groups.Contract.Group;

namespace Backend.Modules.Groups.Endpoints;

public class CreateGroup : Endpoint<CreateGroupRequest, GroupDto, GroupMapper>
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public CreateGroup(AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/groups");
        Description(b => b
            .Produces<GroupDto>(201, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(404)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Groups"));
        Version(1);
        Roles("Administrator");
    }

    public override async Task HandleAsync(CreateGroupRequest req, CancellationToken ct)
    {
        var users = new List<User>();

        var role = await _db.Roles
            .Where(e => e.Name == "Student")
            .FirstAsync(ct);

        foreach (var reqUser in req.Users)
        {
            var password = _authService.GeneratePassword();
            _authService.CreatePasswordHash(password, out var passwordSalt, out var passwordHash);

            users.Add(new User
            {
                FirstName = reqUser.FirstName,
                LastName = reqUser.LastName,
                Patronymic = reqUser.Patronymic,
                DateOfBirth = reqUser.DateOfBirth,
                Email = reqUser.Email,
                Role = role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            // TODO: Добавить отправку писем с данными для входа (в очередь)
        }

        var group = new Group
        {
            Name = req.Name,
            CurrentSemester = req.CurrentSemester,
            MaxSemester = req.MaxSemester,
            Students = users
        };

        await _db.Groups.AddAsync(group, ct);
        await _db.SaveChangesAsync(ct);

        await SendCreatedAtAsync(
            "/groups",
            null,
            Map.FromEntity(group),
            cancellation: ct
        );
    }
}