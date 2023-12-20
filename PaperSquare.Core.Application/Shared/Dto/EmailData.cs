using Microsoft.AspNetCore.Http;

namespace PaperSquare.Infrastructure.MailService.Models;

public sealed class EmailData
{
    public List<string> Recipients { get; init;  }
    public string Subject { get; init; }
    public string Content { get; init; }
    public IFormFileCollection? Attachments { get; set; }

    public EmailData(List<string> recipient, string subject, string content)
    {
        Recipients = recipient;
        Subject = subject;
        Content = content;
    }
}
