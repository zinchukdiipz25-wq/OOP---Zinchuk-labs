using lab12v7;

var repo = new InMemoryRepository<Department>();

// Додавання
repo.Add(new Department { Name = "IT", Location = "Корпус 1, к. 101" });
repo.Add(new Department { Name = "Бухгалтерія", Location = "Корпус 2, к. 205" });
repo.Add(new Department { Name = "HR", Location = "Корпус 1, к. 110" });

Console.WriteLine("Після додавання:");
foreach (var d in repo.GetAll()) Console.WriteLine("  " + d);

// Отримання за Id
var found = repo.GetById(2);
Console.WriteLine($"\nGetById(2): {found}");
Console.WriteLine($"GetById(99): {(repo.GetById(99) is null ? "не знайдено (null)" : "знайдено")}");

// Оновлення
repo.Update(new Department { Id = 2, Name = "Бухгалтерія", Location = "Корпус 3, к. 300" });
Console.WriteLine($"\nПісля Update(2): {repo.GetById(2)}");

try
{
    repo.Update(new Department { Id = 99, Name = "X", Location = "Y" });
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine("Update неіснуючого: " + ex.Message);
}

// Видалення
repo.Delete(1);
Console.WriteLine("\nПісля Delete(1):");
foreach (var d in repo.GetAll()) Console.WriteLine("  " + d);

try
{
    repo.Delete(99);
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine("Delete неіснуючого: " + ex.Message);
}