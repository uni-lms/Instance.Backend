using FluentAssertions;

using Uni.Instance.Backend.Data.Models;


namespace Uni.Instance.Backend.Tests.Unit;

public class GroupTests {
  public static TheoryData<DateTime, int> Cases = new() {
    { new DateTime(2020, 9, 10), 1 },
    { new DateTime(2021, 1, 10), 2 },
    { new DateTime(2021, 9, 10), 3 },
    { new DateTime(2022, 1, 10), 4 },
    { new DateTime(2022, 9, 10), 5 },
    { new DateTime(2023, 1, 10), 6 },
    { new DateTime(2023, 9, 10), 7 },
    { new DateTime(2024, 1, 10), 8 },
  };

  private static readonly Group Group = new() {
    Id = Guid.Empty,
    Name = string.Empty,
    EnteringYear = 2020,
    YearsOfStudy = 4,
    Courses = [],
    Students = [],
  };

  [Theory]
  [MemberData(nameof(Cases))]
  public void GetCurrentSemesterTests(DateTime currentDate, int result) {
    Group.GetCurrentSemester(currentDate).Should().Be(result);
  }
}