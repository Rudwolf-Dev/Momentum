namespace Momentum.Services;

public class TaskService
{
    private readonly DatabaseService _databaseService;

    public TaskService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await _databaseService.GetTasksAsync();
    }

    public async Task AddTaskAsync(string title)
    {
        var task = new TaskItem
        {
            Title = title,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        await _databaseService.SaveTaskAsync(task);
    }

    public async Task DeleteTaskAsync(TaskItem task)
    {
        await _databaseService.DeleteTaskAsync(task);
    }

    public async Task ToggleTaskAsync(TaskItem task)
    {
        task.IsCompleted = !task.IsCompleted;

        await _databaseService.SaveTaskAsync(task);
    }
}