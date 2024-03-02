﻿using Aip.Instance.Backend.Api.Common.Services;
using Aip.Instance.Backend.Api.Content.Assignment.Data;
using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Data.Models;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Content.Assignment.Services;

public class AssignmentService(AppDbContext db, StaticFileService service) {
  public async Task<Result<SearchByIdModel>> CreateAssignment(CreateAssignmentRequest req, CancellationToken ct) {
    var internship = await db.Internships.Where(e => e.Id == req.InternshipId).FirstOrDefaultAsync(ct);

    if (internship is null) {
      return Result.NotFound(nameof(internship));
    }

    var section = await db.Sections.Where(e => e.Id == req.SectionId).FirstOrDefaultAsync(ct);

    if (section is null) {
      return Result.NotFound(nameof(internship));
    }

    StaticFile? existedFile = null;

    if (req.File is not null) {
      var checksum = await service.CalculateChecksumAsync(req.File, ct);

      existedFile = await db.StaticFiles.Where(e => e.Checksum == checksum).FirstOrDefaultAsync(ct);

      if (existedFile is null) {
        var ioResult = await service.SaveFileAsync(req.File, ct);

        if (ioResult.IsSuccess) {
          existedFile = new StaticFile {
            Id = ioResult.FileId!,
            Checksum = checksum,
            Filename = req.File.FileName,
            Filepath = ioResult.FilePath,
          };

          await db.StaticFiles.AddAsync(existedFile, ct);
        }
        else {
          return Result.Error($"Не вышло загрузить файл: {ioResult.Error}");
        }
      }
    }

    var assignment = new Backend.Data.Models.Assignment {
      Title = req.Title,
      Deadline = req.Deadline,
      Internship = internship,
      Section = section,
      IsVisibleToInterns = req.IsVisibleToInterns,
      Description = req.Description,
      File = existedFile,
    };

    await db.Assignments.AddAsync(assignment, ct);
    await db.SaveChangesAsync(ct);

    return Result.Success(new SearchByIdModel {
      Id = assignment.Id,
    });
  }

  public async Task<Result<SearchByIdModel>> UpdateAssignment(UpdateAssignmentRequest req, CancellationToken ct) {
    var assignment = await db.Assignments
      .Where(e => e.Id == req.Id)
      .Include(e => e.Section)
      .FirstOrDefaultAsync(ct);

    if (assignment is null) {
      return Result.NotFound(nameof(assignment));
    }

    if (assignment.Section.Id != req.SectionId) {
      var section = await db.Sections
        .Where(e => e.Id == req.SectionId)
        .FirstOrDefaultAsync(ct);

      if (section is null) {
        return Result.NotFound(nameof(section));
      }

      assignment.Section = section;
    }

    assignment.Description = req.Description;
    assignment.Deadline = req.Deadline;
    assignment.Title = req.Title;
    assignment.IsVisibleToInterns = req.IsVisibleToInterns;

    await db.SaveChangesAsync(ct);

    return Result.Success(new SearchByIdModel {
      Id = assignment.Id,
    });
  }
}