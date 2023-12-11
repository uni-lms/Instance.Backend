using FastEndpoints;

using JetBrains.Annotations;

using Riok.Mapperly.Abstractions;

using Uni.Instance.Backend.Modules.CourseContents.Quiz.Contracts;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

[Mapper]
public partial class QuizPassAttemptMapper : ResponseMapper<QuizPassAttemptDto, QuizPassAttempt> {
  public QuizPassAttemptDto FromEntity(QuizPassAttempt e) {
    var dto = QuizPassAttemptToDto(e);

    dto.Points = CalculateAccruedPoints(e.AccruedPoints);

    return dto;
  }

  private QuizPassAttemptDto QuizPassAttemptToDto(QuizPassAttempt e) {
    var dto = new QuizPassAttemptDto {
      Id = e.Id,
      Points = CalculateAccruedPoints(e.AccruedPoints),
    };

    return dto;
  }

  [UsedImplicitly]
  private int CalculateAccruedPoints(List<AccruedPoint> points) {
    return points.Sum(e => e.AmountOfPoints);
  }

  [UsedImplicitly]
  private TimeSpan? CalculateSpentTime(DateTime startedAt, DateTime? finishedAt) {
    return finishedAt - startedAt;
  }
}