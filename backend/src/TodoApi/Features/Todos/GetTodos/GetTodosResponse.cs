using TodoApi.Features.Todos;

namespace TodoApi.Features.Todos.GetTodos;

public class GetTodosResponse
{
    public IReadOnlyCollection<TodoItemDto> Items { get; set; } = null!;
    public int TotalCount { get; set; }
}
