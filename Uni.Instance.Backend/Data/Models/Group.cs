using Uni.Instance.Backend.Data.Common;


namespace Uni.Instance.Backend.Data.Models;

public class Group : BaseModel {
  public required string Name { get; set; }
  public int EnteringYear { get; set; }
  public int YearsOfStudy { get; set; }
  public required List<User> Students { get; set; }
  public required List<Course> Courses { get; set; }

  public int GetCurrentSemester(DateTime currentDate) {
    var yearsElapsed = currentDate.Year - EnteringYear;

    switch (currentDate.Month) {
      case >= 9:
      case <= 1: {
        if (currentDate.Month >= 9) {
          return yearsElapsed * 2 + 1;
        }

        return yearsElapsed * 2;
      }
      case <= 6:
        return yearsElapsed * 2 + 2;
      default:
        return 0;
    }
  }
}