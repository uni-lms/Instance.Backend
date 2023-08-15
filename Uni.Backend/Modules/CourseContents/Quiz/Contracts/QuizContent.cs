using Uni.Backend.Data;
using Uni.Backend.Modules.CourseContents.Abstractions;

namespace Uni.Backend.Modules.CourseContents.Quiz.Contracts;

public class QuizContent : BaseModel
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TimeSpan? TimeLimit { get; set; }
    public bool IsQuestionsShuffled { get; set; }
    public DateTime AvailableUntil { get; set; }
    public required List<MultipleChoiceQuestion> Questions { get; set; }
}