using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Tests;

public sealed class TodosControllerTests
{
    [Fact]
    public void Get_ShouldReturnOkWithItems()
    {
        var items = new[] { new TodoItem { Title = "X" } };
        var repo = new Mock<ITodoRepository>();
        repo.Setup(r => r.GetAll()).Returns(items);
        var controller = new TodosController(repo.Object);

        var result = controller.Get();

        result.Result.Should().BeOfType<OkObjectResult>();
        (result.Result as OkObjectResult)!.Value.Should().BeSameAs(items);
    }

    [Fact]
    public void Post_ShouldValidateAndReturnCreated()
    {
        var toReturn = new TodoItem { Title = "A" };
        var repo = new Mock<ITodoRepository>();
        repo.Setup(r => r.Add("A")).Returns(toReturn);
        var controller = new TodosController(repo.Object);

        var result = controller.Post(new TodosController.CreateTodoRequest("A"));

        result.Result.Should().BeOfType<CreatedAtActionResult>();
        (result.Result as CreatedAtActionResult)!.Value.Should().BeSameAs(toReturn);
    }

    [Fact]
    public void Post_ShouldReturnBadRequest_WhenInvalid()
    {
        var controller = new TodosController(new Mock<ITodoRepository>().Object);

        var result = controller.Post(new TodosController.CreateTodoRequest("   "));

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void Delete_ShouldReturnNoContentOrNotFound()
    {
        var id = Guid.NewGuid();
        var repo = new Mock<ITodoRepository>();
        repo.Setup(r => r.Delete(id)).Returns(true);
        var controller = new TodosController(repo.Object);

        var ok = controller.Delete(id);
        ok.Should().BeOfType<NoContentResult>();

        repo.Setup(r => r.Delete(id)).Returns(false);
        var notFound = controller.Delete(id);
        notFound.Should().BeOfType<NotFoundResult>();
    }
}


