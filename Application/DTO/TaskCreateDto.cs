namespace Application.DTO;

public class TaskCreateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }   // "New", "InProgress", "Done"
    public string? Priority { get; set; } // "Low", "Medium", "High"
    public DateTime? DueDate { get; set; }
}