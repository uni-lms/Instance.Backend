﻿using System.Net.Mime;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

using Uni.Backend.Data;
using Uni.Backend.Modules.Assignments.Contracts;
using Uni.Backend.Modules.AssignmentSolutions.Contracts;
using Uni.Backend.Modules.Common.Contracts;
using Uni.Backend.Modules.SolutionChecks.Contracts;


namespace Uni.Instance.Backend.Modules.Assignments.Endpoints;

public class GetAssignmentInfo : Endpoint<SearchEntityRequest, AssignmentDto> {
  private readonly AppDbContext _db;

  public GetAssignmentInfo(AppDbContext db) {
    _db = db;
  }

  public override void Configure() {
    Version(1);
    Get("/assignments/{id}");
    Options(x => x.WithTags("Assignments"));
    Description(b => b
      .Produces<Assignment>(200, MediaTypeNames.Application.Json)
      .ProducesProblemFE(401)
      .ProducesProblemFE(404)
      .ProducesProblemFE(500));
    Summary(x => {
      x.Summary = "Gets info of assignment";
      x.Description = "<b>Allowed scopes:</b> Any authorized user";
      x.Responses[201] = "Information was fetched successfully";
      x.Responses[401] = "Unauthorized";
      x.Responses[403] = "Forbidden";
      x.Responses[404] = "Some related entity was not found";
      x.Responses[500] = "Some other error occured";
    });
  }

  public override async Task HandleAsync(SearchEntityRequest req, CancellationToken ct) {
    if (User.Identity is null) {
      ThrowError(_ => User, "Not authorized", 401);
    }

    var user = await _db.Users
      .AsNoTracking()
      .Where(e => e.Email == User.Identity.Name)
      .FirstOrDefaultAsync(ct);

    if (user is null) {
      ThrowError(_ => User.Identity!.Name!, "User not found", 404);
    }

    var assignment = await _db.Assignments
      .Where(e => e.Id == req.Id)
      .FirstOrDefaultAsync(ct);

    if (assignment is null) {
      ThrowError(e => e.Id, "Assignment was not found", 404);
    }

    var solutions = await _db.AssignmentSolutions
      .Include(e => e.Author)
      .Include(e => e.Team)
      .ThenInclude(e => e!.Members)
      .Where(e => e.Assignment.Id == req.Id && ((e.Author != null && e.Author.Id == user.Id) ||
        (e.Team != null && e.Team.Members.Contains(user))))
      .Include(e => e.Checks).Include(assignmentSolution => assignmentSolution.Files)
      .ToListAsync(ct);

    var defaultStatus = SolutionCheckStatus.NotSent;
    if (assignment.AvailableUntil <= DateTime.UtcNow) {
      defaultStatus = SolutionCheckStatus.Overdue;
    }

    var rating = 0;
    var status = defaultStatus;
    if (solutions.Count > 0) {
      var amountOfChecks = 0;
      foreach (var solution in solutions) {
        amountOfChecks += solution.Checks.Count;
      }

      if (amountOfChecks > 0) {
        rating = solutions.Max(e => e.Checks.Max(sc => sc.Points));
      }

      ArgumentNullException.ThrowIfNull(solutions);
      var temp = solutions.MinBy(e => e.UpdatedAt)?.Checks.MinBy(e => e.CheckedAt)?.Status;

      if (temp is not null) {
        status = temp.GetValueOrDefault(defaultStatus);
      }
    }

    var solutionDtos = solutions.Select(e => {
        var st = "Не проверено";
        if (solutions.Count > 0) {
          var amountOfChecks = 0;
          foreach (var solution in solutions) {
            amountOfChecks += solution.Checks.Count;
          }

          if (amountOfChecks > 0) {
            rating = solutions.Max(s => s.Checks.Max(sc => sc.Points));
            st = $"{rating} / {assignment.MaximumPoints}";
          }
        }

        return new SolutionDto {
          Id = e.Id,
          AmountOfFiles = e.Files.Count,
          DateTime = e.UpdatedAt,
          Status = st,
        };
      }
    ).ToList();

    var dto = new AssignmentDto {
      Title = assignment.Title,
      Description = assignment.Description,
      AvailableUntil = assignment.AvailableUntil,
      Id = assignment.Id,
      MaximumPoints = assignment.MaximumPoints,
      Rating = rating,
      Status = status,
      Solutions = solutionDtos,
    };

    await SendAsync(dto, cancellation: ct);
  }
}