using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeOOP
{
    internal class Program
    {
        //Create a simple OOP program of Tic Tac Toe.

        //There are two players, the player and the computer.

        //The computer and the player SHOULD use multiple same methods.
        //Example, if the player logic uses method1 to place moves on the board,
        //the computer logic should also use method1. (15 exp penalty if failed to accomplish)

        //There should be no logic performed inside the Main method. (15 exp penalty if failed to accomplish)

        //Each game should alternate who goes first. Example, if the first game the player starts,
        //the second game the computer starts. (5 exp penalty if failed to accomplish)

        //The game ends after a best of 5(first to 3 wins) or after 5 consecutive draws. (5 exp penalty if failed to accomplish)

        //If it can win, the computer must always try to win.
        //The computer should always do anything within the rules to stop the player from winning. (5 exp penalty if failed to accomplish)

        //BONUS: After every game, a history of moves is written to a file with a unique filename.

        //I wanna add color to the board hehehe
        //I wanna add a winner/loser tracker so when the prog runs again it knows who will play first

        static string[,] _board = new string[3, 3];
        static void Main(string[] args)
        {             
            string currMove = "";
            int moveCount = 0;
            string[] moveSplit = new string[] { };

            iniBoard();
            while(true)
            {
                displayBoard();
                if (!gameFlag(moveCount))
                    break;
                moveCount = move(currMove, moveSplit, moveCount);
            }
            Console.ReadKey();
        }
        static void iniBoard()
        {
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    _board[x, y] = "-";
                }
            }
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

            Console.WriteLine("Please enter valid coordinates using the following format x-y" +
                             "\nx is the column number (0-2)" +
                             "\ny is the row number (0-2)");
        }
        static int move (string currMove, string[] moveSplit, int moveCount)
        {
            if (moveCount % 2 == 0)
            {
                Console.WriteLine("\nYour turn: ");
                Console.SetCursorPosition(11, 11);
            }
            else
            {
                Console.WriteLine("\nOpponent's turn: ");
                Console.SetCursorPosition(17, 11);
            }

            currMove = Console.ReadLine();
            moveSplit = currMove.Split('-');
            moveCount = assignMove(moveCount, moveSplit);

            return moveCount;
        }
        static int assignMove(int moveCount, string[] moveSplit)
        {
            int x = int.Parse(moveSplit[0]);
            int y = int.Parse(moveSplit[1]);

            if (moveCount % 2 == 0) //player 0
            {
                if (!_board[x, y].Contains("X") && !_board[x, y].Contains("O"))
                {
                    _board[x, y] = "X";
                    moveCount++;
                }
            }
            else //player 1
            {
                if (!_board[x, y].Contains("X") && !_board[x, y].Contains("O"))
                {
                    _board[x, y] = "O";
                    moveCount++;
                }
            }

            return moveCount;
        }

        static bool gameFlag(int moveCount)
        {
            bool flag = true;
            
            if (moveCount == 5) //testing lang
                flag = false;
            
                return flag;
        }
    }
}
