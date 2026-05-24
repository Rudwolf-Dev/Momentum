namespace Momentum.ViewModels;

public partial class HabitsViewModel(
    HabitService habitService,
    IDialogService dialogService,
    INavigationService navigationService)
    : BaseViewModel(
        dialogService,
        navigationService,
        "Habits")
{
    private readonly HabitService _habitService = habitService;

    // =========================================
    // COLLECTION
    // =========================================

    public ObservableCollection<HabitItem> Habits { get; set; }
        = new();

    // =========================================
    // PROPERTIES
    // =========================================

    [ObservableProperty]
    private string habitName = string.Empty;

    // =========================================
    // COMMANDS
    // =========================================

    [RelayCommand]
    private async Task LoadHabitsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Habits.Clear();

            var habits = await _habitService.GetHabitsAsync();

            foreach (var habit in habits)
            {
                Habits.Add(habit);
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

    [RelayCommand]
    private async Task AddHabitAsync()
    {
        if (string.IsNullOrWhiteSpace(HabitName))
            return;

        try
        {
            await _habitService.AddHabitAsync(HabitName);

            HabitName = string.Empty;

            await LoadHabitsAsync();
        }
        catch (Exception ex)
        {
            await DialogService.DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }

    [RelayCommand]
    private async Task CompleteHabitAsync(HabitItem habit)
    {
        if (habit == null)
            return;

        try
        {
            await _habitService.CompleteHabitAsync(habit);

            await LoadHabitsAsync();
        }
        catch (Exception ex)
        {
            await DialogService.DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }

    [RelayCommand]
    private async Task DeleteHabitAsync(HabitItem habit)
    {
        if (habit == null)
            return;

        try
        {
            await _habitService.DeleteHabitAsync(habit);

            await LoadHabitsAsync();
        }
        catch (Exception ex)
        {
            await DialogService.DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }
}