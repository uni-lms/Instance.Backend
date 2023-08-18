namespace Uni.Backend.Configuration;

public static class UserRoles {
  public const string Student = "Student";
  public const string Tutor = "Tutor";
  public const string Administrator = "Administrator";

  private static readonly string[] MinimumRequiredStudent = { "Student", "Tutor", "Administrator" };
  private static readonly string[] MinimumRequiredTutor = { "Tutor", "Administrator" };
  private static readonly string[] MinimumRequiredAdministrator = { "Administrator" };

  public static string[] MinimumRequired(string roleName) {
    return roleName switch {
      "Student"       => MinimumRequiredStudent,
      "Tutor"         => MinimumRequiredTutor,
      "Administrator" => MinimumRequiredAdministrator,
      _               => throw new ArgumentOutOfRangeException(roleName)
    };
  }
}