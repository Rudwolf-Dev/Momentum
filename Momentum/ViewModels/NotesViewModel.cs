namespace Momentum.ViewModels;

public partial class NotesViewModel(
    NoteService noteService,
    IDialogService dialogService,
    INavigationService navigationService)
    : BaseViewModel(
        dialogService,
        navigationService,
        "Notes")
{
    private readonly NoteService _noteService = noteService;

    // =========================================
    // COLLECTION
    // =========================================

    public ObservableCollection<NoteItem> Notes { get; set; }
        = new();

    // =========================================
    // PROPERTIES
    // =========================================

    [ObservableProperty]
    private string noteTitle = string.Empty;

    [ObservableProperty]
    private string noteContent = string.Empty;

    // =========================================
    // COMMANDS
    // =========================================

    [RelayCommand]
    private async Task LoadNotesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Notes.Clear();

            var notes = await _noteService.GetNotesAsync();

            foreach (var note in notes)
            {
                Notes.Add(note);
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
    private async Task AddNoteAsync()
    {
        if (string.IsNullOrWhiteSpace(NoteTitle))
            return;

        try
        {
            await _noteService.AddNoteAsync(
                NoteTitle,
                NoteContent);

            NoteTitle = string.Empty;
            NoteContent = string.Empty;

            await LoadNotesAsync();
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
    private async Task DeleteNoteAsync(NoteItem note)
    {
        if (note == null)
            return;

        try
        {
            await _noteService.DeleteNoteAsync(note);

            await LoadNotesAsync();
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