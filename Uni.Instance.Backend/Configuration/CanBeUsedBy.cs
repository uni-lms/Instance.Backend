namespace Uni.Instance.Backend.Configuration;

public static class CanBeUsedBy {
  private const string UsedBy = "<b>Может использоваться:</b>";
  public const string Anonymous = $"{UsedBy} Анонимным пользователем";
  public const string AnyAuthorized = $"{UsedBy} Любым авторизованным пользователем";
  public const string AnyStudent = $"{UsedBy} Любым <i>студентом</i>";
  public const string AnyTutor = $"{UsedBy} Любым <i>преподавателем</i>";
  public const string AnyAdmin = $"{UsedBy} Любым <i>администратором</i>";
}