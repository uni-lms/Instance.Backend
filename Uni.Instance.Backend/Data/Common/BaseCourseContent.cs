using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Data.Common;

public abstract class BaseCourseContent : BaseModel {
  public required Course Course { get; set; }
  public required CourseSection Section { get; set; }

  public bool IsVisibleToStudents { get; set; }
}