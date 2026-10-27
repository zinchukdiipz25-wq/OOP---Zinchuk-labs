# Лабораторна робота №10 — варіант 7

**Виконав:** Зінчук Д. Ю., група ІПЗ 3/1
**Дисципліна:** Об'єктно-орієнтоване програмування
**Тема:** інтерфейси та абстрактні класи, поліморфізм
**Варіант 7:** `INotifier` та `Authenticator`

## Завдання

- Інтерфейс `INotifier`: `void SendNotification(string message)`.
- Реалізації інтерфейсу: `EmailNotifier`, `SmsNotifier`.
- Абстрактний клас `Authenticator`: абстрактний метод `bool Authenticate(string username, string password)` та конкретний метод `void LogAttempt(string username, bool success)`.
- Похідні від `Authenticator`: `DatabaseAuthenticator`, `LdapAuthenticator`.
- У `Main` продемонструвати поліморфну поведінку через колекції інтерфейсного й абстрактного типів.

## Структура проєкту

```
lab10v7/
├── INotifier.cs
├── EmailNotifier.cs
├── SmsNotifier.cs
├── Authenticator.cs
├── DatabaseAuthenticator.cs
├── LdapAuthenticator.cs
├── Program.cs
└── lab10v7.csproj
```

## Як запустити

```
dotnet new console -o lab10v7
dotnet run --project lab10v7
```

Після створення проєкту додайте файли з кодом нижче, а вміст `Program.cs` замініть.

## Код

### INotifier.cs

```csharp
namespace lab10v7;

public interface INotifier
{
    void SendNotification(string message);
}
```

### EmailNotifier.cs

```csharp
namespace lab10v7;

public class EmailNotifier : INotifier
{
    public string Address { get; }

    public EmailNotifier(string address)
    {
        Address = address;
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"[Email -> {Address}] {message}");
    }
}
```

### SmsNotifier.cs

```csharp
namespace lab10v7;

public class SmsNotifier : INotifier
{
    public string PhoneNumber { get; }

    public SmsNotifier(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void SendNotification(string message)
    {
        Console.WriteLine($"[SMS -> {PhoneNumber}] {message}");
    }
}
```

### Authenticator.cs

```csharp
namespace lab10v7;

public abstract class Authenticator
{
    // Абстрактний метод: кожен нащадок перевіряє дані по-своєму
    public abstract bool Authenticate(string username, string password);

    // Конкретний метод: спільна логіка для всіх нащадків
    public void LogAttempt(string username, bool success)
    {
        string result = success ? "успішно" : "невдало";
        Console.WriteLine($"  [{DateTime.Now:HH:mm:ss}] Спроба входу '{username}': {result}");
    }
}
```

### DatabaseAuthenticator.cs

```csharp
namespace lab10v7;

public class DatabaseAuthenticator : Authenticator
{
    // Імітація таблиці користувачів у базі даних
    private readonly Dictionary<string, string> _users = new()
    {
        ["admin"] = "admin123",
        ["dmytro"] = "qwerty"
    };

    public override bool Authenticate(string username, string password)
    {
        bool success = _users.TryGetValue(username, out var storedPassword)
                       && storedPassword == password;

        LogAttempt(username, success);
        return success;
    }
}
```

### LdapAuthenticator.cs

```csharp
namespace lab10v7;

public class LdapAuthenticator : Authenticator
{
    // Імітація каталогу користувачів LDAP
    private readonly Dictionary<string, string> _directory = new()
    {
        ["ivan"] = "ldap2026",
        ["olena"] = "ldap2027"
    };

    public override bool Authenticate(string username, string password)
    {
        bool success = _directory.TryGetValue(username, out var storedPassword)
                       && storedPassword == password;

        LogAttempt(username, success);
        return success;
    }
}
```

### Program.cs

```csharp
using lab10v7;

// ---------- Поліморфізм через інтерфейс INotifier ----------
Console.WriteLine("=== Сповіщення (INotifier) ===");

List<INotifier> notifiers = new()
{
    new EmailNotifier("student@example.com"),
    new SmsNotifier("+380501234567")
};

foreach (INotifier notifier in notifiers)
{
    notifier.SendNotification("Вітаємо! Ваш акаунт створено.");
}

// ---------- Поліморфізм через абстрактний клас Authenticator ----------
Console.WriteLine("\n=== Автентифікація (Authenticator) ===");

List<Authenticator> authenticators = new()
{
    new DatabaseAuthenticator(),
    new LdapAuthenticator()
};

foreach (Authenticator auth in authenticators)
{
    Console.WriteLine($"\n{auth.GetType().Name}:");

    bool r1 = auth.Authenticate("admin", "admin123");
    Console.WriteLine($"  admin/admin123 -> {r1}");

    bool r2 = auth.Authenticate("ivan", "ldap2026");
    Console.WriteLine($"  ivan/ldap2026  -> {r2}");

    bool r3 = auth.Authenticate("admin", "wrong");
    Console.WriteLine($"  admin/wrong    -> {r3}");
}
```

## Що виводить програма

1. Два сповіщення (Email і SMS), надіслані однаковим викликом `SendNotification` через колекцію `List<INotifier>`.
2. Результати автентифікації для двох реалізацій через колекцію `List<Authenticator>`:
   - `admin / admin123` проходить лише в `DatabaseAuthenticator`;
   - `ivan / ldap2026` проходить лише в `LdapAuthenticator`;
   - `admin / wrong` не проходить в обох.
3. Для кожної спроби спільний метод `LogAttempt` виводить час і результат.

## Висновки

Інтерфейс `INotifier` задає контракт, який по-різному виконують `EmailNotifier` і `SmsNotifier`, а код у `Main` працює з ними однаково. Абстрактний клас `Authenticator` поєднує спільну логіку (`LogAttempt`) з методом `Authenticate`, який кожен нащадок реалізує по-своєму. Це і є поліморфізм: один виклик через базовий тип дає різну поведінку залежно від реального класу об'єкта.
