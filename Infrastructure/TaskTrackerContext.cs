using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class TaskTrackerContext : DbContext
{
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                Id = 1,
                Title = "Code Review",
                Description = "Look into today's pull requests.",
                Status = Domain.TaskStatus.InProgress,
                Priority = Domain.TaskPriority.Medium,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                DueDate = DateTime.UtcNow.AddDays(2)
            },
            new TaskItem
            {
                Id = 2,
                Title = "New API",
                Description = "Add requested new API ",
                Status = Domain.TaskStatus.New,
                Priority = Domain.TaskPriority.High,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(5)
            },
            new TaskItem
            {
                Id = 3,
                Title = "Setup meeting with QA team",
                Description = "Meet with QA on failing test cases.",
                Status = Domain.TaskStatus.Done,
                Priority = Domain.TaskPriority.Low,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                DueDate = null
            }
        );
    }
}