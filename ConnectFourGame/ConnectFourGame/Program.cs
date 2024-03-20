/*
 * SODV1202 - Introduction to Object Oriented Programming 
 * Term Project: Connect Four Game
 * 
 * Student Name: Marie Angelika Maglinte
 * Student No: 449836
 * 
 *
 * Connect Four Game Criteria:
 * 
 * Core Classes:
 * 1. Controller Class - to play the game
 * 2. Model Class - that implements intermediate steps and holds information about the game
 * 3. Two classes that extend the Player abstract class. 
 * Note: You may also have a class that interacts and provides communication via text input from the keyboard and output on the console.
 * 
 * Basic 2 User Game is for two human users.
 * Optional - One player can be human while the other can be a computer player.
 * 
 * Important!  
 * Make sure OOP principles are followed.
 * 
 * Notes:
 * - assume the first player is always 'X'.
 * - second player is always 'O'.
 * - if a column is already full, player cannot drop a piece in the column, nothing should happen (program should not crash here).
 * - once a player has successfully played a piece, his/her turn is over.
 * - game should check if the move created a four-in-a-row or four-in-a-column or four-in-a-diagonal of the same symbol.
 * - if it is, game should print the winner.
 * - if not, it should simply pass the turn to the other player.
 * - if all 42 blocks in the grid are played, it is a draw, and no one wins.
 * 
 * - (default mode):
 * - in 2-player mode, players will use numbers to select which column they wish to drop their piece.
 * - game should prompt it is who's (player's turn)
 * - game should clearly indicate which player's turn it is by using Disc Symbol/Color/Message 
 * - for example, you could say "It is David's turn:
 * - The turns should go back and forth until the game ends.
 * 
 * - either case, 2 player or 1 player mode, when the game ends, the game should show some text indicating either who won or that the game was a draw.
 * - The game must then return to the "start" screen, where a player can once again choose either 1-player or 2-player mode (optional)
 * 
 * tips!
 * - focus on completing the game working first with 2 players.
 * - Always think about OOP concepts! Consider what objects to use and how to use properties, values, and attributes to come up with a design with more private methods/functions/properties/fields and a few public methods/functions/properties/fields.
 * 
 * 
 */


/*
 * PSEUDO-CODE:
 * 
 * Class GameController:
 *    Method Main():
 *       Create ConnectFourGame instance
 *       Call Start() method of ConnectFourGame instance
 *       
 * Class ConnectFourGame:
 *    Method Initialize():
 *       Initialize grid
 *       Initialize players
 *       Set current player as player 1
 *       Set game status as ongoing
 * 
 */


class ConnectFourGame { 
    
}


class Program
{
    static void Main(string[] args)
    {

    }
}