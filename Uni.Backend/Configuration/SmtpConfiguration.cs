namespace Uni.Backend.Configuration;

public class SmtpConfiguration
{
    public required string SenderAddress { get; set; }
    public required string ServerUrl { get; set; }
    public int Port { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}