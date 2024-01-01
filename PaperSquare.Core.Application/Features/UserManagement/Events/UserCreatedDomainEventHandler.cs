using MediatR;
using PaperSquare.Core.Application.Shared;
using PaperSquare.Core.Domain.Entities.UserAggregate.Events;
using PaperSquare.Infrastructure.MailService.Models;
using System.Text;

namespace PaperSquare.Core.Application.Features.UserManagement.Events;

internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IMailService _mailService;
    private readonly IAzureBlobStorageService _azureBlobStorageService;
    private const string containerName = "mail-templates";
    private const string blobName = "verify-email.html";
    private const string mailSubject = "Verification email!";

    public UserCreatedDomainEventHandler(IMailService mailService, IAzureBlobStorageService azureBlobStorageService)
    {
        _mailService = mailService;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task Handle(UserCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var template = await _azureBlobStorageService.DownaloadBlobAsByteArrayAsync(blobName, containerName);

        string content = Encoding.UTF8.GetString(template);

        string mailTemplate = String.Format(content, @event.username, @event.verificationCode.Code);

        EmailData email = new EmailData(new List<string> { @event.mail }, mailSubject, mailTemplate);

        await _mailService.SendVerificationMailsAsync(email);
    }
}