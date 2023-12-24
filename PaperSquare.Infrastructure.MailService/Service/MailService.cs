using Microsoft.Extensions.Options;
using MimeKit;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Infrastructure.MailService.Models;

namespace PaperSquare.Infrastructure.MailService.Service;

internal sealed class MailService : IMailService
{
    private readonly EmailOptions _mailSettings;

    public MailService(IOptions<EmailOptions> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<bool> SendMailAsync(EmailData emailData)
    {
        MimeMessage email = new MimeMessage();

        // Set the sender of the email
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From);

        // Add recipients of the email
        email.To.AddRange(emailData.Recipients.Select(r => new MailboxAddress(r, r)));
        
        // Set the mail content

        // TO DO: Create email templte

        var body = new BodyBuilder();

        email.Subject = emailData.Subject;
        body.HtmlBody = emailData.Content;

        email.Body = body.ToMessageBody();

        // Send email

        using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                smtpClient.Connect(_mailSettings.Host, _mailSettings.Port);

                smtpClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);

                await smtpClient.SendAsync(email);

                smtpClient.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public async Task<bool> SendVerificationMailsAsync(EmailData emailData)
    {
        MimeMessage email = new MimeMessage();

        // Set the sender of the email
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From);

        // Add recipients of the email
        email.To.AddRange(emailData.Recipients.Select(r => new MailboxAddress(r, r)));

        // Set the mail content

        var body = new BodyBuilder();

        email.Subject = emailData.Subject;
        body.HtmlBody = emailData.Content;

        email.Body = body.ToMessageBody();

        // Send email

        using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                smtpClient.Connect(_mailSettings.Host, _mailSettings.Port);

                smtpClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);

                await smtpClient.SendAsync(email);

                smtpClient.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
