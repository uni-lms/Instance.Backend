using FastEndpoints;

using JetBrains.Annotations;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

[Mapper]
public partial class QuizPassAttemptDetailsMapper : ResponseMapper<QuizPassAttempt, QuizPassAttemptDetails> {
  public QuizPassAttemptDetails FromEntity(QuizPassAttempt entity) {
    var dto = new QuizPassAttemptDetails {
      AccruedPoints = entity.AccruedPoints
        .Select(e => new AccruedPointDto {
          QuestionTitle = e.Question.Text,
          AmountOfPoints = e.AmountOfPoints,
          MaximumPoints = e.Question.MaximumPoints,
        })
        .ToList(),
      TimeSpent = CalculateSpentTime(entity.StartedAt, entity?.FinishedAt),
    };


    return dto;
  }

  [UsedImplicitly]
  private TimeSpan? CalculateSpentTime(DateTime startedAt, DateTime? finishedAt) {
    return finishedAt - startedAt;
  }
}