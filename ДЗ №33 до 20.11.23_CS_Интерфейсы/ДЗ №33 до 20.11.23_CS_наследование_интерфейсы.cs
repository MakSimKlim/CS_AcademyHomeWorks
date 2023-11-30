using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Console;


namespace House
{

    interface IWorker
    {
        string Name { get; }
    }
    interface IPart
    {
        void Do(House house);
    }

    class Basement : IPart
    {
        public void Do(House house)
        {
            house.basement = new Basement();
        }
    }

    class Walls : IPart
    {
        public void Do(House house)
        {
            house.walls.Add(new Walls());
        }
    }

    class Door : IPart
    {
        public void Do(House house)
        {
            house.door = new Door();
        }
    }

    class Window : IPart
    {
        public void Do(House house)
        {
            house.window.Add(new Window());
        }
    }

    class Roof : IPart
    {
        public void Do(House house)
        {
            house.roof = new Roof();
        }
    }

    class House
    {
        public Basement basement;
        public List<Walls> walls;
        public List<Window> window;
        public Door door;
        public Roof roof;

        public void Paint(TeamLeader t)
        {
            if (t.report.Count == 11)
            {

                string domik = @"
                           (   )
                          (    )
                           (    )
                          (    )
                            )  )
                           (  (                  /\
                            (_)                 /  \  /\
                    ________[_]________      /\/    \/  \
           /\      /\        ______    \    /   /\/\  /\/\
          /  \    //_\       \    /\    \  /\/\/    \/    \
   /\    / /\/\  //___\       \__/  \    \/
  /  \  /\/    \//_____\       \ |[]|     \
 /\/\/\/       //_______\       \|__|      \
/      \      /XXXXXXXXXX\                  \
        \    /_I_II  I__I_\__________________\
               I_I|  I__I_____[]_|_[]_____I
               I_II  I__I_____[]_|_[]_____I
               I II__I  I     XXXXXXX     I
            ~~~~~'   '~~~~~~~~~~~~~~~~~~~~~~~~";

                Console.WriteLine(domik);
            }
            else WriteLine("Дом еще не построен");
        }
    }

    class Team : IWorker
    {
        public TeamLeader t;
        public List<Worker> w;
        public string Name { get => "Строительная Компания: House Ingeneering\n"; }

        public Team()
        {
            t = new TeamLeader("Анатолий");
            w = new List<Worker> { new Worker("Андрей"), new Worker("Петр"), new Worker("Иван"), new Worker("Сергей") };
        }


    }

    class Worker : IWorker
    {
        string Name { get; set; }

        string IWorker.Name => Name;

        public Worker(string name)
        { Name = name; }

        public void Build(House house, TeamLeader t)
        {
            if (house.basement == null)
            {
                Basement basement = new Basement();
                basement.Do(house);
                t.report.Add($"Рабочий {Name} построил фундамент!");
            }
            else if (house.walls == null || house.walls.Count < 4)
            {
                if (house.walls == null) house.walls = new List<Walls>();
                Walls wall = new Walls();
                wall.Do(house);
                t.report.Add($"Рабочий {Name} построил стену №{house.walls.Count}!");
            }
            else if (house.door == null)
            {
                Door door = new Door();
                door.Do(house);
                t.report.Add($"Рабочий {Name} установил дверь!");

            }

            else if (house.window == null || house.window.Count < 4)
            {
                if (house.window == null) house.window = new List<Window>();
                Window window = new Window();
                window.Do(house);
                t.report.Add($"Рабочий {Name} установил окно №{house.window.Count}!");

            }

            else if (house.roof == null)
            {
                Roof roof = new Roof();
                roof.Do(house);
                t.report.Add($"Рабочий {Name} построил крышу!");

            }

        }



    }

    class TeamLeader : IWorker
    {
        string Name { get; set; }
        public List<string> report = new List<string>();
        string IWorker.Name => Name;

        public TeamLeader(string name)
        { Name = name; }
        public void Report()
        {
            double d = (report.Count / 11.0) * 100;// формула для расчета процента выполнения
            // 11 - кол-во видов работ (1 фунд + 4 стены + 1 дверь + 4 окна + 1 крыша)
            WriteLine($"Бригадир {Name} сообщает: {(int)d} % работы выполнено!");
        }
    }

    class Program
    {
        static void Main()
        {
            // Создаем объекты House и Team
            House house = new House();
            Team team = new Team();

            // Выводим наименование строительной компании из класса Team
            WriteLine(team.Name);

            // Создаем объект Random для генерации случайных чисел
            Random r = new Random();

            // Генерируем случайное количество работ от 1 до 11
            int numberOfTasks = r.Next(12, 12);

            // Строим дом: 
            // - В случайном порядке выбираем рабочего из команды и даем ему выполнять Build, передаем объекты house и team.t
            for (int i = 1; i < numberOfTasks; i++)//вид работ и ее кол-во
                    {
                        team.w[r.Next(0,4)].Build(house, team.t);//это случайным образом выбирает одного из четырех рабочих в команде.
                                                                 // Вызов метода Build выбранного рабочего
                    }
                    // Выводим отчеты о построенных частях дома
                    foreach (var a in team.t.report)
                    {
                        WriteLine(a);
                    }
                    // Выводим процент завершенности строительства
                    team.t.Report();
                    WriteLine();
           
            // Рисуем дом, если он построен полностью, в противном случае сообщаем о незаконченном строительстве
            house.Paint(team.t);
            //Ждем, чтобы консольное окно не закрылось сразу после выполнения программы
            ReadKey();
        }
    }
}