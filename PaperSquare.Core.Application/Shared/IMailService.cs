using PaperSquare.Infrastructure.MailService.Models;

namespace PaperSquare.Core.Application.Shared;

public interface IMailService
{
    Task<bool> SendMailAsync(EmailData emailData);
    Task<bool> SendVerificationMailsAsync(EmailData emailData);
}
