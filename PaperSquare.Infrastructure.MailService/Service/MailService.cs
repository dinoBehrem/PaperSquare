using Microsoft.Extensions.Options;
using MimeKit;
using PaperSquare.Infrastructure.MailService.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace PaperSquare.Infrastructure.MailService.Service;

internal sealed class MailService : IMailService
{
    private readonly EmailConfiguration _mailSettings;

    public MailService(IOptions<EmailConfiguration> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<bool> SendMailAsync(EmailData emailData)
    {

        MimeMessage email = new MimeMessage();

        // Set the sender of the email
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From);

        // Add recipients of the email
        email.To.Add(emailData.recipient);

        // Set the mail content

        // TO DO: Create email templte

        var body = new BodyBuilder();

        email.Subject = emailData.subject;
        body.HtmlBody = emailData.content;

        email.Body = body.ToMessageBody();

        // Send email

        using (var smtpClient = new SmtpClient())
        {
            try
            {
                await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port);

                await smtpClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);

                await smtpClient.SendAsync(email);

                await smtpClient.DisconnectAsync(true);

                //smtpClient?.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
