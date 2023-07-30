using MassTransit;
using Uni.Backend.Background.Mailings.Contracts;
using Uni.Backend.Modules.Common.Services;

namespace Uni.Backend.Background.Mailings.Consumers;

public class CreateGroupMailingConsumer : IConsumer<CreateGroupMailingContract>
{
    private readonly ILogger<CreateGroupMailingConsumer> _logger;
    private readonly MailingService _mailingService;

    public CreateGroupMailingConsumer(ILogger<CreateGroupMailingConsumer> logger, MailingService mailingService)
    {
        _logger = logger;
        _mailingService = mailingService;
    }

    public Task Consume(ConsumeContext<CreateGroupMailingContract> context)
    {
        _logger.LogInformation("Sending letter to {Email}", context.Message.Credentials.Email);
        return Task.CompletedTask;
    }
}