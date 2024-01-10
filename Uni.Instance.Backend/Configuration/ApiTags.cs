namespace Uni.Instance.Backend.Configuration;

public static class ApiTags {
  public static readonly ApiTag Internal = new("Internal", "Внутренние служебные методы");
  public static readonly ApiTag Auth = new("Auth", "Методы, связанные с управлением пользовательскими аккаунтами");
}

public record ApiTag(string Tag, string Description);