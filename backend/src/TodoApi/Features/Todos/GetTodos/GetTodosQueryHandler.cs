using AutoMapper;
using MediatR;
using TodoApi.Repositories;

namespace TodoApi.Features.Todos.GetTodos;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, GetTodosResponse>
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<GetTodosResponse> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var all = _repository.GetAll();
        var paged = all
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return Task.FromResult(new GetTodosResponse
        {
            Items = _mapper.Map<List<TodoItemDto>>(paged),
            TotalCount = all.Count
        });
    }
}
