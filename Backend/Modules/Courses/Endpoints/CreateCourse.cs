﻿using Backend.Configuration;
using Backend.Data;
using Backend.Modules.CourseBlocks.Contracts;
using Backend.Modules.Courses.Contract;
using Backend.Modules.Users.Contract;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Backend.Modules.Groups.Contract.Group;

namespace Backend.Modules.Courses.Endpoints;

public class CreateCourse : Endpoint<CreateCourseRequest, CourseDto, CoursesMapper>
{
    private readonly AppDbContext _db;

    public CreateCourse(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/courses");
        Options(x => x.WithTags("Courses"));
        Version(1);
        Roles(UserRoles.Tutor, UserRoles.Administrator);
    }

    public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct)
    {
        if (User.Identity is null)
        {
            ThrowError(_ => User, "Not authorized", 401);
        }

        var user = await _db.Users
            .Where(e => e.Email == User.Identity.Name)
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(_ => User.Identity.Name, "User not found", 404);
        }

        var assignedGroups = new List<Group>();
        var enabledBlocks = new List<CourseBlock>();
        var owners = new List<User> { user };

        foreach (var groupId in req.AssignedGroups)
        {
            var group = await _db.Groups
                .FindAsync(new object?[] { groupId }, cancellationToken: ct);

            if (group is null)
            {
                AddError(e => e.AssignedGroups, $"Group {groupId} was not found");
                continue;
            }

            assignedGroups.Add(group);
        }

        foreach (var blockId in req.Blocks)
        {
            var block = await _db.CourseBlocks
                .FindAsync(new object?[] { blockId }, cancellationToken: ct);

            if (block is null)
            {
                AddError(e => e.Blocks, $"Block {blockId} was not found");
                continue;
            }

            enabledBlocks.Add(block);
        }

        ThrowIfAnyErrors(404);

        var course = new Course
        {
            Name = req.Name,
            Abbreviation = req.Abbreviation,
            Semester = req.Semester,
            AssignedGroups = assignedGroups,
            Blocks = enabledBlocks,
            Owners = owners
        };

        await _db.Courses.AddAsync(course, ct);
        await _db.SaveChangesAsync(ct);

        await SendCreatedAtAsync(
            "/v1/courses",
            null,
            Map.FromEntity(course),
            cancellation: ct
        );
    }
}