using Aip.Instance.Backend.Api.Flows.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;
using Aip.Instance.Backend.Extensions;

using Ardalis.Result;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Flows.Services;

public class FlowsService(AppDbContext db) {
  public async Task<Result<FlowDto>> GetGroupByIdAsync(SearchByIdModel req, CancellationToken ct) {
    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    var dto = new FlowDto {
      Id = group.Id,
      Name = group.Name,
    };

    return Result.Success(dto);
  }

  public async Task<Result<CreateFlowResponse>> CreateGroupAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    CreateFlowRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var group = new Flow {
      Name = req.Name,
      Internships = [],
      Students = [],
    };

    await db.Groups.AddAsync(group, ct);
    await db.SaveChangesAsync(ct);

    return Result.Success(new CreateFlowResponse {
      Id = group.Id,
      Name = group.Name,
    });
  }

  public async Task<Result<EditFlowResponse>> EditGroupAsync(
    bool validationFailed,
    IEnumerable<ValidationFailure> validationFailures,
    EditFlowRequest req,
    CancellationToken ct
  ) {
    if (validationFailed) {
      return Result.Invalid(validationFailures.ToValidationErrors());
    }

    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    group.Name = req.Name;

    await db.SaveChangesAsync(ct);

    return Result.Success(new EditFlowResponse {
      Name = req.Name,
    });
  }

  public async Task<Result<EditFlowResponse>> DeleteGroupAsync(SearchByIdModel req, CancellationToken ct) {
    var group = await db.Groups.Where(e => e.Id == req.Id).FirstOrDefaultAsync(ct);

    if (group is null) {
      return Result.NotFound();
    }

    db.Groups.Remove(group);
    await db.SaveChangesAsync(ct);

    return Result.Success(new EditFlowResponse {
      Name = group.Name,
    });
  }

  public async Task<Result<List<FlowDto>>> GetAllGroupsAsync(CancellationToken ct) {
    var groups = await db.Groups.Select(e => new FlowDto {
      Id = e.Id,
      Name = e.Name,
    }).ToListAsync(ct);

    return Result.Success(groups);
  }
}