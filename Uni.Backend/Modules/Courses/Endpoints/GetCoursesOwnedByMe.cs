﻿using System.Net.Mime;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Courses.Contract;

namespace Uni.Backend.Modules.Courses.Endpoints;

public class GetCoursesOwnedByMe : EndpointWithoutRequest<List<CourseDto>, CoursesMapper>
{
    private readonly AppDbContext _db;

    public GetCoursesOwnedByMe(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/courses/owned");
        Description(b => b
            .Produces<List<CourseDto>>(200, MediaTypeNames.Application.Json)
            .ProducesProblemFE<InternalErrorResponse>(500));
        Options(x => x.WithTags("Courses"));
        Version(1);
        Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (User.Identity is null)
        {
            ThrowError(_ => User, "Not authorized", 401);
        }

        var user = await _db.Users
            .Where(e => e.Email == User.Identity!.Name!)
            .Include(e => e.OwnedCourses!)
            .ThenInclude(e => e.AssignedGroups)
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            ThrowError(_ => User.Identity!.Name!, "User not found", 404);
        }

        var courses = user.OwnedCourses!
            .Select(e => Map.FromEntity(e))
            .ToList();

        await SendAsync(courses, cancellation: ct);
    }
}