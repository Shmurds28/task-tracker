

namespace Domain.Model;

public class TaskItem
{
     public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.New;
    public TaskPriority Priority { get; set; } = TaskPriority.Low;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}