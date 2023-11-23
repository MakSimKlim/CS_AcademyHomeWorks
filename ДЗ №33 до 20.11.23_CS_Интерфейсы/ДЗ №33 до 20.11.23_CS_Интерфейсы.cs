using System;
using System.Collections.Generic;
using static System.Console;

// Интерфейс, определяющий метод для построения части дома
interface IPart
{
    void Build();
}

// Классы, представляющие различные части дома и реализующие интерфейс IPart
class House : IPart
{
    public void Build()
    {
        WriteLine("Дом построен!");
    }
}

class Basement : IPart
{
    public void Build()
    {
        WriteLine("Фундамент построен!");
    }
}

class Walls : IPart
{
    public void Build()
    {
        WriteLine("Стены построены!");
    }
}

class Door : IPart
{
    public void Build()
    {
        WriteLine("Дверь построена!");
    }
}

class Window : IPart
{
    public void Build()
    {
        WriteLine("Окно построено!");
    }
}

class Roof : IPart
{
    public void Build()
    {
        WriteLine("Крыша построена!");
    }
}

// Интерфейс для работника (строителя или бригадира)
interface IWorker
{
    event EventHandler WorkEnded;// Событие завершения работы
    bool IsWorking { get; }      // Проверка, работает ли работник
    string Work(IPart part);     // Метод выполнения работы
}

// Реализация строителя
class Worker : IWorker
{
    public event EventHandler WorkEnded;
    public bool IsWorking { get; private set; }

    public string Work(IPart part)
    {
        if (part != null)
        {
            part.Build();
            IsWorking = true;
            //OnWorkEnded();
            return "Работа выполнена.";
        }
        else
        {
            return "Работа не может быть выполнена. Отсутствует часть дома для строительства.";
        }
    }

    //protected virtual void OnWorkEnded() => WorkEnded?.Invoke(this, EventArgs.Empty);
}

class TeamLeader : IWorker
{
    private List<IWorker> workers;

    public TeamLeader(List<IWorker> workers)
    {
        this.workers = workers;
        foreach (var worker in workers)
        {
            worker.WorkEnded += (sender, e) => CheckTeamCompletion();
        }
    }

    private void CheckTeamCompletion()
    {
        foreach (var worker in workers)
        {
            if (worker.IsWorking)
                return;
        }
        //OnWorkEnded();
    }

    public event EventHandler WorkEnded;

    public bool IsWorking
    {
        get
        {
            foreach (var worker in workers)
            {
                if (worker.IsWorking)
                    return true;
            }
            return false;
        }
    }

    public string Work(IPart part)
    {
        if (IsWorking)
        {
            return "Бригадир не строит, а формирует отчёт о выполненной работе.";
        }
        else
        {
            return "Работа не может быть выполнена. Вся работа уже завершена.";
        }
    }
}

class Team
{
    private List<IWorker> workers;

    public Team(List<IWorker> workers) => this.workers = workers;

    public void StartBuilding()
    {
        foreach (var worker in workers)
        {
            worker.WorkEnded += (sender, e) => CheckTeamCompletion();
        }

        foreach (var worker in workers)
        {
            worker.Work(new Basement());
        }
        foreach (var worker in workers)
        {
            worker.Work(new Walls());
        }
        foreach (var worker in workers)
        {
            worker.Work(new Door());
        }
        foreach (var worker in workers)
        {
            worker.Work(new Window());
        }
        foreach (var worker in workers)
        {
            worker.Work(new Roof());
        }
        foreach (var worker in workers)
        {
            worker.Work(new House());
        }
    }

    private void CheckTeamCompletion()
    {
        foreach (var worker in workers)
        {
            if (worker.IsWorking)
                return;
        }
        WriteLine("Бригада завершила работу!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Worker worker1 = new Worker();
        Worker worker2 = new Worker();
        Worker worker3 = new Worker();
        Worker worker4 = new Worker();
        //TeamLeader teamLeader = new TeamLeader(new List<IWorker> { worker1, worker2, worker3, worker4 });

        ////teamLeader.WorkEnded += (sender, e) => WriteLine("Бригада завершила работу!");

        //Team team = new Team(new List<IWorker> { worker1, worker2, worker3, worker4, teamLeader });

        //team.StartBuilding();

        //ReadLine();
    }
}
