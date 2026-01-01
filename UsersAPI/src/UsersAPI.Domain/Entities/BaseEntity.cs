namespace UsersAPI.Domain.Entities;

/// <summary>
/// Base class for all domain entities.
/// Provides identity and audit information.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    protected void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
