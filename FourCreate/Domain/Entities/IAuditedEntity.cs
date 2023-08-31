namespace Domain.Entities;

public interface IAuditedEntity
{
    public DateTime CreatedAt { get; }
}

