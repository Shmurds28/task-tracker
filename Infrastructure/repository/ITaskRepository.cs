using Domain.Model;

namespace Infrastructure.repository;

public interface ITaskRepository
{
    IQueryable<TaskItem> Query();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItem item);
    Task UpdateAsync(TaskItem item);
    Task DeleteAsync(TaskItem item);
}