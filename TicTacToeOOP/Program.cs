using System;
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
        //I wanna add a winner/loser tracker so when the prog runs again it knows who will win

        static string[,] board = new string[3, 3];
        static void Main(string[] args)
        {             
            string[,] board = new string[3,3];
            string currMove = "";
            int moveCount = 0;
            //int pMove = 0;
            //int cpMove = 0;
            string[] moveSplit = new string[] { };

            //initialize board
            iniBoard();

            while(true)
            {
                Console.Clear();
                displayBoard();
                //player move
                moveCount = move(currMove, moveSplit, moveCount);
                
            }
            Console.ReadKey();
        }
        static void iniBoard()
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    board[x, y] = "-";
                }
            }
        }
        static void displayBoard()
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                Console.Write("| ");
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    Console.Write(board[x, y] + " | ");
                }
                Console.WriteLine("\n-------------");
            }
        }
        static int move (string currMove, string[] moveSplit, int moveCount)
        {
            Console.WriteLine("Please enter valid coordinates using the following format x-y" +
                             "\nx is the column number (0-2)" +
                             "\ny is the row number (0-2)");
            currMove = Console.ReadLine();
            moveSplit = currMove.Split('-');
            
            //assign move value to board
            if(moveCount % 2 == 0)
                board[int.Parse(moveSplit[0]), int.Parse(moveSplit[1])] = "X";
            else
                board[int.Parse(moveSplit[0]), int.Parse(moveSplit[1])] = "O";

            moveCount++;
            return moveCount;
        }
    }
}
