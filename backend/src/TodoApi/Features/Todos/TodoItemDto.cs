namespace TodoApi.Features.Todos;

public class TodoItemDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public bool IsCompleted { get; set; }
}
