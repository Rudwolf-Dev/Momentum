namespace Momentum.Services;

public class NoteService
{
    private readonly DatabaseService _databaseService;

    public NoteService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<List<NoteItem>> GetNotesAsync()
    {
        return await _databaseService.GetNotesAsync();
    }

    public async Task AddNoteAsync(
        string title,
        string content)
    {
        var note = new NoteItem
        {
            Title = title,
            Content = content,
            CreatedAt = DateTime.Now
        };

        await _databaseService.SaveNoteAsync(note);
    }

    public async Task DeleteNoteAsync(NoteItem note)
    {
        await _databaseService.DeleteNoteAsync(note);
    }
}