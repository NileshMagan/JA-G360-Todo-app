using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    IReadOnlyCollection<TodoItem> GetAll();
    TodoItem Add(string title);
    bool Delete(Guid id);
}


