using Domain.Model;

namespace Infrastructure.repository;

public class TaskRepository : ITaskRepository
{
     private readonly TaskTrackerContext _context;
    public TaskRepository(TaskTrackerContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> AddAsync(TaskItem item)
    {
        var e = await _context.TaskItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return e.Entity;
    }

    public async Task DeleteAsync(TaskItem item)
    {
        _context.TaskItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public IQueryable<TaskItem> Query()
    {
        return _context.TaskItems.AsQueryable();
    }

    public async Task UpdateAsync(TaskItem item)
    {
        _context.TaskItems.Update(item);
        await _context.SaveChangesAsync();
    }
}