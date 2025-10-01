using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Common;
using TodoApi.Features.Todos;
using TodoApi.Features.Todos.GetTodos;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<ActionResult<ApiResponse<GetTodosResponse>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        var query = new GetTodosQuery(page, pageSize);
        var result = await _mediator.Send(query);
        
        Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
        
        return ApiResponse<GetTodosResponse>.Ok(result);
            .ToArray();

        Response.Headers["X-Total-Count"] = all.Count.ToString();
        return Ok(paged);
    }

    public sealed record CreateTodoRequest(string Title);

    [HttpPost]
    public ActionResult<TodoItem> Post([FromBody] CreateTodoRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required");
        }

        var created = _repository.Add(request.Title);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var removed = _repository.Delete(id);
        return removed ? NoContent() : NotFound();
    }
}


