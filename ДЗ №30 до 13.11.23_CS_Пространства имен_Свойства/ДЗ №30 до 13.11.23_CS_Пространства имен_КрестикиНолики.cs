using System;

namespace TicTacToeCore
{
    public enum GameState
    {
        Continue,
        Win,
        Draw
    }

    public class Board
    {
        private char[,] gameBoard;

        public Board()
        {
            // Инициализация игрового поля
            gameBoard = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("Игровое поле:");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(gameBoard[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        // Логика для совершения хода на игровом поле
        public void MakeMove(int row, int col, char symbol)
        {
            // Проверка на корректность введенных координат
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
            {
                Console.WriteLine("Некорректные координаты. Попробуйте снова.");
                return;
            }

            // Проверка, что ячейка не занята
            if (gameBoard[row, col] == 'X' || gameBoard[row, col] == 'O')
            {
                Console.WriteLine("Эта ячейка уже занята. Выберите другую.");
                return;
            }

            // Обновление игрового поля символом
            gameBoard[row, col] = symbol;
        }

        public char GetBoardValue(int row, int col)
        {
            return gameBoard[row, col];
        }

        // Проверка состояния игры (победа, ничья, продолжение игры)
        public GameState CheckGameState()
        {
            // Проверка по горизонтали, вертикали и диагоналям на наличие трех одинаковых символов
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 1] == gameBoard[i, 2])
                    return GameState.Win;

                if (gameBoard[0, i] == gameBoard[1, i] && gameBoard[1, i] == gameBoard[2, i])
                    return GameState.Win;
            }

            if (gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
                return GameState.Win;

            if (gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
                return GameState.Win;

            // Проверка наличия пустых ячеек
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] != 'X' && gameBoard[i, j] != 'O')
                        return GameState.Continue;
                }
            }

            // Если нет пустых ячеек, и нет победителя, то ничья
            return GameState.Draw;
        }
    }

    public class Gamer
    {
        protected char symbol; // Символ (X или O), которым игрок ходит

        public Gamer(char symbol)
        {
            this.symbol = symbol;
        }

        public char Symbol
        {
            get { return symbol; }
        }

        public virtual void MakeMove(Board board)
        {
            // Базовая логика для хода игрока
            Console.WriteLine("Абстрактный метод MakeMove");
        }
    }
}

namespace TicTacToeGame
{
    //using TicTacToeCore; // Используем пространство имен Core для доступа к классам оттуда

    public class User : TicTacToeCore.Gamer
    {
        public User(char symbol) : base(symbol)
        {
            Console.WriteLine($"Игрок создан с символом {symbol}");
        }

        public override void MakeMove(TicTacToeCore.Board board)
        {
            // Логика для хода пользователя
            Console.WriteLine("Ваш ход. Введите номер строки (1-3): ");
            int row = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Введите номер столбца (1-3): ");
            int col = int.Parse(Console.ReadLine()) - 1;

            board.MakeMove(row, col, Symbol);  // Используем символ пользователя
        }
    }

    public class Computer : TicTacToeCore.Gamer
    {
        public Computer(char symbol) : base(symbol)
        {
            Console.WriteLine($"Компьютер играет с символом {symbol}");
        }

        public void UpdateSymbol(char symbol)
        {
            this.symbol = symbol;
        }

        public override void MakeMove(TicTacToeCore.Board board)
        {
            // Логика для хода компьютера
            // Простая логика компьютера: выбор первой свободной ячейки
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.GetBoardValue(i, j) != 'X' && board.GetBoardValue(i, j) != 'O')
                    {
                        board.MakeMove(i, j, symbol);
                        return;
                    }
                }
            }
        }
    }

    public class GameProcessor
    {
        public static void PlayGame(User userPlayer, Computer computerPlayer, TicTacToeCore.Board board)
        {
            Console.WriteLine("Добро пожаловать в игру Крестики-Нолики!");

            // Определение первого хода
            bool userTurn = DecideFirstPlayer();

            // Игровой цикл
            while (true)
            {
                if (userTurn)
                {
                    Console.WriteLine("\nХод пользователя:");
                    userPlayer.MakeMove(board);
                }
                else
                {
                    Console.WriteLine("\nХод компьютера:");
                    computerPlayer.MakeMove(board);
                }

                board.Render();

                // Проверка состояния игры
                TicTacToeCore.GameState gameState = board.CheckGameState();

                if (gameState == TicTacToeCore.GameState.Win)
                {
                    Console.WriteLine(userTurn ? "Вы победили!" : "Компьютер победил!");
                    Environment.Exit(0);
                }
                else if (gameState == TicTacToeCore.GameState.Draw)
                {
                    Console.WriteLine("Игра завершилась вничью!");
                    Environment.Exit(0);
                }

                // Переключение хода
                userTurn = !userTurn;
            }
        }

        private static bool DecideFirstPlayer()
        {
            Console.WriteLine("\nОпределение первого хода...");

            Random random = new Random();
            return random.Next(0, 2) == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем объекты из TicTacToe.Core
            TicTacToeCore.Board board = new TicTacToeCore.Board();
            User userPlayer = new User('X'); // Начинаем с символа 'X'
            Computer computerPlayer = new Computer('O'); // Символ 'O' для компьютера

            // Играем
            GameProcessor.PlayGame(userPlayer, computerPlayer, board);
        }
    }
}
