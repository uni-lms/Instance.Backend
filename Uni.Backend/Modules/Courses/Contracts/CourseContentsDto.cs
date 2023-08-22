using JetBrains.Annotations;

using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;


namespace Uni.Backend.Modules.Courses.Contracts;

public class CourseContentsDto {
  public required string Name { [UsedImplicitly] get; set; }
  public int Semester { [UsedImplicitly] get; set; }
  public required List<string> Owners { [UsedImplicitly] get; set; }
  public required Dictionary<string, List<TextContent>> TextContents { [UsedImplicitly] get; set; }
  public required Dictionary<string, List<FileContentDto>> FileContents { [UsedImplicitly] get; set; }
  public required Dictionary<string, List<QuizDto>> Quizzes { [UsedImplicitly] get; set; }
}