﻿using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Services;
using Uni.Backend.Modules.Groups.Contract;
using Uni.Backend.Modules.Users.Contract;
using Group = Uni.Backend.Modules.Groups.Contract.Group;

namespace Uni.Backend.Modules.Groups.Endpoints;

public class CreateGroup : Endpoint<CreateGroupRequest, CreateGroupDto, GroupMapper>
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
        Version(1);
        Post("/groups");
        Roles(UserRoles.Administrator);
        Options(x => x.WithTags("Groups"));
        Description(b => b
            .Produces<CreateGroupDto>(201, MediaTypeNames.Application.Json)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(409)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Creates new group";
            x.Description = """
                               Additionally:
                               <ul>
                               <li>registers new users belonging to the group</li>
                               <li>sends letter with credentials to every created user <i>(WIP)</i></li>
                               </ul>
                               <b>Allowed scopes:</b> Administrator
                               <b>Date format:</b> yyyy-MM-dd
                            """;
            x.Responses[200] = "Group created";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Access forbidden";
            x.Responses[404] = "Gender was not found";
            x.Responses[409] = "User with this email has already been registered";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(CreateGroupRequest req, CancellationToken ct)
    {
        var users = new List<User>();
        var usersData = new List<UserCredentials>();

        var role = await _db.Roles
            .Where(e => e.Name == "Student")
            .FirstAsync(ct);

        foreach (var reqUser in req.Users)
        {

            var tryFindUser = await _db.Users.AnyAsync(e => e.Email == reqUser.Email, ct);

            if (tryFindUser)
            {
                AddError(e => e.Users, $"User with email {reqUser.Email} already registered", "409");
                continue;
            };
            var password = _authService.GeneratePassword();
            _authService.CreatePasswordHash(password, out var passwordSalt, out var passwordHash);

            var gender = await _db.Genders.FindAsync(new object?[] { reqUser.Gender }, ct);

            if (gender is null)
            {
                ThrowError("Gender is not found", 404);
            }
            
            users.Add(new User
            {
                FirstName = reqUser.FirstName,
                LastName = reqUser.LastName,
                Patronymic = reqUser.Patronymic,
                DateOfBirth = reqUser.DateOfBirth,
                Email = reqUser.Email,
                Role = role,
                Gender = gender,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
            
            usersData.Add(new UserCredentials
            {
                Email = reqUser.Email,
                Password = password
            });
        }
        
        var tryFindGroup = await _db.Groups.AnyAsync(e => e.Name == req.Name, ct);

        if (tryFindGroup)
        {
            AddError(e => e.Name, $"Group with name {req.Name} already created");
        }
        
        ThrowIfAnyErrors();

        var group = new Group
        {
            Name = req.Name,
            CurrentSemester = req.CurrentSemester,
            MaxSemester = req.MaxSemester,
            Students = users
        };

        var result = new CreateGroupDto()
        {
            Group = Map.FromEntity(group),
            UsersData = usersData
        };

        await _db.Groups.AddAsync(group, ct);
        await _db.SaveChangesAsync(ct);
        
        // TODO: Добавить отправку писем с данными для входа (в очередь)

        await SendCreatedAtAsync(
            "/groups",
            null,
            result,
            cancellation: ct
        );
    }
}