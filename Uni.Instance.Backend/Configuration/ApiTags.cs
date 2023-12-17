namespace Uni.Instance.Backend.Configuration;

public class ApiTags {
  public static readonly ApiTag Internal = new ApiTag("Internal", "Internal service endpoints");
  public static readonly ApiTag Auth = new ApiTag("Auth", "Endpoints related to user accounts processes");
}

public record ApiTag(string Tag, string Description);