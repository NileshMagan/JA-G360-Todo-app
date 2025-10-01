using System.Collections.Concurrent;
using TodoApi.Models;

namespace TodoApi.Repositories;

public sealed class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _items = new();

    public IReadOnlyCollection<TodoItem> GetAll()
    {
        return _items.Values
            .OrderBy(i => i.Title, StringComparer.CurrentCultureIgnoreCase)
            .ToArray();
    }

    public TodoItem Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        var item = new TodoItem
        {
            Title = title.Trim(),
            IsCompleted = false
        };

        _items[item.Id] = item;
        return item;
    }

    public bool Delete(Guid id)
    {
        return _items.TryRemove(id, out _);
    }
}


