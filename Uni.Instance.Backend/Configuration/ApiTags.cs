namespace Uni.Instance.Backend.Configuration;

public class ApiTags {
  public static readonly ApiTag Internal = new ApiTag("Internal", "Internal service endpoints");
}

public record ApiTag(string Tag, string Description);