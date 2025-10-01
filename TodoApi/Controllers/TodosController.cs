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
    public ActionResult<IReadOnlyCollection<TodoItem>> Get()
    {
        return Ok(_repository.GetAll());
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


