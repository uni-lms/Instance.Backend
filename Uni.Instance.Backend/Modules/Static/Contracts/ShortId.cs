using System.Text;


namespace Uni.Backend.Modules.Static.Contracts;

public static class ShortId {
  public static string FromGuid(Guid guid) {
    var base64 = Convert.ToBase64String(guid.ToByteArray());
    return base64
      .Replace("+", "-")
      .Replace("/", "_")[..^2];
  }

  public static Guid ToGuid(string value) {
    var sb = new StringBuilder(value
      .Replace("-", "+")
      .Replace("_", "/"));
    sb.Append("==");
    var ba = Convert.FromBase64String(sb.ToString());
    return new Guid(ba);
  }
}