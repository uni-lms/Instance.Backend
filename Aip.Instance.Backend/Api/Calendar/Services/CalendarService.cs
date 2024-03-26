using System.Security.Claims;

using Aip.Instance.Backend.Api.Calendar.Data;
using Aip.Instance.Backend.Data;

using Ardalis.Result;

using FastEndpoints.Security;

using Microsoft.EntityFrameworkCore;


namespace Aip.Instance.Backend.Api.Calendar.Services;

public class CalendarService(AppDbContext db) {
  public async Task<Result<MonthEventsResponse>> GetEventsInMonth(
    ClaimsPrincipal user,
    MonthEventsRequest req,
    CancellationToken ct
  ) {
    var userEmail = user.ClaimValue(ClaimTypes.Name);

    if (userEmail is null) {
      return Result.Unauthorized();
    }

    var userData = await db.Users.Where(e => e.Email == userEmail).FirstOrDefaultAsync(ct);

    if (userData is null) {
      return Result.NotFound(nameof(userEmail));
    }

    var internships = await db.InternshipBasedRoles
      .Include(e => e.Internship)
      .Include(e => e.User)
      .Where(e => e.User == userData)
      .Select(e => e.Internship)
      .ToListAsync(ct);

    var assignments = await db.Assignments
      .Where(e => e.Deadline.Month == req.Month && e.Deadline.Year == req.Year &&
        internships.Any(i => i == e.Internship))
      .ToListAsync(ct);

    var events = new List<DayEventsOverview>();

    foreach (var assignment in assignments) {
      events.Add(new DayEventsOverview {
        DayOfMonth = assignment.Deadline.Day,
        HasDeadlines = true,
      });
    }

    return Result.Success(new MonthEventsResponse {
      Days = events,
      Month = req.Month,
      Year = req.Year,
    });
  }

  public async Task<Result<DayEventsResponse>> GetEventsInDay(
    ClaimsPrincipal user,
    DayEventsRequest req,
    CancellationToken ct
  ) {
    var userEmail = user.ClaimValue(ClaimTypes.Name);

    if (userEmail is null) {
      return Result.Unauthorized();
    }

    var userData = await db.Users.Where(e => e.Email == userEmail).FirstOrDefaultAsync(ct);

    if (userData is null) {
      return Result.NotFound(nameof(userEmail));
    }

    var internships = await db.InternshipBasedRoles
      .Include(e => e.Internship)
      .Include(e => e.User)
      .Where(e => e.User == userData)
      .Select(e => e.Internship)
      .ToListAsync(ct);

    var assignments = await db.Assignments
      .Where(e => e.Deadline.Month == req.Month && e.Deadline.Year == req.Year && e.Deadline.Day == req.Day &&
        internships.Any(i => i == e.Internship))
      .ToListAsync(ct);

    var events = assignments
      .Select(e => new DeadlineEvent {
        Id = e.Id,
        Title = e.Title,
      });

    return Result.Success(new DayEventsResponse {
      Events = events,
      Day = req.Day,
      Month = req.Month,
      Year = req.Year,
    });
  }
}