using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Users.Contracts;
using Group = Uni.Backend.Modules.Groups.Contracts.Group;

namespace Uni.Backend.Modules.Courses.Endpoints;

public class UpdateCourse : Endpoint<UpdateCourseRequest, CourseDto, CoursesMapper>
{
    private readonly AppDbContext _db;

    public UpdateCourse(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Version(1);
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
        Put("/courses/{id}");
        Options(x => x.WithTags("Courses"));
        Description(b => b
            .Produces(200)
            .ProducesProblemFE(401)
            .ProducesProblemFE(403)
            .ProducesProblemFE(404)
            .ProducesProblemFE(500));
        Summary(x =>
        {
            x.Summary = "Edits course";
            x.Description = """
                               <b>Allowed scopes:</b> Any Administrator, Tutor who ownes the course
                            """;
            x.Responses[200] = "User updated successfully";
            x.Responses[401] = "Not authorized";
            x.Responses[403] = "Forbidden";
            x.Responses[404] = "User was not found";
            x.Responses[500] = "Some other error occured";
        });
    }

    public override async Task HandleAsync(UpdateCourseRequest req, CancellationToken ct)
    {
        var course = await _db.Courses
            .Where(e => e.Id == req.Id)
            .Include(e => e.Owners)
            .FirstOrDefaultAsync(ct);

        if (course is null)
        {
            ThrowError(e => e.Id, "Course was not found", 404);
        }

        var user = await _db.Users.AsNoTracking().Where(e => e.Email == User.Identity!.Name).FirstAsync(ct);

        if (User.HasClaim(ClaimTypes.Role, UserRoles.Tutor) && !course.Owners.Contains(user))
        {
            ThrowError(_ => User, "Access forbidden", 403);
        }

        course.Name = req.Name;
        course.Abbreviation = req.Abbreviation;
        course.Semester = req.Semester;

        var groups = new List<Group>();
        var users = new List<User>();
        var blocks = new List<CourseBlock>();

        foreach (var assignedGroupId in req.AssignedGroups)
        {
            var group = await _db.Groups
                .AsNoTracking()
                .Where(e => e.Id == assignedGroupId)
                .FirstOrDefaultAsync(ct);

            if (group is null)
            {
                ThrowError(e => e.AssignedGroups, "Group was not found", 404);
            }

            groups.Add(group);
        }

        foreach (var ownerId in req.Owners)
        {
            var owner = await _db.Users
                .AsNoTracking()
                .Where(e => e.Id == ownerId)
                .FirstOrDefaultAsync(ct);

            if (owner is null)
            {
                ThrowError(e => e.Owners, "User was not found", 404);
            }
            
            users.Add(owner);
        }

        foreach (var blockId in req.Blocks)
        {
            var block = await _db.CourseBlocks
                .AsNoTracking()
                .Where(e => e.Id == blockId)
                .FirstOrDefaultAsync(ct);

            if (block is null)
            {
                ThrowError(e => e.Blocks, "Block was not found", 404);
            }
            
            blocks.Add(block);
        }

        course.AssignedGroups = groups;
        course.Owners = users;
        course.Blocks = blocks;

        await _db.SaveChangesAsync(ct);
        await SendAsync(Map.FromEntity(course), cancellation: ct);
    }
}