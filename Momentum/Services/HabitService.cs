namespace Momentum.Services;

public class HabitService
{
    private readonly DatabaseService _databaseService;

    public HabitService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<HabitItem>> GetHabitsAsync()
    {
        return await _databaseService.GetHabitsAsync();
    }

    public async Task AddHabitAsync(string name)
    {
        var habit = new HabitItem
        {
            Name = name,
            CompletedToday = false,
            Streak = 0,
            LastCompletedDate = DateTime.Now
        };

        await _databaseService.SaveHabitAsync(habit);
    }

    public async Task CompleteHabitAsync(HabitItem habit)
    {
        habit.CompletedToday = true;
        habit.Streak++;

        habit.LastCompletedDate = DateTime.Now;

        await _databaseService.SaveHabitAsync(habit);
    }

    public async Task DeleteHabitAsync(HabitItem habit)
    {
        await _databaseService.DeleteHabitAsync(habit);
    }
}