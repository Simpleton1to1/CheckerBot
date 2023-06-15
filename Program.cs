using System;

class CheckersGame
{
    static int currentPlayer = 1; // 1 for Player 1, 2 for Player 2
    static int[,] board = new int[8, 8];

    static void Main()
    {
        InitializeBoard();

        while (!IsGameOver())
        {
            DrawBoard();
            Console.WriteLine(currentPlayer == 1 ? "Player 1's turn" : "Player 2's turn");

            Console.WriteLine("Enter move (e.g., 12-34):");
            string move = Console.ReadLine();

            if (IsValidMove(move))
            {
                MakeMove(move);
                currentPlayer = currentPlayer == 1 ? 2 : 1;
            }
            else
            {
                Console.WriteLine("Invalid move! Try again.");
                continue;
            }

            Console.WriteLine();
        }

        DrawBoard();
        Console.WriteLine("Player {0} wins!", currentPlayer);
        Console.ReadLine();
    }

    static void InitializeBoard()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if ((row + col) % 2 == 1 && row < 3)
                {
                    board[row, col] = 1; // Player 1's men
                }
                else if ((row + col) % 2 == 1 && row > 4)
                {
                    board[row, col] = 2; // Player 2's men
                }
                else
                {
                    board[row, col] = 0; // Empty squares
                }
            }
        }
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("   0   1   2   3   4   5   6   7");
        Console.WriteLine();

        for (int row = 0; row < 8; row++)
        {
            Console.Write(row + " ");

            for (int col = 0; col < 8; col++)
            {
                Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.Black;

                int piece = board[row, col];
                if (piece == 0)
                {
                    Console.Write("     ");
                }
                else if (piece == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  O  ");
                }
                else if (piece == 2)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  O  ");
                }
                else if (piece == 3)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  K  ");
                }

                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }

    static bool IsValidMove(string move)
    {
        if (move.Length != 5 || move[2] != '-')
        {
            return false;
        }

        int x1, y1, x2, y2;
        if (!int.TryParse(move[0].ToString(), out x1) || !int.TryParse(move[1].ToString(), out y1) ||
            !int.TryParse(move[3].ToString(), out x2) || !int.TryParse(move[4].ToString(), out y2) ||
            x1 < 0 || x1 > 7 || y1 < 0 || y1 > 7 || x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
        {
            return false;
        }

        int piece = board[y1, x1];

        if ((currentPlayer == 1 && piece != 1) || (currentPlayer == 2 && piece != 2))
        {
            return false;
        }

        if (board[y2, x2] != 0)
        {
            return false;
        }

        if (Math.Abs(x2 - x1) != 1 || Math.Abs(y2 - y1) != 1)
        {
            return false;
        }

        if (piece == 1 && y2 <= y1)
        {
            return false;
        }

        return true;
    }

    static void MakeMove(string move)
    {
        int x1 = int.Parse(move[0].ToString());
        int y1 = int.Parse(move[1].ToString());
        int x2 = int.Parse(move[3].ToString());
        int y2 = int.Parse(move[4].ToString());

        int piece = board[y1, x1];
        board[y1, x1] = 0;
        board[y2, x2] = piece;

        if (piece == 1 && y2 == 7)
        {
            board[y2, x2] = 3; // Promote to king
        }
        else if (piece == 2 && y2 == 0)
        {
            board[y2, x2] = 3; // Promote to king
        }
    }

    static bool IsGameOver()
    {
        int player1Pieces = 0;
        int player2Pieces = 0;

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                int piece = board[row, col];

                if (piece == 1 || piece == 3)
                {
                    player1Pieces++;
                }
                else if (piece == 2 || piece == 3)
                {
                    player2Pieces++;
                }
            }
        }

        return player1Pieces == 0 || player2Pieces == 0;
    }
}
