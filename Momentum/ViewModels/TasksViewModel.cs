namespace Momentum.ViewModels;

public partial class TasksViewModel(
    TaskService taskService,
    IDialogService dialogService,
    INavigationService navigationService)
    : BaseViewModel(
        dialogService,
        navigationService,
        "Tasks")
{
    private readonly TaskService _taskService = taskService;

    // =========================================
    // COLLECTION
    // =========================================

    public ObservableCollection<TaskItem> Tasks { get; set; }
        = new();

    // =========================================
    // PROPERTIES
    // =========================================

    [ObservableProperty]
    private string taskTitle = string.Empty;

    // =========================================
    // COMMANDS
    // =========================================

    [RelayCommand]
    private async Task LoadTasksAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Tasks.Clear();

            var tasks = await _taskService.GetTasksAsync();

            foreach (var task in tasks)
            {
                Tasks.Add(task);
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
    private async Task AddTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(TaskTitle))
            return;

        try
        {
            await _taskService.AddTaskAsync(TaskTitle);

            TaskTitle = string.Empty;

            await LoadTasksAsync();
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
    private async Task DeleteTaskAsync(TaskItem task)
    {
        if (task == null)
            return;

        try
        {
            await _taskService.DeleteTaskAsync(task);

            await LoadTasksAsync();
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
    private async Task ToggleTaskAsync(TaskItem task)
    {
        if (task == null)
            return;

        try
        {
            await _taskService.ToggleTaskAsync(task);

            await LoadTasksAsync();
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