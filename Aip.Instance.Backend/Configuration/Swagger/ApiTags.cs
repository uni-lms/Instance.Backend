namespace Aip.Instance.Backend.Configuration.Swagger;

public static class ApiTags {
  public static readonly ApiTag Internal = new("Служебные", "Внутренние служебные методы");

  public static readonly ApiTag Auth = new("Авторизация",
    "Методы, связанные с управлением пользовательскими аккаунтами");

  public static readonly ApiTag Flows = new("Потоки стажировок", "Методы, связанные с управлением потоками стажировок");
  public static readonly ApiTag Users = new("Пользователи", "Методы, связанные с управлением пользователями");

  public static readonly ApiTag Sections =
    new("Разделы стажировок", "Методы, связанные с управлением секциями в стажировке");

  public static readonly ApiTag Internships =
    new("Стажировки", "Методы, связанные с управлением стажировками");

  public static readonly ApiTag FileContent =
    new("Файлы", "Методы, связанные с управлением файловым контентом на стажировках");

  public static readonly ApiTag TextContent =
    new("Текст", "Методы, связанные с управлением текстовым контентом на стажировках");

  public static readonly ApiTag LinkContent =
    new("Ссылки", "Методы, связанные с управлением ссылочным контентом на стажировках");

  public static readonly ApiTag Content =
    new("Контент", "Методы, связанные с управлением контентом на стажировках");

  public static readonly ApiTag Assignment =
    new("Задания", "Методы, связанные с управлением заданиями на стажировках");

  public static readonly ApiTag Calendar =
    new("Календарь", "Методы, связанные с показом событий на календаре");
}