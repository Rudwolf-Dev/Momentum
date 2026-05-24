namespace Momentum.ViewModels;

public partial class DashboardViewModel(
    DatabaseService databaseService,
    IDialogService dialogService,
    INavigationService navigationService)
    : BaseViewModel(
        dialogService,
        navigationService,
        "Dashboard")
{
    private readonly DatabaseService _databaseService = databaseService;

    public ObservableCollection<NoteItem> RecentNotes { get; set; } = new();

    [ObservableProperty]
    private int pendingTasks;

    [ObservableProperty]
    private int completedHabits;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProductivityText))]
    private double productivityProgress;

    public string ProductivityText =>
        $"{(int)(ProductivityProgress * 100)}% completed today";

    [RelayCommand]
    private async Task LoadDashboardAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            // =========================================
            // LOAD DATA
            // =========================================

            var tasks = await _databaseService.GetTasksAsync();

            var habits = await _databaseService.GetHabitsAsync();

            var notes = await _databaseService.GetNotesAsync();

            // =========================================
            // TASKS
            // =========================================

            PendingTasks = tasks.Count(t => !t.IsCompleted);

            // =========================================
            // HABITS
            // =========================================

            CompletedHabits = habits.Count(h => h.CompletedToday);

            // =========================================
            // PRODUCTIVITY
            // =========================================

            int totalItems = tasks.Count + habits.Count;

            int completedItems =
                tasks.Count(t => t.IsCompleted) +
                habits.Count(h => h.CompletedToday);

            ProductivityProgress =
                totalItems == 0
                ? 0
                : (double)completedItems / totalItems;

            // =========================================
            // RECENT NOTES
            // =========================================

            RecentNotes.Clear();

            foreach (var note in notes
                .OrderByDescending(n => n.CreatedAt)
                .Take(5))
            {
                RecentNotes.Add(note);
            }
        }
        catch (Exception ex)
        {
            await DialogService.DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}