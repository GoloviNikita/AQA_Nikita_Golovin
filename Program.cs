using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main()
    {
        // Создаем объект игры и запускаем её
        TicTacToeGame game = new TicTacToeGame();
        game.StartGame();
    }
}

// Класс, представляющий игру "Крестики-нолики"
class TicTacToeGame
{
    private char[,] board; // Игровое поле
    private char currentPlayer; // Текущий игрок (X или O)

    // Конструктор инициализации
    public TicTacToeGame()
    {
        board = new char[3, 3];
        currentPlayer = 'X';
        InitializeBoard();
    }

    // Инициализация игрового поля
    private void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    // Запуск игры
    public void StartGame()
    {
        Console.WriteLine("Добро пожаловать в игру Крестики-нолики!");

        do
        {
            PrintBoard();
            MakeMove();
        } while (!IsGameOver());

        PrintResult();
    }

    // Вывод игрового поля в консоль
    private void PrintBoard()
    {
        Console.WriteLine("  0 1 2");
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"{board[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    // Ход игрока
    private void MakeMove()
    {
        int row, col;

        do
        {
            Console.WriteLine($"Ход игрока {currentPlayer}. Введите номер строки (0-2): ");
        } while (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row >= 3);

        do
        {
            Console.WriteLine($"Ход игрока {currentPlayer}. Введите номер столбца (0-2): ");
        } while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col >= 3);

        if (board[row, col] == ' ')
        {
            board[row, col] = currentPlayer;
            SwitchPlayer();
        }
        else
        {
            Console.WriteLine("Ячейка уже занята. Попробуйте снова.");
        }
    }

    // Смена текущего игрока
    private void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    // Проверка завершения игры
    private bool IsGameOver()
    {
        return CheckForWin() || CheckForDraw();
    }

    // Проверка наличия победителя
    private bool CheckForWin()
    {
        // Проверка горизонталей, вертикалей и диагоналей
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true; // Горизонтальная линия
            if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true; // Вертикальная линия
        }
        // Диагонали
        if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return true;
        if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return true;

        return false;
    }

    // Проверка наличия ничьей
    private bool CheckForDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false; // Есть свободная ячейка
                }
            }
        }
        return true; // Все ячейки заполнены
    }

    // Вывод результата игры
    private void PrintResult()
    {
        PrintBoard();
        if (CheckForWin())
        {
            Console.WriteLine($"Игрок {currentPlayer} победил!");
        }
        else
        {
            Console.WriteLine("Ничья!");
        }

        Console.WriteLine("Хотите сыграть еще раз? (да/нет)");

        if (Console.ReadLine().ToLower() == "да")
        {
            // Начинаем новую игру
            InitializeBoard();
            currentPlayer = 'X';
            StartGame();
        }
    }
}
