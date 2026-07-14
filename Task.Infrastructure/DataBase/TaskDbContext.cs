using Microsoft.EntityFrameworkCore;
using Task.Domain.Entity;
using Task.Application.Interface.DataBase;
using Task.Application.Interface.TenantInterface;

namespace Task.Infrastructure.DataBase;

public class TaskDbContext : DbContext, ITaskDbContext
{
    private readonly ICurrentTenant _currentTenant;
    public TaskDbContext(
        DbContextOptions<TaskDbContext> options,
        ICurrentTenant currentTenant) 
        : base(options)
    {
        _currentTenant = currentTenant;
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>()
          .HasQueryFilter(t => t.TenantId == _currentTenant.CurrentTenantId || _currentTenant.IsAdminUser);
        modelBuilder.Entity<User>()
          .HasQueryFilter(t => t.TenantId == _currentTenant.CurrentTenantId || _currentTenant.IsAdminUser);
        modelBuilder.Entity<Project>()
          .HasQueryFilter(t => t.TenantId == _currentTenant.CurrentTenantId || _currentTenant.IsAdminUser);
    
        // User ForeignKey：TenantId
        modelBuilder.Entity<User>()
          .HasOne<Tenant>()
          .WithMany()
          .HasForeignKey(user => user.TenantId)
          .OnDelete(DeleteBehavior.Restrict);

        // Project ForeignKey：TenantId
        modelBuilder.Entity<Project>()
          .HasOne<Tenant>()
          .WithMany()
          .HasForeignKey(project => project.TenantId)
          .OnDelete(DeleteBehavior.Restrict);

        // TaskItem ForeignKey：TenantId
        modelBuilder.Entity<TaskItem>()
          .HasOne<Tenant>()
          .WithMany()
          .HasForeignKey(task => task.TenantId)
          .OnDelete(DeleteBehavior.Restrict);

        // TaskItem ForeignKey：ProjectId
        modelBuilder.Entity<TaskItem>()
          .HasOne<Project>()
          .WithMany()
          .HasForeignKey(task => task.ProjectId)
          .OnDelete(DeleteBehavior.Restrict);

        // TaskItem ForeignKey：AssigneeUserId
        modelBuilder.Entity<TaskItem>()
          .HasOne(task => task.AssigneeUser)
          .WithMany()
          .HasForeignKey(task => task.AssigneeUserId)
          .OnDelete(DeleteBehavior.Restrict);
    }

    private void ApplyTenantId()
    {
        foreach (var entry in ChangeTracker.Entries<TaskItem>())
        {
            if (entry.State == EntityState.Added && entry.Entity.TenantId == Guid.Empty)
                entry.Entity.TenantId = _currentTenant.CurrentTenantId;
        }
        foreach (var entry in ChangeTracker.Entries<Project>())
        {
            if (entry.State == EntityState.Added && entry.Entity.TenantId == Guid.Empty)
                entry.Entity.TenantId = _currentTenant.CurrentTenantId;
        }
    }

    public override int SaveChanges()
    {
        ApplyTenantId();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTenantId();
        return base.SaveChangesAsync(cancellationToken);
    }
}
