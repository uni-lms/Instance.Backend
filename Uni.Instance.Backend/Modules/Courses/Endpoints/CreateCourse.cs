﻿using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Users.Contracts;
using Uni.Instance.Backend.Modules.Courses.Contracts;

using Group = Uni.Backend.Modules.Groups.Contracts.Group;


namespace Uni.Instance.Backend.Modules.Courses.Endpoints;

public class CreateCourse : Endpoint<CreateCourseRequest, CourseDtoV2, CoursesMapper> {
  private readonly AppDbContext _db;

  public CreateCourse(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Post("/courses");
    Roles(UserRoles.MinimumRequired(UserRoles.Tutor));
    Options(x => x.WithTags("Courses"));
    Description(b => b
      .ClearDefaultProduces()
      .Produces<CourseDtoV2>(201, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(403)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Creates new course";
      x.Description = "<b>Allowed scopes:</b> Tutor, Administrator";
      x.Responses[201] = "Course created";
      x.Responses[401] = "Not authorized";
      x.Responses[403] = "Access forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct) {
    if (User.Identity is null) {
      ThrowError(_ => User, "Not authorized", 401);
    }

    var user = await _db.Users
      .Where(e => e.Email == User.Identity.Name)
      .FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError(_ => User.Identity!.Name!, "User not found", 404);
    }

    var assignedGroups = new List<Group>();
    var enabledBlocks = await _db.CourseBlocks.ToListAsync(ct);
    var owners = new List<User> { user };

    foreach (var groupId in req.AssignedGroups) {
      var group = await _db.Groups
        .FindAsync(new object?[] { groupId }, cancellationToken: ct);

      if (group is null) {
        AddError(e => e.AssignedGroups, $"Group {groupId} was not found");
        continue;
      }

      assignedGroups.Add(group);
    }

    ThrowIfAnyErrors(404);

    var course = new Course {
      Name = req.Name,
      Abbreviation = req.Abbreviation,
      Semester = req.Semester,
      AssignedGroups = assignedGroups,
      Blocks = enabledBlocks,
      Owners = owners,
      TextContents = new List<TextContent>(),
      FileContents = new List<FileContent>(),
    };

    await _db.Courses.AddAsync(course, ct);
    await _db.SaveChangesAsync(ct);

    await SendCreatedAtAsync(
      "/v1/courses",
      null,
      Map.FromEntityToV2(course),
      cancellation: ct
    );
  }
}