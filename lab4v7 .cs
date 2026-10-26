using System;

class Person
{
    // Приватні поля
    private string _name;
    private int _age;

    // Публічні властивості з валідацією
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ім'я не може бути порожнім!");

            _name = value;
        }
    }

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0 || value > 120)
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Вік має бути від 0 до 120!");

            _age = value;
        }
    }

    // Конструктор
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Віртуальний метод: може бути перевизначений у похідних класах
    public virtual void Greet()
    {
        Console.WriteLine($"Привіт, мене звати {Name}. Мій вік: {Age}.");
    }

    // Звичайний метод (не віртуальний)
    public string GetRole()
    {
        return "Особа";
    }
}

class Student : Person
{
    private string _studentId;

    public string StudentId
    {
        get { return _studentId; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер студентського не може бути порожнім!");

            _studentId = value;
        }
    }

    // Виклик конструктора базового класу
    public Student(string name, int age, string studentId) : base(name, age)
    {
        StudentId = studentId;
    }

    // Перевизначення віртуального методу
    public override void Greet()
    {
        Console.WriteLine($"Привіт, я студент {Name}, студентський квиток {StudentId}.");
    }

    // Власний унікальний метод
    public void Study()
    {
        Console.WriteLine($"{Name} вивчає об'єктно-орієнтоване програмування.");
    }

    // Приховування методу базового класу (не override!)
    public new string GetRole()
    {
        return "Студент";
    }
}

class Professor : Person
{
    private string _department;

    public string Department
    {
        get { return _department; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Кафедра не може бути порожньою!");

            _department = value;
        }
    }

    // Виклик конструктора базового класу
    public Professor(string name, int age, string department) : base(name, age)
    {
        Department = department;
    }

    // Перевизначення віртуального методу
    public override void Greet()
    {
        Console.WriteLine($"Доброго дня, я професор {Name}, кафедра: {Department}.");
    }

    // Власний унікальний метод
    public void Teach()
    {
        Console.WriteLine($"{Name} читає лекцію на кафедрі: {Department}.");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Person person = new Person("Олег", 40);
        Student student = new Student("Марія", 19, "ST-2025-07");
        Professor professor = new Professor("Іван Петренко", 52, "Інформаційні технології");

        Console.WriteLine("=== Поліморфізм (override) ===");
        Person[] people = { person, student, professor };
        foreach (Person p in people)
        {
            p.Greet();
        }

        Console.WriteLine();

        Console.WriteLine("=== Власні методи похідних класів ===");
        student.Study();
        professor.Teach();

        Console.WriteLine();

        Console.WriteLine("=== Різниця між override та new ===");
        Person studentAsPerson = student;

        Console.WriteLine("Greet() через посилання типу Person (override):");
        studentAsPerson.Greet();

        Console.WriteLine($"GetRole() через посилання типу Person (new): {studentAsPerson.GetRole()}");
        Console.WriteLine($"GetRole() через посилання типу Student (new): {student.GetRole()}");
        Console.WriteLine($"GetRole() у Professor (успадкований): {professor.GetRole()}");
    }
}
