using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class TicTacToe
{
    static char[,] board = {
        {'1', '2', '3'},
        {'4', '5', '6'},
        {'7', '8', '9'}
    };
    static char currentPlayer = 'X';
    static string player1Name = "Player 1";
    static string player2Name = "Player 2";
    static int player1Score = 0;
    static int player2Score = 0;
    static DateTime startTime;
    static DateTime endTime;
    static List<GameResult> gameResults = new List<GameResult>();

    static void Main()
    {
        bool playAgain;

        do
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("Select option:");
            Console.WriteLine("1. Play against computer");
            Console.WriteLine("2. Play against another player");
            Console.WriteLine("3. View Ratings");
            Console.WriteLine("4. View Game Table");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 5))
            {
                Console.Write("Invalid input. Enter a number between 1 and 5: ");
            }

            if (choice == 1 || choice == 2)
            {
                Console.Write("Enter name for Player 1: ");
                player1Name = Console.ReadLine();
            }

            if (choice == 1)
            {
                player2Name = "T1000";
                PlayAgainstComputer();
            }
            else if (choice == 2)
            {
                Console.Write("Enter name for Player 2: ");
                player2Name = Console.ReadLine();
                PlayAgainstPlayer();
            }
            else if (choice == 3)
            {
                ViewRatings();
            }
            else if (choice == 4)
            {
                ViewGameTable();
            }

            playAgain = (choice != 5);
            if (playAgain)
            {
                Console.Write("Do you want to play again? (y/n): ");
                playAgain = Console.ReadLine().ToLower() == "y";
                ResetGame();
            }
        } while (playAgain);

        Console.WriteLine("Thanks for playing! Goodbye.");
        Console.ReadLine();
    }

    static void PlayAgainstComputer()
    {
        do
        {
            DrawBoard();
            if (currentPlayer == 'X')
            {
                MakeMove();
            }
            else
            {
                MakeComputerMove();
            }
        } while (!IsGameFinishedAgainstComputer());

        DrawBoard();
        endTime = DateTime.Now;
        UpdateScoresAndSaveResults();
        DisplayScores();
        DisplayGameResults();
    }

    static void PlayAgainstPlayer()
    {
        do
        {
            DrawBoard();
            MakeMove();
        } while (!IsGameFinishedAgainstPlayer());

        DrawBoard();
        endTime = DateTime.Now;
        UpdateScoresAndSaveResults();
        DisplayScores();
        DisplayGameResults();
    }

    static void ViewRatings()
    {
        Console.WriteLine("Player Ratings:");
        var sortedRatings = gameResults
            .OrderByDescending(r => r.Player1Score + r.Player2Score)
            .ThenBy(r => r.Player1Name)
            .ToList();

        foreach (var rating in sortedRatings)
        {
            Console.WriteLine($"{rating.Player1Name}: {rating.Player1Score} points");
            Console.WriteLine($"{rating.Player2Name}: {rating.Player2Score} points");
            Console.WriteLine("=============================");
        }
    }

    static void ViewGameTable()
    {
        Console.WriteLine("Game Table:");
        foreach (var result in gameResults)
        {
            Console.WriteLine($"Start Time: {result.StartTime}");
            Console.WriteLine($"Player 1: {result.Player1Name} - {result.Player1Score} points");
            Console.WriteLine($"Player 2: {result.Player2Name} - {result.Player2Score} points");
            Console.WriteLine("=============================");
        }
    }

    static void UpdateScoresAndSaveResults()
    {
        GameResult result = new GameResult
        {
            StartTime = startTime,
            EndTime = endTime,
            Player1Name = player1Name,
            Player2Name = player2Name,
            Player1Score = player1Score,
            Player2Score = player2Score
        };

        gameResults.Add(result);
        SaveGameResultsToFile();

        if (CheckWin('X'))
        {
            Console.WriteLine($"{player1Name} wins!");
            player1Score += 3;
        }
        else if (CheckWin('O'))
        {
            Console.WriteLine($"{player2Name} wins!");
            player2Score += 3;
        }
        else
        {
            Console.WriteLine("It's a draw!");
            player1Score += 1;
            player2Score += 1;
        }
    }

    static void SaveGameResultsToFile()
    {
        string fileName = "TicTacToeResults.txt";
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine("Game Results");
            writer.WriteLine($"Start Time: {startTime}");
            writer.WriteLine($"End Time: {endTime}");
            writer.WriteLine($"Player 1: {player1Name} - Score: {player1Score}");
            writer.WriteLine($"Player 2: {player2Name} - Score: {player2Score}");
            writer.WriteLine("======================================");
        }
    }

    static void DisplayScores()
    {
        Console.WriteLine($"Scores: {player1Name} - {player1Score} | {player2Name} - {player2Score}");
    }

    static void DisplayGameResults()
    {
        Console.WriteLine("Game Results:");
        foreach (var result in gameResults)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Start Time: {result.StartTime}");
            Console.WriteLine($"End Time: {result.EndTime}");
            Console.WriteLine($"Player 1: {result.Player1Name} - Score: {result.Player1Score}");
            Console.WriteLine($"Player 2: {result.Player2Name} - Score: {result.Player2Score}");
        }
        Console.WriteLine("======================================");
    }

    static void ResetGame()
    {
        board = new char[,]
        {
            {'1', '2', '3'},
            {'4', '5', '6'},
            {'7', '8', '9'}
        };
        currentPlayer = 'X';
        startTime = DateTime.Now;
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("  {0} | {1} | {2}", board[0, 0], board[0, 1], board[0, 2]);
        Console.WriteLine("  ---------");
        Console.WriteLine("  {0} | {1} | {2}", board[1, 0], board[1, 1], board[1, 2]);
        Console.WriteLine("  ---------");
        Console.WriteLine("  {0} | {1} | {2}", board[2, 0], board[2, 1], board[2, 2]);
        Console.WriteLine("  ---------");
        Console.WriteLine($"  {player1Name}: X  |  {player2Name}: O");
    }

    static void MakeMove()
    {
        bool validInput = false;
        do
        {
            Console.Write($"{currentPlayer}'s turn. Enter your move (1-9): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int position))
            {
                if (position >= 1 && position <= 9)
                {
                    int row = (position - 1) / 3;
                    int col = (position - 1) % 3;
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        board[row, col] = currentPlayer;
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Cell already taken. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter a number between 1 and 9.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter a number between 1 and 9.");
            }
        } while (!validInput);

        SwitchPlayer();
    }

    static void MakeComputerMove()
    {
        Console.WriteLine($"{player2Name}'s turn (Computer).");
        Random random = new Random();
        bool validMove = false;

        do
        {
            int position = random.Next(1, 10);
            int row = (position - 1) / 3;
            int col = (position - 1) % 3;

            if (board[row, col] != 'X' && board[row, col] != 'O')
            {
                board[row, col] = currentPlayer;
                validMove = true;
            }
        } while (!validMove);

        SwitchPlayer();
    }

    static void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    static bool CheckWin(char player)
    {
        // Check rows, columns, and diagonals for a win
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))
            {
                return true;
            }
        }

        if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
            (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
        {
            return true;
        }

        return false;
    }

    static bool IsBoardFull()
    {
        // Check if the board is full
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != 'X' && board[i, j] != 'O')
                {
                    return false;
                }
            }
        }

        return true;
    }

    static bool IsGameFinishedAgainstPlayer()
    {
        return CheckWin('X') || CheckWin('O') || IsBoardFull();
    }

    static bool IsGameFinishedAgainstComputer()
    {
        return CheckWin('X') || CheckWin('O') || IsBoardFull();
    }

    class GameResult
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
    }
}
