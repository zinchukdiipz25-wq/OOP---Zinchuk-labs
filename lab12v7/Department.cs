namespace lab12v7;

/// <summary>
/// Сутність "Відділ" (варіант 7).
/// </summary>
public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public override string ToString() => $"[{Id}] {Name} ({Location})";
}
