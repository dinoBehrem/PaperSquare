﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MimeKit;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Infrastructure.MailService.Models;
using PaperSquare.Infrastructure.MailService.Service;

namespace PaperSquare.Infrastructure.Data.Interceptors;

public sealed class PaperSquareSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;
    private readonly IMailService _mailService;

    public PaperSquareSaveChangesInterceptor(ICurrentUser currentUser, IMailService mailService)
    {
        _currentUser = currentUser;
        _mailService = mailService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {


        _mailService.SendMailAsync(new EmailData(new MailboxAddress("Dino", "dino_behrem@hotmail.com"), "Welcome mail!", "Welcome to the application!"));

        DbContext? dbContrext = eventData.Context;

        if (dbContrext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = dbContrext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (!string.IsNullOrWhiteSpace(_currentUser.Id))
                    {
                        entry.Property(e => e.CreatedBy).CurrentValue = _currentUser.Id;
                    }
                    else if (entry.Entity is User entity)
                    {
                        entry.Property(e => e.CreatedBy).CurrentValue = entity.Id;
                    }
                    entry.Property(e => e.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property(e => e.LastModifiedBy).CurrentValue = _currentUser.Id;
                    entry.Property(e => e.LastModifiedOnUtc).CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.Property(e => e.DeletedBy).CurrentValue = _currentUser.Id;
                    entry.Property(e => e.DeletedOnUtc).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}