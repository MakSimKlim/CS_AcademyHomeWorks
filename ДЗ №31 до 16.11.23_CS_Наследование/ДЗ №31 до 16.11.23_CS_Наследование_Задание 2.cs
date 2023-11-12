using System;

class Device
{
    protected string Name;

    public Device(string name)
    {
        Name = name;
    }

    public virtual void Sound()
    {
        Console.WriteLine(GetSoundDescription());
    }
    public virtual string GetSoundDescription()
    {
        return $"{Name} производит звук.";
    }

    public virtual void Desc()
    {
        Console.WriteLine($"Описание {Name}: [Описание по-умолчанию]");
    }

    public override string ToString()
    {
        return $"Устройство: {Name}";
    }
}

class Kettle : Device
{
    public Kettle(string name, string characteristic)
        : base(name)
    {
        // Дополнительные характеристики для чайника
        Characteristic = characteristic;
    }

    public string Characteristic { get; set; }

    public override void Desc()
    {
        Console.WriteLine($"Описание {Name}: {Characteristic}");
    }
   public override string GetSoundDescription()
    {
        return $"{Name} производит звук кипящей воды.";
    }
}

class Microwave : Device
{
    public Microwave(string name, string feature)
        : base(name)
    {
        // Дополнительные характеристики для микроволновки
        Feature = feature;
    }

    public string Feature { get; set; }

    public override void Desc()
    {
        Console.WriteLine($"Описание {Name}: {Feature}");
    }
    public override void Sound()
    {
        Console.WriteLine($"{Name} производит звук колокольчика.");
    }
}

class Car : Device
{
    public Car(string name, string model)
        : base(name)
    {
        // Дополнительные характеристики для автомобиля
        Model = model;
    }

    public string Model { get; set; }

    public override void Desc()
    {
        Console.WriteLine($"Описание {Name}: {Model}");
    }
    public override void Sound()
    {
        Console.WriteLine($"{Name} производит звук двигателя.");
    }
}

class Ship : Device
{
    public Ship(string name, string type)
        : base(name)
    {
        // Дополнительные характеристики для парохода
        Type = type;
    }

    public string Type { get; set; }

    public override void Desc()
    {
        Console.WriteLine($"Описание {Name}: {Type}");
    }
    public override void Sound()
    {
        Console.WriteLine($"{Name} производит звук гудка.");
    }
}

class Program
{
    static void Main()
    {
        Kettle kettle = new Kettle("Электрический чайник", "Кипятит воду");
        Microwave microwave = new Microwave("Микроволновка", "Разогревает еду");
        Car car = new Car("Машина", "Перемещается по дорогам");
        Ship ship = new Ship("Пароход", "Перевозит грузы по воде");

        Console.WriteLine(kettle);
        kettle.Sound();
        kettle.Desc();

        Console.WriteLine();

        Console.WriteLine(microwave);
        microwave.Sound();
        microwave.Desc();

        Console.WriteLine();

        Console.WriteLine(car);
        car.Sound();
        car.Desc();

        Console.WriteLine();

        Console.WriteLine(ship);
        ship.Sound();
        ship.Desc();
    }
}
