﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;

namespace PaperSquare.Data.Interceprots;

public sealed class PaperSquareSaveChangesInterceptor: SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;

    public PaperSquareSaveChangesInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContrext = eventData.Context;

        if(dbContrext is null) 
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = dbContrext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
            switch(entry.State)
            {
                case EntityState.Added:
                    entry.Property(e => e.CreatedBy).CurrentValue = _currentUser.Id;
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
