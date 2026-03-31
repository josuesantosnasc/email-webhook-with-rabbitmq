
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Shared.DTOs;

namespace EmailNotificationWebHook.Services;

public class EmailService: IEmailService
{
    public string SendEmail(EmailDTO email)
    {
        var _email = new MimeMessage();
        _email.From.Add(MailboxAddress.Parse("ally.ruecker23@ethereal.email'"));
        _email.To.Add(MailboxAddress.Parse("ally.ruecker23@ethereal.email'"));
        _email.Subject = email.Title;
        _email.Body = new TextPart(MimeKit.Text.TextFormat.Html){Text=email.Content};
        using var stmp = new SmtpClient();
        stmp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        stmp.Authenticate("ally.ruecker23@ethereal.email", "kbjqJZRATEqXByCTTS",CancellationToken.None);
        stmp.Send(_email);
        stmp.Disconnect(true);
        return "Email sent";

    }
}