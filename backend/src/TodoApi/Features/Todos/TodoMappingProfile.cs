using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Features.Todos;

public class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
    }
}
