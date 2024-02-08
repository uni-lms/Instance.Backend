namespace Uni.Instance.Backend.Configuration.Swagger;

public static class ApiTags {
  public static readonly ApiTag Internal = new("Служебные", "Внутренние служебные методы");

  public static readonly ApiTag Auth = new("Авторизация",
    "Методы, связанные с управлением пользовательскими аккаунтами");

  public static readonly ApiTag Groups = new("Группы", "Методы, связанные с управлением группами");
  public static readonly ApiTag Users = new("Пользователи", "Методы, связанные с управлением пользователями");

  public static readonly ApiTag CourseSections =
    new("Разделы курсов", "Методы, связанные с управлением секциями в курсе");

  public static readonly ApiTag Courses =
    new("Курсы", "Методы, связанные с управлением курсами");

  public static readonly ApiTag CourseContentFile =
    new("Файлы", "Методы, связанные с управлением файловым контентом на курсах");
}

public record ApiTag(string Tag, string Description);