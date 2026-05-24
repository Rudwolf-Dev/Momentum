namespace Momentum.Models;

public class HabitItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool CompletedToday { get; set; }

    public int Streak { get; set; }

    public DateTime LastCompletedDate { get; set; }
}