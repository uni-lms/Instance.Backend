using System.Net.Mail;
using Razor.Templating.Core;
using Uni.Backend.Background.Mailings.Contracts;
using Uni.Backend.Configuration;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Common.Services;

public class MailingService
{

    private readonly UniversityConfiguration _configuration;

    public MailingService(UniversityConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmail(CreateGroupMailingContract data)
    {
        var message = new MailMessage
        {
            From = new MailAddress("my@email.com"),
            Subject = "Регистрация в LMS UNI",
            Body = await RenderTemplate(data.Credentials, data.GroupName),
            IsBodyHtml = true
        };
        
        message.To.Add(data.Credentials.Email);

    }
    private async Task<string> RenderTemplate(UserCredentials credentials, string groupName)
    {
        var viewBag = new Dictionary<string, object>
        {
            { "Credentials", credentials },
            { "GroupName", groupName },
            { "Domain", _configuration.Domain },
            { "University", _configuration.Name }
        };
        return await RazorTemplateEngine.RenderAsync("/Templates/Mailings/CreateGroupCredentials.cshtml", viewBagOrViewData: viewBag);
    }
}