namespace Uni.Instance.Backend.Configuration;

public static class ApiTags {
  public static readonly ApiTag Internal = new("Internal", "Internal service endpoints");
  public static readonly ApiTag Auth = new("Auth", "Endpoints related to user accounts processes");
}

public record ApiTag(string Tag, string Description);