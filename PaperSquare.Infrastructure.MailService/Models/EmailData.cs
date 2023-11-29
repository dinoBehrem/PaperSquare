using MimeKit;
using Microsoft.AspNetCore.Http;

namespace PaperSquare.Infrastructure.MailService.Models;

public sealed class EmailData
{
    public MailboxAddress Recipient { get; init;  }
    public string Subject { get; init; }
    public string Content { get; init; }
    public IFormFileCollection? Attachments { get; set; }

    public EmailData(MailboxAddress recipient, string subject, string content)
    {
        Recipient = recipient;
        Subject = subject;
        Content = content;
    }
}
