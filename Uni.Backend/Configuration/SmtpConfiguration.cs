using JetBrains.Annotations;


namespace Uni.Backend.Configuration;

public class SmtpConfiguration {
  public required string SenderAddress { get; [UsedImplicitly] set; }
  public required string ServerUrl { get; [UsedImplicitly] set; }
  public int Port { get; [UsedImplicitly] set; }
  public required string Login { get; [UsedImplicitly] set; }
  public required string Password { get; [UsedImplicitly] set; }
}