using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Data;
using Uni.Backend.Modules.Static.Contracts;
using Uni.Backend.Modules.Static.Services;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Users.Endpoints;

public class EditUser : Endpoint<EditUserRequest, UserDto, UserMapper>
{
    private readonly AppDbContext _db;
    private readonly StaticService _staticService;


    public EditUser(AppDbContext db, StaticService staticService)
    {
        _db = db;
        _staticService = staticService;
    }

    public override void Configure()
    {
        Version(1);
        Put("/users/{id}");
        AllowFileUploads();
        Options(x => x.WithTags("Users"));
        Description(b => b
            .Produces(200)
            .ProducesProblemFE(401)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Edits user";
            x.Description = "<b>Allowed scopes:</b> Any authorized user";
            x.Responses[200] = "User updated successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[404] = "User (or related entity) was not found";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(EditUserRequest req, CancellationToken ct)
    {
        var user = await _db.Users.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(e => e.Id, "User with this id was not found", 404);
        }

        user.FirstName = req.FirstName;
        user.LastName = req.LastName;
        user.Patronymic = req.Patronymic;
        user.DateOfBirth = req.DateOfBirth;

        var newGender = await _db.Genders
            .AsNoTracking()
            .Where(e => e.Id == req.Gender)
            .FirstOrDefaultAsync(ct);

        if (newGender is null)
        {
            ThrowError(e => e.Gender, "Gender was not found", 404);
        }

        user.Gender = newGender;

        if (user.Avatar is not null)
        {
            var currentAvatar = await _db.StaticFiles
                .Where(e => e.Id == user.Avatar.Id)
                .FirstOrDefaultAsync(ct);

            File.Delete(currentAvatar!.FilePath);
            _db.StaticFiles.Remove(currentAvatar);
        }

        if (req.Avatar is not null)
        {
            var fileSaveResult = await _staticService.SaveFile(req.Avatar, ct);

            if (!fileSaveResult.IsSuccess)
            {
                ThrowError("File is empty", 422);
            }

            var checksum = await StaticService.GetChecksum(req.Avatar, ct);
            var staticFile = new StaticFile
            {
                Id = fileSaveResult.FileId!,
                Checksum = checksum,
                FileName = req.Avatar.FileName,
                FilePath = fileSaveResult.FilePath!,
                VisibleName = Path.GetFileNameWithoutExtension(fileSaveResult.FilePath!)
            };

            await _db.StaticFiles.AddAsync(staticFile, ct);
            user.Avatar = staticFile;
        }

        await _db.SaveChangesAsync(ct);

        await SendAsync(Map.FromEntity(user), cancellation: ct);
    }
}