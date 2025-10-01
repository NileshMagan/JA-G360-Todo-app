using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TodosController : ControllerBase
{
    private readonly ITodoRepository _repository;

    public TodosController(ITodoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
    public ActionResult<IReadOnlyCollection<TodoItem>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        if (page <= 0 || pageSize <= 0 || pageSize > 500)
        {
            return BadRequest("Invalid paging parameters");
        }

        var all = _repository.GetAll();
        var paged = all
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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


