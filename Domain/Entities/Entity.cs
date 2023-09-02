namespace Domain.Entities;

public abstract class Entity<T>
{
    public Entity(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    // for code-generated ids
    public Entity(T id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public T? Id { get; init; }

    public DateTime CreatedAt { get; init; }
}

