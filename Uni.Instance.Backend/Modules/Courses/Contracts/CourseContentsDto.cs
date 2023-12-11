using JetBrains.Annotations;

using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.CourseContents.File.Contracts;
using Uni.Backend.Modules.CourseContents.Quiz.Contracts;
using Uni.Backend.Modules.CourseContents.Text.Contract;
using Uni.Instance.Backend.Modules.CourseBlocks.Contracts;


namespace Uni.Instance.Backend.Modules.Courses.Contracts;

public class CourseContentsDto {
  public required string Name { [UsedImplicitly] get; set; }
  public required string Abbreviation { [UsedImplicitly] get; set; }
  public int Semester { [UsedImplicitly] get; set; }
  public required List<CourseBlockDto> Blocks { [UsedImplicitly] get; set; }
}