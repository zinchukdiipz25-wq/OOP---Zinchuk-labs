using lab12v7;

namespace lab12v7.Tests;

public class InMemoryRepositoryTests
{
    private static Department Make(string name = "IT", string location = "Корпус 1") =>
        new() { Name = name, Location = location };

    [Fact]
    public void Add_NewItem_IsStoredAndGetsId()
    {
        var repo = new InMemoryRepository<Department>();
        var dep = Make();

        repo.Add(dep);

        Assert.NotEqual(0, dep.Id);
        Assert.Single(repo.GetAll());
        Assert.Same(dep, repo.GetById(dep.Id));
    }

    [Fact]
    public void Add_TwoItems_GetDifferentIds()
    {
        var repo = new InMemoryRepository<Department>();
        var a = Make("A");
        var b = Make("B");

        repo.Add(a);
        repo.Add(b);

        Assert.NotEqual(a.Id, b.Id);
    }

    [Fact]
    public void Add_DuplicateId_Throws()
    {
        var repo = new InMemoryRepository<Department>();
        repo.Add(new Department { Id = 5, Name = "A", Location = "L" });

        Assert.Throws<InvalidOperationException>(() =>
            repo.Add(new Department { Id = 5, Name = "B", Location = "L" }));
    }

    [Fact]
    public void Add_Null_Throws()
    {
        var repo = new InMemoryRepository<Department>();

        Assert.Throws<ArgumentNullException>(() => repo.Add(null!));
    }

    [Fact]
    public void GetById_Existing_ReturnsItem()
    {
        var repo = new InMemoryRepository<Department>();
        var dep = Make("HR", "Корпус 2");
        repo.Add(dep);

        var result = repo.GetById(dep.Id);

        Assert.NotNull(result);
        Assert.Equal("HR", result!.Name);
        Assert.Equal("Корпус 2", result.Location);
    }

    [Fact]
    public void GetById_NonExistent_ReturnsNull()
    {
        var repo = new InMemoryRepository<Department>();
        repo.Add(Make());

        Assert.Null(repo.GetById(999));
    }

    [Fact]
    public void GetAll_EmptyRepository_ReturnsEmpty()
    {
        var repo = new InMemoryRepository<Department>();

        Assert.Empty(repo.GetAll());
    }

    [Fact]
    public void GetAll_ReturnsAllItems()
    {
        var repo = new InMemoryRepository<Department>();
        repo.Add(Make("A"));
        repo.Add(Make("B"));
        repo.Add(Make("C"));

        var all = repo.GetAll().ToList();

        Assert.Equal(3, all.Count);
        Assert.Contains(all, d => d.Name == "A");
        Assert.Contains(all, d => d.Name == "B");
        Assert.Contains(all, d => d.Name == "C");
    }

    [Fact]
    public void Update_Existing_ChangesItem()
    {
        var repo = new InMemoryRepository<Department>();
        var dep = Make("Old", "OldLoc");
        repo.Add(dep);

        repo.Update(new Department { Id = dep.Id, Name = "New", Location = "NewLoc" });

        var result = repo.GetById(dep.Id);
        Assert.Equal("New", result!.Name);
        Assert.Equal("NewLoc", result.Location);
        Assert.Single(repo.GetAll());
    }

    [Fact]
    public void Update_NonExistent_Throws()
    {
        var repo = new InMemoryRepository<Department>();

        Assert.Throws<KeyNotFoundException>(() =>
            repo.Update(new Department { Id = 42, Name = "X", Location = "Y" }));
    }

    [Fact]
    public void Delete_Existing_RemovesItem()
    {
        var repo = new InMemoryRepository<Department>();
        var dep = Make();
        repo.Add(dep);

        repo.Delete(dep.Id);

        Assert.Null(repo.GetById(dep.Id));
        Assert.Empty(repo.GetAll());
    }

    [Fact]
    public void Delete_NonExistent_Throws()
    {
        var repo = new InMemoryRepository<Department>();

        Assert.Throws<KeyNotFoundException>(() => repo.Delete(42));
    }
}