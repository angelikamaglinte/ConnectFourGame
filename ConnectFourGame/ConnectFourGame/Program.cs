/*
 * SODV1202 - Introduction to Object Oriented Programming 
 * Term Project: Connect Four Game
 * 
 * Student Name: Marie Angelika Maglinte
 * Student No: 449836 
 * 
 */





class ConnectFourGame
{
    // TO DO
    // study what variables need to be initialized
}

// Game Board
class GameBoard
{
    // initialize variables here: 2D Array Board, Rows, and Cols
    // game board should have 6 rows and 7 columns
    private char[,] gameBoard;
    private const int Rows = 6;
    private const int Cols = 7;

    // initialize board with empty cells here
    private void InitializeGameBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                gameBoard[row, col] = ' '; // empty cells
            }
        }
    }

    // display board game
    private void DisplayBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                Console.Write(gameBoard[row, col] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------");
        Console.WriteLine("1 2 3 4 5 6 7");
    }

    // drop a piece
    private bool DropPiece(int column, char symbol)
    {
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (gameBoard[row, column] == ' ')
            {
                gameBoard[row, column] = symbol;
                return true;
            }
        }
        return false;
    }

    // TO DO: 
    // Add logic to check for a winner (diagonally, vertically and horizontally)
    // Diagonally (bottom-right to top-left, and bottom-left to top-right)
}

class Program
{
    static void Main(string[] args)
    {
        // TO DO
        ConnectFourGame game = new ConnectFourGame();
        // study how to start the game

    }
}
