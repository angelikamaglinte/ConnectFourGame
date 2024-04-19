/*
 * SODV1202 - Introduction to Object Oriented Programming 
 * Term Project: Connect Four Game
 * 
 * Student Name: Marie Angelika Maglinte
 * Student No: 449836 
 * 
 */
/*
 * SODV1202 - Introduction to Object Oriented Programming 
 * Term Project: Connect Four Game
 * 
 * Student Name: Marie Angelika Maglinte
 * Student No: 449836 
 * 
 */


public class ConnectFourGame
{
    // TO DO
    // study what variables need to be initialized
    private GameBoard gameBoard;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    // create a constructor class 
    public ConnectFourGame()
    {
        gameBoard = new GameBoard();
        player1 = new Player('X');
        player2 = new Player('O');
        currentPlayer = player1;
    }

    // create StartGame() method
    public void StartGame()
    {
        // StartGame Method - old logic
        while (!gameBoard.IsGameOver())
        {
            gameBoard.DisplayBoard();
            int column;

            do
            {
                //System.Console.WriteLine($"{currentPlayer.Name}, enter column (1-7):"); 
                Console.WriteLine($"{currentPlayer.Name}, enter column (1-7):");
            } while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7);

            column--;

            // validations
            if (gameBoard.DropPiece(column, currentPlayer.Symbol))
            {
                if (gameBoard.CheckForWinner())
                {
                    // display game board
                    gameBoard.DisplayBoard();
                    Console.WriteLine($"Congratulations! {currentPlayer.Name} wins!");
                    return;
                }
                else if (gameBoard.CheckIfDraw())
                {
                    // display game board
                    gameBoard.DisplayBoard();
                    Console.WriteLine("Oh-oh! It's a draw!");
                    return;
                }
                else
                {
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
            else
            {
                Console.WriteLine("Oh-oh! Column is already full. Please choose another column.");
            }

        }
    }
}


//Player Class - without OOP abstract class/ methods
class Player
{
    public char Symbol { get; }
    public string Name { get; }

    public Player(char symbol)
    {
        Symbol = symbol;
        Name = (symbol == 'X') ? "Player 1 (X)" : "Player 2 (O)";
    }
}

// Game Board Class
class GameBoard
{
    // initialize variables here: 2D Array Board, Rows, and Cols
    // game board should have 6 rows and 7 columns
    private char[,] gameBoard;
    private const int Rows = 6;
    private const int Cols = 7;

    public GameBoard()
    {
        gameBoard = new char[Rows, Cols];
        InitializeGameBoard(); // initialize game board
    }

    // initialize board with empty cells here
    public void InitializeGameBoard()
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
    public void DisplayBoard()
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
    public bool DropPiece(int column, char symbol)
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
    // Diagonally (top-left to bottom-right, and bottom-left to top-right)
    public bool CheckForWinner()
    {
        // check for a winner diagonally from bottom-left to top-right
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (gameBoard[row, col] != ' ' &&
                    gameBoard[row, col] == gameBoard[row + 1, col + 1] &&
                    gameBoard[row, col] == gameBoard[row + 2, col + 2] &&
                    gameBoard[row, col] == gameBoard[row + 3, col + 3])
                {
                    return true;
                }
            }
        }

        // check for a winner diagonally from top-left to bottom-right
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (gameBoard[row, col] != ' ' &&
                    gameBoard[row, col] == gameBoard[row + 1, col + 1] &&
                    gameBoard[row, col] == gameBoard[row + 2, col + 2] &&
                    gameBoard[row, col] == gameBoard[row + 3, col + 3])
                {
                    return true;
                }
            }
        }

        // check for a winner horizontally
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (gameBoard[row, col] != ' ' &&
                    gameBoard[row, col] == gameBoard[row, col + 1] &&
                    gameBoard[row, col] == gameBoard[row, col + 2] &&
                    gameBoard[row, col] == gameBoard[row, col + 3])
                {
                    return true;
                }
            }
        }

        // check for a winner vertically
        for (int col = 0; col < Cols; col++)
        {
            for (int row = 0; row <= Rows - 4; row++)
            {
                if (gameBoard[row, col] != ' ' &&
                    gameBoard[row, col] == gameBoard[row + 1, col] &&
                    gameBoard[row, col] == gameBoard[row + 2, col] &&
                    gameBoard[row, col] == gameBoard[row + 3, col])
                {
                    return true;
                }
            }
        }


        return false; // if there's no winner, return false
    }

    // check if the game is a draw (no winner!)
    public bool CheckIfDraw()
    {
        for (int col = 0; col < Cols; col++)
        {
            if (gameBoard[0, col] == ' ')
            {
                // if there's still an empty cell at the top row, then game is not yet over (not a draw!)
                return false;
            }
        }
        // otherwise, if all cells on top row are filled, game is a draw (no winner!)
        return true;
    }

    // check if the game is already over (if someone wins or board is already full)
    public bool IsGameOver()
    {
        return CheckForWinner() || CheckIfDraw();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // TO DO
        ConnectFourGame game = new ConnectFourGame();
        // study how to start the game
        game.StartGame();
    }
}