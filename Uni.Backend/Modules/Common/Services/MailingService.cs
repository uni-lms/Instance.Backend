using MailKit.Net.Smtp;

using MimeKit;

using Razor.Templating.Core;

using Uni.Backend.Background.Mailings.Contracts;
using Uni.Backend.Configuration;
using Uni.Backend.Modules.Users.Contracts;


namespace Uni.Backend.Modules.Common.Services;

public class MailingService {
  private readonly UniversityConfiguration _configuration;

  public MailingService(UniversityConfiguration configuration) {
    _configuration = configuration;
  }

  public async Task SendEmailAsync(CreateGroupMailingContract data) {
    var message = new MimeMessage {
      Subject = $"Регистрация в LMS {_configuration.Name}",
      Body = new TextPart("html") { Text = await RenderTemplate(data.Credentials, data.GroupName) },
    };

    message.From.Add(new MailboxAddress($"LMS {_configuration.Name}",
      _configuration.SmtpConfiguration.SenderAddress));
    message.To.Add(new MailboxAddress("", data.Credentials.Email));

    using var client = new SmtpClient();
    await client.ConnectAsync(_configuration.SmtpConfiguration.ServerUrl, _configuration.SmtpConfiguration.Port,
      false);
    await client.AuthenticateAsync(_configuration.SmtpConfiguration.Login,
      _configuration.SmtpConfiguration.Password);
    await client.SendAsync(message);
    await client.DisconnectAsync(true);
  }

  private async Task<string> RenderTemplate(UserCredentials credentials, string groupName) {
    var viewBag = new Dictionary<string, object> {
      { "Credentials", credentials },
      { "GroupName", groupName },
      { "Domain", _configuration.Domain },
      { "University", _configuration.Name },
    };
    return await RazorTemplateEngine.RenderAsync("/Templates/Mailings/CreateGroupCredentials.cshtml",
      viewBagOrViewData: viewBag);
  }
}