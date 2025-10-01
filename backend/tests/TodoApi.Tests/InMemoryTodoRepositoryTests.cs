using FluentAssertions;
using TodoApi.Repositories;

namespace TodoApi.Tests;

public sealed class InMemoryTodoRepositoryTests
{
    [Fact]
    public void Add_ShouldStoreAndReturnItem()
    {
        var repo = new InMemoryTodoRepository();

        var created = repo.Add("Test");

        created.Title.Should().Be("Test");
        repo.GetAll().Should().ContainSingle(i => i.Id == created.Id);
    }

    [Fact]
    public void Add_ShouldTrimAndRejectEmpty()
    {
        var repo = new InMemoryTodoRepository();

        var created = repo.Add("  Hello  ");
        created.Title.Should().Be("Hello");

        var act = () => repo.Add("   ");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Delete_ShouldRemove_WhenPresent()
    {
        var repo = new InMemoryTodoRepository();
        var item = repo.Add("A");

        var removed = repo.Delete(item.Id);

        removed.Should().BeTrue();
        repo.GetAll().Should().BeEmpty();
    }
}


