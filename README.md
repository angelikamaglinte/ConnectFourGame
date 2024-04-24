# ConnectFourGame
Object-Oriented Programming - Term Project: Connect Four Game

SODV1202 Term Project
Student Name: Marie Angelika Maglinte

HOW TO PLAY
Installation
1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or your preferred C# IDE.
3. Build and run the project.

Game Modes
1. Player vs Player: Two human players take turns to play.
2. Player vs Computer: A human player plays against the computer.

Gameplay
- The game board consists of 6 rows and 7 columns.
- Players take turns to drop their symbol (X or O) into a column of their choice.
- The first player to connect four of their symbols horizontally, vertically, or diagonally wins.
- If no player achieves a four-in-a-row and the game board is full, it's a draw.


CODE STRUCTURE
Player Classes
Player: An abstract class representing a player with a symbol and a name.
HumanPlayer: Inherits from Player and represents a human player who interacts with the console to input their moves and name.
ComputerPlayer: Inherits from Player and represents a computer player that generates random moves.

GameBoard Class
Represents the game board with methods to initialize, display, drop pieces, and check for a winner or draw.
ConnectFourGame Class
Manages the flow of the game, including choosing the game mode, starting the game, and handling player turns and game over conditions.

Program Class
Contains the Main method to start the game.


GETTING STARTED
To start playing, run the program and follow the prompts to choose the game mode and enter player names (if applicable). Then, take turns dropping pieces on the board until a winner is determined or the game ends in a draw.