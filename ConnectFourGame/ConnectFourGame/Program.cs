/*
 * SODV1202 - Introduction to Object Oriented Programming 
 * Term Project: Connect Four Game
 * 
 * Student Name: Marie Angelika Maglinte
 * Student No: 449836 
 * 
 */

//////////////////////////////////////////// WORKING CODE WITH 2 GAME MODES - 1. PLAYER VS PLAYER, 2. PLAYER VS. COMPUTER ////////////////////////////////////////////
// abstract player class
public abstract class Player
{
    public char Symbol { get; }
    public string Name { get; protected set; }

    // constructor method
    protected Player(char symbol)
    {
        Symbol = symbol;
    }

    // get player's name
    public abstract void GetName();

    // player's turn
    public abstract void PlayMove(GameBoard gameBoard);
}

// HumanPlayer class - inherits Player class
public class HumanPlayer : Player
{
    public HumanPlayer(char symbol) : base(symbol) { }

    public override void GetName()
    {
        //Console.WriteLine($"Enter name for {Symbol} player:");
        //Name = Console.ReadLine();

        while (true)
        {
            Console.WriteLine($"Enter name for {Symbol} player:");
            Name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine("Name cannot be blank! Please enter your name.");
            }

            else if (Name.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name! Name cannot contain numbers. Please enter again.");
            }
            else
            {
                break;
            }
        }
    }

    public override void PlayMove(GameBoard gameBoard)
    {
        int column;

        while (true)
        {
            Console.WriteLine($"It's {Name}'s ({Symbol}) turn! Please enter column (1-7):");
            try
            {
                column = int.Parse(Console.ReadLine());
                if (column < 1 || column > 7)
                {
                    throw new ArgumentOutOfRangeException();
                }
                break; // exit the loop if input is valid
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a number.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Column number must be between 1 and 7.");
            }
        }

        column--;

        if (!gameBoard.DropPiece(column, Symbol))
        {
            Console.WriteLine("Oh-oh! Column is full. Please choose another column.");
            PlayMove(gameBoard);
        }
    }
}

public class ComputerPlayer : Player
{
    private readonly Random random;

    public ComputerPlayer(char symbol) : base(symbol)
    {
        random = new Random();
        Name = "Computer";
    }

    public override void GetName()
    {
        // no need for this 
    }

    public override void PlayMove(GameBoard gameBoard)
    {
        int column;
        do
        {
            column = random.Next(0, GameBoard.Cols);
        } while (!gameBoard.DropPiece(column, Symbol));
    }
}


public class ConnectFourGame
{
    private GameBoard gameBoard;
    private Player player1;
    private Player player2;
    private Player currentPlayer;

    // constructor method
    public ConnectFourGame()
    {
        gameBoard = new GameBoard();
        ChooseGameMode();
    }

    // start game 
    public void StartGame()
    {
        bool continuePlaying = true;

        while (continuePlaying)
        {
            Console.WriteLine();
            // get player names based on chosen game mode
            player1.GetName(); 
            player2.GetName();

            while (!gameBoard.IsGameOver())
            {
                Console.Clear();
                gameBoard.DisplayBoard();
                currentPlayer.PlayMove(gameBoard);
                currentPlayer = (currentPlayer == player1) ? player2 : player1;
            }

            Console.Clear();
            gameBoard.DisplayBoard();

            Player winner = (currentPlayer == player1) ? player2 : player1;

            if (gameBoard.CheckForWinner())
            {
                Console.WriteLine($"Player {winner.Name} ({winner.Symbol} symbol) wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }

            //// prompt the user if they want to play again
            //Console.WriteLine("Do you want to play again? (Y/N)");
            //char choice = Console.ReadKey().KeyChar;
            //continuePlaying = (choice == 'Y' || choice == 'y');

            //// if the user wants to play again, reset the game board and player turns
            //if (continuePlaying)
            //{
            //    gameBoard = new GameBoard();
            //    currentPlayer = player1; // Reset currentPlayer to player1
            //}

            // 
            // prompt the user if they want to play again
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Do you want to play again? (Y/N)");
                try
                {
                    char choice = Console.ReadKey().KeyChar;
                    if (choice == 'Y' || choice == 'y')
                    {
                        continuePlaying = true;
                        validInput = true;
                    }
                    else if (choice == 'N' || choice == 'n')
                    {
                        continuePlaying = false;
                        validInput = true;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input! Please enter 'Y' or 'N'.");
                }
            }

            // if the user wants to play again, reset the game board and player turns
            if (continuePlaying)
            {
                gameBoard = new GameBoard();
                currentPlayer = player1; // Reset currentPlayer to player1
            }
        }
    }

    private void ChooseGameMode()
    {
        Console.WriteLine("Choose Game Mode:");
        Console.WriteLine("1. Player vs Player");
        Console.WriteLine("2. Player vs Computer");
        Console.Write("Enter your choice: ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                player1 = new HumanPlayer('X');
                player2 = new HumanPlayer('O');
                break;
            case 2:
                player1 = new HumanPlayer('X');
                player2 = new ComputerPlayer('O');
                break;
            default:
                Console.WriteLine("Invalid choice. Defaulting to Player vs Player mode.");
                player1 = new HumanPlayer('X');
                player2 = new HumanPlayer('O');
                break;
        }

        currentPlayer = player1; // set currentPlayer to player1
    }
}

public class GameBoard
{
    private char[,] gameBoard;
    public const int Rows = 6;
    public const int Cols = 7;

    // constructor method
    public GameBoard()
    {
        gameBoard = new char[Rows, Cols];
        InitializeGameBoard();
    }

    // initialize game board
    private void InitializeGameBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                gameBoard[row, col] = ' ';
            }
        }
    }

    // display game board
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

    // drop symbol 
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

    // check for winner 
    public bool CheckForWinner()
    {
        // check diagonally (bottom-left to top-right)
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

        // check diagonally (top-left to bottom-right)
        for (int row = 3; row < Rows; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (gameBoard[row, col] != ' ' &&
                    gameBoard[row, col] == gameBoard[row - 1, col + 1] &&
                    gameBoard[row, col] == gameBoard[row - 2, col + 2] &&
                    gameBoard[row, col] == gameBoard[row - 3, col + 3])
                {
                    return true;
                }
            }
        }

        // check horizontally
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

        // Check vertically
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

        return false;
    }

    // check if game is draw
    public bool CheckIfDraw()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                if (gameBoard[row, col] == ' ')
                {
                    // if there's an empty space, the game is not a draw yet.
                    return false;
                }
            }
        }

        // if there are no empty spaces, the game is a draw
        return true;
    }

    // check if game is over
    public bool IsGameOver()
    {
        return CheckForWinner() || CheckIfDraw();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConnectFourGame game = new ConnectFourGame();
        game.StartGame();
    }
}


//////////////////////////////////////////// WORKING SIMPLE PLAYER GAME - HUMAN VS. HUMAN ////////////////////////////////////////////
//// Abstract Player Class
//public abstract class Player
//{
//    public char Symbol { get; }

//    public string Name { get; protected set; }

//    // constructor method
//    protected Player(char symbol)
//    {
//        Symbol = symbol;
//    }

//    // get player's name
//    public abstract void GetName();

//    // player's turn
//    public abstract void PlayMove(GameBoard gameBoard);

//}

//// HumanPlayer class - inherits Player class
//public class HumanPlayer : Player
//{
//    public HumanPlayer(char symbol) : base(symbol) { }

//    public override void GetName()
//    {
//        //Console.WriteLine($"Enter name for {Symbol} player:");
//        //Name = Console.ReadLine();
//        while (true)
//    {
//        Console.WriteLine($"Enter name for {Symbol} player:");
//        Name = Console.ReadLine();

//        if (string.IsNullOrWhiteSpace(Name))
//        {
//            Console.WriteLine("Name cannot be blank! Please enter your name.");
//        }

//        else if (Name.Any(char.IsDigit))
//        {
//            Console.WriteLine("Invalid name! Name cannot contain numbers. Please enter again.");
//        }
//        else
//        {
//            break; 
//        }
//    }
//    }

//    public override void PlayMove(GameBoard gameBoard)
//    {

//        //int column;
//        //do
//        //{
//        //    Console.WriteLine($"{Name}, with {Symbol} disc symbol, please enter column (1-7):");
//        //} while (!int.TryParse( Console.ReadLine(), out column) || column < 1 || column > 7);

//        //column--;

//        //if(!gameBoard.DropPiece(column, Symbol))
//        //{
//        //    Console.WriteLine("Oh-oh! Column is full. Please choose another column.");
//        //    PlayMove(gameBoard);
//        //}

//        // Updated PlayMove method to handle exception errors!
//        int column;

//        while (true)
//        {
//            Console.WriteLine($"It's {Name}'s ({Symbol}) turn! Please enter column (1-7):");
//            try
//            {
//                column = int.Parse(Console.ReadLine());
//                if (column < 1 || column > 7)
//                {
//                    throw new ArgumentOutOfRangeException();
//                }
//                break; // Exit the loop if input is valid
//            }
//            catch (FormatException)
//            {
//                Console.WriteLine("Invalid input! Please enter a number.");
//            }
//            catch (ArgumentOutOfRangeException)
//            {
//                Console.WriteLine("Column number must be between 1 and 7.");
//            }
//        }

//        column--;

//        if (!gameBoard.DropPiece(column, Symbol))
//        {
//            Console.WriteLine("Oh-oh! Column is full. Please choose another column.");
//            PlayMove(gameBoard);
//        }
//    }
//}


//public class ConnectFourGame
//{
//    private GameBoard gameBoard;
//    private Player player1;
//    private Player player2;
//    private Player currentPlayer;

//    // constructor method
//    public ConnectFourGame()
//    {
//        gameBoard = new GameBoard();
//        player1 = new HumanPlayer('X');
//        player2 = new HumanPlayer('O');
//        player1.GetName();
//        player2.GetName();
//        currentPlayer = player1; 
//    }

//    // start game 
//    public void StartGame()
//    {
//        //player1.GetName();
//        //player2.GetName();

//        bool continuePlaying = true;

//        while (continuePlaying)
//        {


//            while (!gameBoard.IsGameOver())
//            {
//                Console.Clear();
//                gameBoard.DisplayBoard();
//                currentPlayer.PlayMove(gameBoard);
//                currentPlayer = (currentPlayer == player1) ? player2 : player1;
//            }

//            Console.Clear();
//            gameBoard.DisplayBoard();

//            Player winner = (currentPlayer == player1) ? player2 : player1;

//            if (gameBoard.CheckForWinner())
//            {
//                Console.WriteLine($"Player {winner.Name} ({winner.Symbol} symbol) wins!");
//            }
//            else
//            {
//                Console.WriteLine("It's a draw!");
//            }

//            // prompt the user if they want to play again
//            Console.WriteLine("Do you want to play again? (Y/N)");
//            char choice = Console.ReadKey().KeyChar;
//            continuePlaying = (choice == 'Y' || choice == 'y');



//            // if the user wants to play again, reset the game board and player turns
//            if (continuePlaying)
//            {
//                gameBoard = new GameBoard();

//                Console.WriteLine();
//                player1.GetName();
//                player2.GetName();
//            }
//        }
//    }


//}

//public class GameBoard
//{
//    private char[,] gameBoard;
//    private const int Rows = 6;
//    private const int Cols = 7;

//    // constructor method
//    public GameBoard()
//    {
//        gameBoard = new char[Rows, Cols];
//        InitializeGameBoard();
//    }

//    // initialize game board
//    private void InitializeGameBoard()
//    {
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col < Cols; col++)
//            {
//                gameBoard[row, col] = ' ';
//            }
//        }
//    }

//    // display game board
//    public void DisplayBoard()
//    {
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col < Cols; col++)
//            {
//                Console.Write(gameBoard[row, col] + " ");
//            }
//            Console.WriteLine();
//        }
//        Console.WriteLine("---------------");
//        Console.WriteLine("1 2 3 4 5 6 7");
//    }

//    // drop symbol 
//    public bool DropPiece(int column, char symbol)
//    {
//        for (int row = Rows - 1; row >= 0; row--)
//        {
//            if (gameBoard[row, column] == ' ')
//            {
//                gameBoard[row, column] = symbol;
//                return true;
//            }
//        }
//        return false;
//    }

//    // check for winner 
//    public bool CheckForWinner()
//    {
//        // Check diagonally (bottom-left to top-right)
//        for (int row = 0; row <= Rows - 4; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row + 1, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row + 2, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row + 3, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        // Check diagonally (top-left to bottom-right)
//        for (int row = 3; row < Rows; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row - 1, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row - 2, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row - 3, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        //Check horizontally
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        // Check vertically
//        for (int col = 0; col < Cols; col++)
//        {
//            for (int row = 0; row <= Rows - 4; row++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row + 1, col] &&
//                    gameBoard[row, col] == gameBoard[row + 2, col] &&
//                    gameBoard[row, col] == gameBoard[row + 3, col])
//                {
//                    return true;
//                }
//            }
//        }

//        return false;
//    }

//    // check if game is draw
//    public bool CheckIfDraw()
//    {
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col < Cols; col++)
//            {
//                if (gameBoard[row, col] == ' ')
//                {
//                    // Found an empty space, the game is not a draw
//                    return false;
//                }
//            }
//        }

//        // If there are no empty spaces, the game is a draw
//        return true;
//    }

//    // check if game is over
//    public bool IsGameOver()
//    {
//        return CheckForWinner() || CheckIfDraw();
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        ConnectFourGame game = new ConnectFourGame();
//        game.StartGame();
//    }
//}


//////////////////////////////////////////// WORKING CODE - WITHOUT OOP ////////////////////////////////////////////
//public class ConnectFourGame
//{
//    // TO DO
//    // study what variables need to be initialized
//    private GameBoard gameBoard;
//    private Player player1;
//    private Player player2;
//    private Player currentPlayer;

//    // create a constructor class 
//    public ConnectFourGame()
//    {
//        gameBoard = new GameBoard();
//        player1 = new Player('X');
//        player2 = new Player('O');
//        currentPlayer = player1;
//    }

//    // create StartGame() method
//    public void StartGame()
//    {
//        // StartGame Method - old logic
//        while (!gameBoard.IsGameOver())
//        {
//            gameBoard.DisplayBoard();
//            int column;

//            do
//            {
//                //System.Console.WriteLine($"{currentPlayer.Name}, enter column (1-7):"); 
//                Console.WriteLine($"{currentPlayer.Name}, enter column (1-7):");
//            } while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7);

//            column--;

//            // validations
//            if (gameBoard.DropPiece(column, currentPlayer.Symbol))
//            {
//                if (gameBoard.CheckForWinner())
//                {
//                    // display game board
//                    gameBoard.DisplayBoard();
//                    Console.WriteLine($"Congratulations! {currentPlayer.Name} wins!");
//                    return;
//                }
//                else if (gameBoard.CheckIfDraw())
//                {
//                    // display game board
//                    gameBoard.DisplayBoard();
//                    Console.WriteLine("Oh-oh! It's a draw!");
//                    return;
//                }
//                else
//                {
//                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Oh-oh! Column is already full. Please choose another column.");
//            }

//        }
//    }
//}


////Player Class - without OOP abstract class/ methods
//class Player
//{
//    public char Symbol { get; }
//    public string Name { get; }

//    public Player(char symbol)
//    {
//        Symbol = symbol;
//        Name = (symbol == 'X') ? "Player 1 (X)" : "Player 2 (O)";
//    }
//}

//// Game Board Class
//class GameBoard
//{
//    // initialize variables here: 2D Array Board, Rows, and Cols
//    // game board should have 6 rows and 7 columns
//    private char[,] gameBoard;
//    private const int Rows = 6;
//    private const int Cols = 7;

//    public GameBoard()
//    {
//        gameBoard = new char[Rows, Cols];
//        InitializeGameBoard(); // initialize game board
//    }

//    // initialize board with empty cells here
//    public void InitializeGameBoard()
//    {
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col < Cols; col++)
//            {
//                gameBoard[row, col] = ' '; // empty cells
//            }
//        }
//    }

//    // display board game
//    public void DisplayBoard()
//    {
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col < Cols; col++)
//            {
//                Console.Write(gameBoard[row, col] + " ");
//            }
//            Console.WriteLine();
//        }
//        Console.WriteLine("---------------");
//        Console.WriteLine("1 2 3 4 5 6 7");
//    }

//    // drop a piece
//    public bool DropPiece(int column, char symbol)
//    {
//        for (int row = Rows - 1; row >= 0; row--)
//        {
//            if (gameBoard[row, column] == ' ')
//            {
//                gameBoard[row, column] = symbol;
//                return true;
//            }
//        }
//        return false;
//    }

//    // TO DO: 
//    // Add logic to check for a winner (diagonally, vertically and horizontally)
//    // Diagonally (top-left to bottom-right, and bottom-left to top-right)
//    public bool CheckForWinner()
//    {
//        // check for a winner diagonally from bottom-left to top-right
//        for (int row = 0; row <= Rows - 4; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row + 1, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row + 2, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row + 3, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        // check for a winner diagonally from top-left to bottom-right
//        for (int row = 0; row <= Rows - 4; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row + 1, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row + 2, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row + 3, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        // check for a winner horizontally
//        for (int row = 0; row < Rows; row++)
//        {
//            for (int col = 0; col <= Cols - 4; col++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row, col + 1] &&
//                    gameBoard[row, col] == gameBoard[row, col + 2] &&
//                    gameBoard[row, col] == gameBoard[row, col + 3])
//                {
//                    return true;
//                }
//            }
//        }

//        // check for a winner vertically
//        for (int col = 0; col < Cols; col++)
//        {
//            for (int row = 0; row <= Rows - 4; row++)
//            {
//                if (gameBoard[row, col] != ' ' &&
//                    gameBoard[row, col] == gameBoard[row + 1, col] &&
//                    gameBoard[row, col] == gameBoard[row + 2, col] &&
//                    gameBoard[row, col] == gameBoard[row + 3, col])
//                {
//                    return true;
//                }
//            }
//        }


//        return false; // if there's no winner, return false
//    }

//    // check if the game is a draw (no winner!)
//    public bool CheckIfDraw()
//    {
//        for (int col = 0; col < Cols; col++)
//        {
//            if (gameBoard[0, col] == ' ')
//            {
//                // if there's still an empty cell at the top row, then game is not yet over (not a draw!)
//                return false;
//            }
//        }
//        // otherwise, if all cells on top row are filled, game is a draw (no winner!)
//        return true;
//    }

//    // check if the game is already over (if someone wins or board is already full)
//    public bool IsGameOver()
//    {
//        return CheckForWinner() || CheckIfDraw();
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        // TO DO
//        ConnectFourGame game = new ConnectFourGame();
//        // study how to start the game
//        game.StartGame();
//    }
//}