namespace Task.Domain.Entity;

public class Project
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}