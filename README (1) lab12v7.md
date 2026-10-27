# Лабораторна робота №12 — варіант 7

**Виконав:** Зінчук Д. Ю., група ІПЗ 3/1
**Дисципліна:** Об'єктно-орієнтоване програмування
**Сутність (варіант 7):** `Department` — `Name` (string), `Location` (string)

## Структура

```
OOP-Zinchuk/
├── lab12v7/                      # основний консольний проєкт
│   ├── IEntity.cs                # інтерфейс сутності: int Id { get; set; }
│   ├── IRepository.cs            # IRepository<T> where T : IEntity
│   ├── InMemoryRepository.cs     # реалізація на List<T>
│   ├── Department.cs             # сутність варіанта 7
│   └── Program.cs                # демонстрація CRUD у Main
└── lab12v7.Tests/                # xUnit-тести
    └── InMemoryRepositoryTests.cs
```

## Опис

- **IEntity** — містить лише властивість `int Id { get; set; }`.
- **IRepository&lt;T&gt;** — методи `GetById`, `GetAll`, `Add`, `Update`, `Delete`; обмеження `where T : IEntity`.
- **InMemoryRepository&lt;T&gt;** — зберігає дані в `List<T>`. Якщо `Id == 0`, при `Add` генерується автоматично.
  - `GetById` для неіснуючого Id повертає `null`.
  - `Update` / `Delete` для неіснуючого Id кидають `KeyNotFoundException`.
  - `Add` з уже зайнятим Id кидає `InvalidOperationException`.
- **Department** — реалізує `IEntity`, властивості `Name`, `Location`.

## Команди

```
dotnet new console -o OOP-Zinchuk/lab12v7
dotnet new xunit   -o OOP-Zinchuk/lab12v7.Tests
cd OOP-Zinchuk/lab12v7.Tests
dotnet add reference ../lab12v7/lab12v7.csproj
cd ..

dotnet run  --project lab12v7
dotnet test lab12v7.Tests
```
