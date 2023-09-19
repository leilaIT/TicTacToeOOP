using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToeOOP
{
    internal class Program
    {
        //Create a simple OOP program of Tic Tac Toe.

        //OK - There are two players, the player and the computer.

        //OK - The computer and the player SHOULD use multiple same methods.
        //Example, if the player logic uses method1 to place moves on the board,
        //the computer logic should also use method1. (15 exp penalty if failed to accomplish)

        //OK (ig) - There should be no logic performed inside the Main method. (15 exp penalty if failed to accomplish)

        //Each game should alternate who goes first. Example, if the first game the player starts,
        //the second game the computer starts. (5 exp penalty if failed to accomplish)

        //The game ends after a best of 5(first to 3 wins) or after 5 consecutive draws. (5 exp penalty if failed to accomplish)

        //If it can win, the computer must always try to win.
        //The computer should always do anything within the rules to stop the player from winning. (5 exp penalty if failed to accomplish)

        //BONUS: After every game, a history of moves is written to a file with a unique filename. (20 exp)

        //I wanna add color to the board hehehe
        //I wanna add a winner/loser tracker so when the prog runs again it knows who will play first

        static string[,] _board = new string[3, 3];
        static void Main(string[] args)
        {             
            string currMove = "";
            int playerTurn = 0;
            string[] moveSplit = new string[] { };

            iniBoard();
            while(true)
            {
                playerTurn = move(currMove, moveSplit, playerTurn);
                displayBoard();

                if (!winFlag(""))
                    break;
            }
            Console.ReadKey();
        }
        static void iniBoard()
        {
            int value = 0;
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    _board[x, y] = "-";
                }
            }
            displayBoard();
        }
        static void displayBoard()
        {
            Console.Clear();
            Console.WriteLine("-------------");
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                Console.Write("| ");
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    Console.Write(_board[x, y] + " | ");
                }
                Console.WriteLine("\n-------------");
            }
        }
        static int move (string currMove, string[] moveSplit, int playerTurn)
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;

            Console.WriteLine("Please enter valid coordinates using the following format x-y" +
                             "\nx is the column number (0-2)" +
                             "\ny is the row number (0-2)");

            if (playerTurn % 2 == 0)
            {
                Console.WriteLine("\nYour turn: ");
                Console.SetCursorPosition(11, 11);
                currMove = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nOpponent's turn: ");
                Console.SetCursorPosition(17, 11);
                Console.WriteLine("\nOpponent is thinking. . .");
                Thread.Sleep(2000);

                if (spacesLeft() > 0)
                {
                    while (true)
                    {
                        x = rnd.Next(0, 2);
                        y = rnd.Next(0, 2);

                        if (_board[x, y] == "-")
                        {
                            currMove = x.ToString() + "-" + y.ToString();
                            break;
                        }
                    }

                }
                //else
                //draw

            }

            moveSplit = currMove.Split('-');
            playerTurn = assignMove(playerTurn, moveSplit);

            return playerTurn;
        }
        static int assignMove(int playerTurn, string[] moveSplit)
        {
            int x = int.Parse(moveSplit[0]);
            int y = int.Parse(moveSplit[1]);

            if (playerTurn % 2 == 0) //player 1
            {
                if (!_board[x, y].Contains("X") && !_board[x, y].Contains("O"))
                {
                    _board[x, y] = "X";
                    playerTurn++;
                }
            }
            else //player 2
            {
                _board[x, y] = "O";
                playerTurn++;
            }
            return playerTurn;
        }
        static int spacesLeft ()
        {
            int spaceCount = 0;

            for(int x = 0; x < _board.GetLength(0); x++) 
            { 
                for(int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y].Contains("-"))  
                        spaceCount++;
                }
            }

            return spaceCount;
        }
        static bool winFlag(string player)
        {
            int pNum = 0;
            bool flag = true;
            
            while(pNum < 2 && flag)
            {
                if (pNum == 0)
                    player = "X";
                else
                    player = "O";

                for (int x = 0; x < _board.GetLength(0); x++)
                {
                    if (_board[x, 0] == player && _board[x, 1] == player && _board[x, 2] == player
                     || _board[0, x] == player && _board[1, 0] == player && _board[2, x] == player) //column
                    {
                        flag = false;
                        break;
                    }
                    else if (_board[0, 0] == player && _board[1, 1] == player && _board[2, 2] == player
                          || _board[0, 2] == player && _board[1, 1] == player && _board[2, 0] == player) //diagonal
                    {
                        flag = false;
                        break;
                    }
                }
                pNum++;
            }

            if (!flag)
                displayWinner(player);

            return flag;
        }
        static void displayWinner(string player)
        {
            Console.Clear();
            displayBoard();

            if (player.Contains("X"))
                Console.WriteLine("You win!");
            else
                Console.WriteLine("Opponent wins!");
        }
    }
}
