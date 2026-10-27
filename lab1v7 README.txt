
using System;

class Phone
{
    // Поля класу
    private string brand;
    private string model;

    // Властивість рівня заряду батареї
    public int BatteryLevel { get; set; }

    // Конструктор без параметрів
    public Phone() : this("Невідомий", "Невідома модель", 100)
    {
    }

    // Конструктор із параметрами
    public Phone(string brand, string model, int batteryLevel)
    {
        this.brand = brand;
        this.model = model;
        BatteryLevel = batteryLevel;
    }

    // Метод здійснення дзвінка
    public void Call(string number)
    {
        Console.WriteLine($"{brand} {model} телефонує на номер {number}.");
    }

    // Метод виведення інформації
    public void ShowInfo()
    {
        Console.WriteLine($"Бренд: {brand}");
        Console.WriteLine($"Модель: {model}");
        Console.WriteLine($"Заряд батареї: {BatteryLevel}%");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Phone phone1 = new Phone();
        Phone phone2 = new Phone("Apple", "iPhone 12", 95);
        Phone phone3 = new Phone("Samsung", "Galaxy S24", 80);

        Console.WriteLine("ІНФОРМАЦІЯ ПРО ТЕЛЕФОНИ");
        Console.WriteLine("-----------------------");

        phone1.ShowInfo();
        phone2.ShowInfo();
        phone3.ShowInfo();

        Console.WriteLine("ДЕМОНСТРАЦІЯ ДЗВІНКА");
        Console.WriteLine("-----------------------");

        phone2.Call("+380501234567");
        phone3.Call("+380671234567");
    }
}
