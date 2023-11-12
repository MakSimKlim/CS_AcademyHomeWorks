using System;
using System.Collections.Generic;
namespace ДЗ__33_до_20._11._23_CS_Интерфейсы
{

    // Интерфейс для частей дома
    interface IPart
{
    string GetName();
}

// Интерфейс для рабочих
interface IWorker
{
    void Work(House house);
}

// Классы частей дома
class Basement : IPart
{
    public string GetName()
    {
        return "Basement";
    }
}

class Wall : IPart
{
    public string GetName()
    {
        return "Wall";
    }
}

class Door : IPart
{
    public string GetName()
    {
        return "Door";
    }
}

class Window : IPart
{
    public string GetName()
    {
        return "Window";
    }
}

class Roof : IPart
{
    public string GetName()
    {
        return "Roof";
    }
}

// Класс дома
class House
{
    private List<IPart> parts = new List<IPart>();

    public void AddPart(IPart part)
    {
        parts.Add(part);
    }

    public void Show()
    {
        Console.WriteLine("House is built with the following parts:");
        foreach (var part in parts)
        {
            Console.WriteLine(part.GetName());
        }
    }
}

// Класс строителя
class Worker : IWorker
{
    public void Work(House house)
    {
        house.AddPart(new Basement());
        house.AddPart(new Wall());
        house.AddPart(new Wall());
        house.AddPart(new Wall());
        house.AddPart(new Wall());
        house.AddPart(new Door());
        house.AddPart(new Window());
        house.AddPart(new Window());
        house.AddPart(new Roof());
    }
}

// Класс бригадира
class TeamLeader : IWorker
{
    public void Work(House house)
    {
        Console.WriteLine("Team Leader checks the progress:");
        house.Show();
    }
}

// Класс бригады строителей
class Team
{
    private List<IWorker> workers = new List<IWorker>();

    public void AddWorker(IWorker worker)
    {
        workers.Add(worker);
    }

    public void StartBuilding(House house)
    {
        Console.WriteLine("Construction of the house begins:");

        foreach (var worker in workers)
        {
            worker.Work(house);
        }

        Console.WriteLine("Construction of the house is complete.");
        house.Show();
    }
}

// Пример использования
class Program
{
        static void Main(string[] args)
        {
        House house = new House();
        Team team = new Team();

        // Добавляем строителей в бригаду
        team.AddWorker(new Worker());
        team.AddWorker(new Worker());
        team.AddWorker(new Worker());

        // Добавляем бригадира в бригаду
        team.AddWorker(new TeamLeader());

        // Начинаем строительство
        team.StartBuilding(house);
    }
}
}
