using FluentValidation;
using MediatR;

namespace TodoApi.Features.Todos.GetTodos;

public record GetTodosQuery(int Page = 1, int PageSize = 50) : IRequest<GetTodosResponse>;

public class GetTodosQueryValidator : AbstractValidator<GetTodosQuery>
{
    public GetTodosQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0")
            .LessThanOrEqualTo(500).WithMessage("PageSize must not exceed 500");
    }
}
