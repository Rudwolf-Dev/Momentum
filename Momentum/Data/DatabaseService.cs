namespace Momentum.Data;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;

    // =========================================
    // INIT
    // =========================================

    public async Task InitAsync()
    {
        if (_database is not null)
            return;

        SQLitePCL.Batteries.Init();

        _database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.Flags);

        // =========================================
        // TABLES
        // =========================================

        await _database.CreateTableAsync<TaskItem>();
        await _database.CreateTableAsync<NoteItem>();
        await _database.CreateTableAsync<HabitItem>();
    }

    // =========================================
    // TASKS
    // =========================================

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        await InitAsync();

        return await _database!
            .Table<TaskItem>()
            .ToListAsync();
    }

    public async Task<int> SaveTaskAsync(TaskItem task)
    {
        await InitAsync();

        if (task.Id != 0)
            return await _database!.UpdateAsync(task);

        return await _database!.InsertAsync(task);
    }

    public async Task<int> DeleteTaskAsync(TaskItem task)
    {
        await InitAsync();

        return await _database!.DeleteAsync(task);
    }

    // =========================================
    // NOTES
    // =========================================

    public async Task<List<NoteItem>> GetNotesAsync()
    {
        await InitAsync();

        return await _database!
            .Table<NoteItem>()
            .ToListAsync();
    }

    public async Task<int> SaveNoteAsync(NoteItem note)
    {
        await InitAsync();

        if (note.Id != 0)
            return await _database!.UpdateAsync(note);

        return await _database!.InsertAsync(note);
    }

    public async Task<int> DeleteNoteAsync(NoteItem note)
    {
        await InitAsync();

        return await _database!.DeleteAsync(note);
    }

    // =========================================
    // HABITS
    // =========================================

    public async Task<List<HabitItem>> GetHabitsAsync()
    {
        await InitAsync();

        return await _database!
            .Table<HabitItem>()
            .ToListAsync();
    }

    public async Task<int> SaveHabitAsync(HabitItem habit)
    {
        await InitAsync();

        if (habit.Id != 0)
            return await _database!.UpdateAsync(habit);

        return await _database!.InsertAsync(habit);
    }

    public async Task<int> DeleteHabitAsync(HabitItem habit)
    {
        await InitAsync();

        return await _database!.DeleteAsync(habit);
    }
}