using FastEndpoints;

using Riok.Mapperly.Abstractions;

using Uni.Backend.Modules.Courses.Contracts;
using Uni.Backend.Modules.Users.Contracts;

using Group = Uni.Backend.Modules.Groups.Contracts.Group;


namespace Uni.Instance.Backend.Modules.Courses.Contracts;

[Mapper]
public partial class CoursesMapper : ResponseMapper<CourseDto, Course> {
  public partial CourseDto FromEntity(Course r);

  public CourseDtoV2 FromEntityToV2(Course r) {
    return new CourseDtoV2 {
      Abbreviation = r.Abbreviation,
      Name = r.Name,
      Progress = 90,
      Id = r.Id,
      Semester = r.Semester,
      Tutors = r.Owners.Select(UserDtoToString).ToList(),
    };
  }
  public CourseTutorDto FromEntityToTutorDto(Course r) {
    return new CourseTutorDto {
      Abbreviation = r.Abbreviation,
      Name = r.Name,
      Id = r.Id,
      Semester = r.Semester,
      Groups = r.AssignedGroups.Select(GroupToString).ToList(),
    };
  }

  private string UserDtoToString(User u) {
    var patronymic = "";
    if (u.Patronymic is not null) {
      patronymic = $" {u.Patronymic[0]}.";
    }

    return $"{u.LastName} {u.FirstName[0]}.{patronymic}";
  }

  private string GroupToString(Group g) {
    return g.Name;
  }
}