using PaperSquare.Infrastructure.MailService.Models;

namespace PaperSquare.Infrastructure.MailService.Service;

public interface IMailService
{
    Task<bool> SendMailAsync(EmailData emailData);
}
