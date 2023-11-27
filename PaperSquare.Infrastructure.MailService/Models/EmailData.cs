using MimeKit;

namespace PaperSquare.Infrastructure.MailService.Models;

public sealed record EmailData(MailboxAddress recipient, string subject, string content);
