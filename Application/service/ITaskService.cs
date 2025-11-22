using Domain.Model;

namespace Application.service;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync(string? q, string? sort);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(int id, TaskItem task);
    Task<bool> DeleteAsync(int id);
}