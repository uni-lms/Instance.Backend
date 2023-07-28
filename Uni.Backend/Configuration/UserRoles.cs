namespace Uni.Backend.Configuration;

public static class UserRoles
{
    public static readonly string Student = "Student";
    public static readonly string Tutor = "Tutor";
    public static readonly string Administrator = "Administrator";

    private static readonly string[] MinimumRequiredStudent = { "Student", "Tutor", "Administrator" };
    private static readonly string[] MinimumRequiredTutor = { "Tutor", "Administrator" };
    private static readonly string[] MinimumRequiredAdministrator = { "Administrator" };

    public static string[] MinimumRequired(string roleName)
    {
        return roleName switch
        {
            "Student" => MinimumRequiredStudent,
            "Tutor" => MinimumRequiredTutor,
            "Administrator" => MinimumRequiredAdministrator,
            _ => throw new ArgumentOutOfRangeException(roleName)
        };
    }
}