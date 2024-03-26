namespace Aip.Instance.Backend.Configuration.Swagger;

public static class CanBeUsedBy {
  private const string UsedBy = "<b>Может использоваться:</b>";
  public const string Anonymous = $"{UsedBy} Анонимным пользователем";
  public const string AnyAuthorized = $"{UsedBy} Любым авторизованным пользователем";
  public const string AnyIntern = $"{UsedBy} Любым <i>стажёром</i>";
  public const string AnyInvitedTutor = $"{UsedBy} Любым <i>приглашённым преподавателем</i> на выбранной стажировке";
  public const string AnyPrimaryTutor = $"{UsedBy} Любым <i>ведущим преподавателем</i> на выбранной стажировке";
  public const string AnyTutor = $"{UsedBy} Любым <i>преподавателем</i> на выбранной стажировке";

  public static readonly string[] OnlyIntern = ["Intern"];
}