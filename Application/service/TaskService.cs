using Domain.Model;
using Infrastructure.repository;
using Microsoft.EntityFrameworkCore;

namespace Application.service;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItem> CreateAsync(TaskItem taskItem)
    {
         if (taskItem == null) throw new ArgumentNullException(nameof(taskItem));
        if (string.IsNullOrWhiteSpace(taskItem.Title)) throw new ArgumentException("Title is required.", nameof(taskItem.Title));
        // Validate enums by type already (caller should set TaskStatus/TaskPriority)
        taskItem.Title = taskItem.Title.Trim();
        taskItem.Description = string.IsNullOrWhiteSpace(taskItem.Description) ? null : taskItem.Description.Trim();
        taskItem.CreatedAt = DateTime.UtcNow;
        if (taskItem.DueDate.HasValue) taskItem.DueDate = taskItem.DueDate.Value.ToUniversalTime();
        var created = await _taskRepository.AddAsync(taskItem);
        return created;
    }

    public async Task<bool> DeleteAsync(int id)
    {
         var existing = await _taskRepository.GetByIdAsync(id);
        if (existing == null) return false;
        await _taskRepository.DeleteAsync(existing);

        return true;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string? q, string? sort)
    {
        var query = _taskRepository.Query();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var qLower = q.Trim();
            query = query.Where(t =>
                (t.Title ?? string.Empty).Contains(qLower, StringComparison.Ordinal) ||
                (t.Description ?? string.Empty).Contains(qLower, StringComparison.Ordinal)
            );
        }

        // Default: dueDate:asc
        query = string.Equals(sort, "dueDate:desc", StringComparison.OrdinalIgnoreCase)
            ? query.OrderByDescending(t => t.DueDate.HasValue).ThenByDescending(t => t.DueDate)
            : query.OrderBy(t => t.DueDate.HasValue).ThenBy(t => t.DueDate);

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem task)
    {
        var existing = await _taskRepository.GetByIdAsync(id);
        if (existing == null) return null;
        if (task == null) throw new ArgumentNullException(nameof(task));
        if (string.IsNullOrWhiteSpace(task.Title)) throw new ArgumentException("Title is required.", nameof(task.Title));

        existing.Title = task.Title.Trim();
        existing.Description = string.IsNullOrWhiteSpace(task.Description) ? null : task.Description.Trim();
        existing.Status = task.Status;
        existing.Priority = task.Priority;
        existing.DueDate = task.DueDate?.ToUniversalTime();

        await _taskRepository.UpdateAsync(existing);

        return existing;
    }
}