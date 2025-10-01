namespace TodoApi.Models;

public sealed class TodoItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; init; }
    public bool IsCompleted { get; init; }
}


