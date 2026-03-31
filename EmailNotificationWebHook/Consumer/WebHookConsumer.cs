using MassTransit;
using Shared.DTOs;

namespace EmailNotificationWebHook.Consumer;

public class WebHookConsumer(HttpClient client) : IConsumer<EmailDTO>
{
    public async Task Consume(ConsumeContext<EmailDTO> context)
    {
        var result=await client.PostAsJsonAsync("http://localhost:5154/email-webhook", new EmailDTO(context.Message.Title, context.Message.Content));
        result.EnsureSuccessStatusCode();
    }
}
