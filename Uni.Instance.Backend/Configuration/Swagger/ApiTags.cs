namespace Uni.Instance.Backend.Configuration.Swagger;

public static class ApiTags {
  public static readonly ApiTag Internal = new("Internal", "Внутренние служебные методы");
  public static readonly ApiTag Auth = new("Auth", "Методы, связанные с управлением пользовательскими аккаунтами");

  public static readonly ApiTag CourseSections =
    new("CourseSections", "Методы, связанные с управлением секциями в курсе");
}

public record ApiTag(string Tag, string Description);