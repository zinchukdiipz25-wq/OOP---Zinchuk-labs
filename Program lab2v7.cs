
using System;

class Phone
{
    // Приватні поля
    private string _brand;
    private string _model;
    private int _batteryLevel;

    // Властивість Brand
    public string Brand
    {
        get { return _brand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Бренд не може бути порожнім!");

            _brand = value;
        }
    }

    // Властивість Model
    public string Model
    {
        get { return _model; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Модель не може бути порожньою!");

            _model = value;
        }
    }

    // Властивість BatteryLevel з валідацією
    public int BatteryLevel
    {
        get { return _batteryLevel; }
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Заряд має бути від 0 до 100!");

            _batteryLevel = value;
        }
    }

    // Конструктор без параметрів
    public Phone() : this("Generic", "Basic", 50)
    {
        Console.WriteLine("Створено телефон за замовчуванням.");
    }

    // Параметризований конструктор
    public Phone(string brand, string model, int batteryLevel)
    {
        Brand = brand;
        Model = model;
        BatteryLevel = batteryLevel;

        Console.WriteLine($"Створено телефон: {Brand} {Model}");
    }

    // Метод дзвінка
    public void Call(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            Console.WriteLine("Помилка: номер не вказано!");
            return;
        }

        if (BatteryLevel == 0)
        {
            Console.WriteLine($"{Brand}: недостатньо заряду!");
            return;
        }

        Console.WriteLine($"{Brand} {Model} телефонує на {number}.");

        BatteryLevel = Math.Max(0, BatteryLevel - 10);

        Console.WriteLine($"Залишок заряду: {BatteryLevel}%");
    }

    // Фіналізатор
    ~Phone()
    {
        Console.WriteLine($"Об'єкт {Brand} {Model} знищено.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Телефон 1 ===");
        Phone phone1 = new Phone();

        Console.WriteLine();

        Console.WriteLine("=== Телефон 2 ===");
        Phone phone2 = new Phone("Apple", "iPhone 12", 80);

        Console.WriteLine();

        Console.WriteLine("=== Телефон 3 ===");
        Phone phone3 = new Phone("Samsung", "Galaxy S24", 20);

        Console.WriteLine();

        phone1.Call("0671234567");
        Console.WriteLine();

        phone2.Call("0509876543");
        Console.WriteLine();

        phone3.Call("0631112233");
        Console.WriteLine();

        Console.WriteLine("=== Примусове збирання сміття ===");

        // Навчальна демонстрація фіналізатора
        phone1 = null;
        phone2 = null;
        phone3 = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Програму завершено.");
    }
}
