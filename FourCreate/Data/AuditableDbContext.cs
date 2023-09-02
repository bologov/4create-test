using Common;
using Data.Helpers;
using Domain.Entities;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    public abstract class AuditableDbContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        internal DbSet<SystemLog> SystemLogs { get; set; }

        public AuditableDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var changedAuditedEntities = ChangeTracker.Entries()
                .Where(x => x.Entity.GetType().IsSubclassOfGeneric(typeof(Entity<>))) // only types of audited entity
                .Where(x => x.Entity is not SystemLog) // ignore SystemLog records themselves
                .Where(x => x.State != EntityState.Detached) // ignore detached
                .Where(x => x.State != EntityState.Unchanged); // ignore unchanged

            var builders = new List<SystemLogBuilder>();

            foreach (var entry in changedAuditedEntities)
            {
                var builder = new SystemLogBuilder();
                builders.Add(builder);

                ProcessChangedEntityEntries(builder, entry);
            }

            SystemLogs.AddRange(builders.Select(x => x.ToSystemLog()));
        }

        private void ProcessChangedEntityEntries(SystemLogBuilder builder, EntityEntry entry)
        {
            builder.ResourceType = entry.Entity.GetType().Name;
            // potentially there are types with a composite primary key, so they would require multiple fields to be stored.
            builder.ResourceId = entry.Properties.SingleOrDefault(x => x.Metadata.IsPrimaryKey()).CurrentValue;
            builder.CreatedAt = _dateTimeProvider.GetUtcDateTimeNow();

            builder.SystemLogType = entry.State switch
            {
                EntityState.Added => SystemLogType.Create,
                EntityState.Deleted => SystemLogType.Delete,
                EntityState.Modified => SystemLogType.Update,
                _ => throw new ArgumentException("Not supported state of the entry.")
            };

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                // skip primary key
                if (property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                // skip not modified entries when entity is modified
                if (entry.State == EntityState.Modified && !property.IsModified)
                {
                    continue;
                }

                // write down unique index value - it can't be th primary key as it was skipped
                // there are potentially could be multiple unique indexes - but it isn't the case for now.
                if (property.Metadata.IsUniqueIndex())
                {
                    builder.ResourceUniqueValue = property.CurrentValue;
                }

                var originalValue = entry.State != EntityState.Added ? property.OriginalValue : null;
                var currentValue = entry.State != EntityState.Deleted ? property.CurrentValue : null;

                builder.AddPropertyValues(propertyName, originalValue, currentValue);
            }
        }
    }
}

