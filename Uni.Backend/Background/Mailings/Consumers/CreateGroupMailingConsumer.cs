using MassTransit;
using Uni.Backend.Background.Contracts;

namespace Uni.Backend.Background.Mailings.Consumers;

public class CreateGroupMailingConsumer : IConsumer<CreateGroupMailingContract>
{
    private readonly ILogger<CreateGroupMailingConsumer> _logger;

    public CreateGroupMailingConsumer(ILogger<CreateGroupMailingConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CreateGroupMailingContract> context)
    {
        _logger.LogInformation("Sending letter to {Email}", context.Message.Credentials.Email);
        return Task.CompletedTask;
    }
}