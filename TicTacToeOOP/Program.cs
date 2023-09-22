using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        //OK - There should be no logic performed inside the Main method. (15 exp penalty if failed to accomplish)

        //OK - Each game should alternate who goes first. Example, if the first game the player starts,
        //the second game the computer starts. (5 exp penalty if failed to accomplish)

        //OK - The game ends after a best of 5(first to 3 wins) or after 5 consecutive draws. (5 exp penalty if failed to accomplish)

        //If it can win, the computer must always try to win.
        //The computer should always do anything within the rules to stop the player from winning. (5 exp penalty if failed to accomplish)

        //BONUS: OK - After every game, a history of moves is written to a file with a unique filename. (20 exp)

        //I wanna add color to the board hehehe

        static string[,] _board = new string[3, 3];
        static List<string> _moveHistory = new List<string>();
        static int _playerTurn = 0;
        static int _pX = 0;
        static int _pO = 0;
        static int _roundCount = 0;
        static void Main(string[] args)
        {   
            while (roundChecker())
            {
                gameStart();
            }
        }
        static bool gameStart()
        {
            bool flag = true;
            iniBoard();
            string add = "Moves in Round " + _roundCount;
            _moveHistory.Add(add);

            if(_roundCount % 2 == 0)
                _playerTurn = 0;
            else
                _playerTurn = 1;
            
            while (flag)
            {
                _playerTurn = move("");
                displayBoard();
                if(spacesLeft() == 0)
                {
                    displayWinner("D");
                    return false;
                }
                if(winFlag("")) //if there is no winner yet
                    _playerTurn++;
                else //if (!winFlag("")) //if there is a winner
                {
                    displayWinner(" ");
                    return false;
                }
            }
            return true;
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
            displayBoard();
        }
        static void displayBoard()
        {
            Console.Clear();
            Console.WriteLine("Player X: {0} | Player O: {1}", _pX, _pO);
            Console.WriteLine("\n-------------");
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                Console.Write("| ");
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y] == "X")
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    else if (_board[x, y] == "O")
                        Console.ForegroundColor= ConsoleColor.DarkRed;

                    Console.Write(_board[x, y]);
                    Console.ResetColor();
                    Console.Write(" | ");
                }
                Console.WriteLine("\n-------------");
            }
        }
        static int move(string currMove)
        {
            Random rnd = new Random();
            string[] moveSplit = new string[] { };
            int x = 0;
            int y = 0;
            int num = 0;
            int turn = 0;

            turn = _playerTurn;

            Console.WriteLine("Please enter valid coordinates using the following format x-y" +
                             "\nx is the column number (0-2)" +
                             "\ny is the row number (0-2)");

            if (_roundCount % 2 == 0) //if current round even, player x first
            { 
                if (_playerTurn % 2 == 0) //player X
                {
                    Console.WriteLine("\nPlayer X's turn: ");
                    Console.SetCursorPosition(17, 13);
                    while(true)
                    {
                        currMove = Console.ReadLine();
                        moveSplit = currMove.Split('-');
                        if(moveSplit.Length != 2 || 
                          !int.TryParse(moveSplit[0], out x) ||
                          !int.TryParse(moveSplit[1], out y))
                        {
                            x = -1;
                            y = -1;
                        }

                        if (x < 0 || x > 2 || y < 0 || y > 2)
                        {
                            Console.WriteLine("{0} is not a valid input. . . Press any key to continue. . .", currMove);
                            Console.ReadKey();
                            Console.SetCursorPosition(17, 13);
                            Console.Write(new string(' ', 100));
                            Console.SetCursorPosition(0, 13 + 1);
                            Console.Write(new string(' ', 200));
                            Console.SetCursorPosition(17, 13);
                        }
                        else
                            break;
                    }
                }

                else //player 0
                {
                    Console.WriteLine("\nPlayer O's turn: ");
                    Console.SetCursorPosition(17, 13);
                    Console.WriteLine("\nPlayer O is thinking. . .");
                    Thread.Sleep(2000);

                    List<int[]> boardCoords = possibleMoves();
                    while (true)
                    {
                        num = rnd.Next(0, boardCoords.Count);
                        x = boardCoords[num][0];
                        y = boardCoords[num][1];

                        if (_board[x, y] == "-")
                        {
                            currMove = x.ToString() + "-" + y.ToString();
                            moveSplit = currMove.Split('-');
                            break;
                        }
                        else
                            boardCoords.RemoveAt(num);
                    }
                }
            }
            else
            {
                if (_playerTurn % 2 != 0) //player O
                {
                    Console.WriteLine("\nPlayer O's turn: ");
                    Console.SetCursorPosition(17, 13);
                    Console.WriteLine("\nPlayer O is thinking. . .");
                    Thread.Sleep(2000);

                    List<int[]> boardCoords = possibleMoves();
                    while (true)
                    {
                        num = rnd.Next(0, boardCoords.Count);
                        x = boardCoords[num][0];
                        y = boardCoords[num][1];

                        if (_board[x, y] == "-")
                        {
                            currMove = x.ToString() + "-" + y.ToString();
                            moveSplit = currMove.Split('-');
                            break;
                        }
                        else
                            boardCoords.RemoveAt(num);
                    }
                }
                else
                {
                    Console.WriteLine("\nPlayer X's turn: ");
                    Console.SetCursorPosition(17, 13);
                    while (true)
                    {
                        currMove = Console.ReadLine();
                        moveSplit = currMove.Split('-');
                        if (moveSplit.Length != 2 ||
                          !int.TryParse(moveSplit[0], out x) ||
                          !int.TryParse(moveSplit[1], out y))
                        {
                            x = -1;
                            y = -1;
                        }

                        if (x < 0 || x > 2 || y < 0 || y > 2)
                        {
                            Console.WriteLine("{0} is not a valid input. . . Press any key to continue. . .", currMove);
                            Console.ReadKey();
                            Console.SetCursorPosition(17, 13);
                            Console.Write(new string(' ', 100));
                            Console.SetCursorPosition(0, 13 + 1);
                            Console.Write(new string(' ', 200));
                            Console.SetCursorPosition(17, 13);
                        }
                        else
                            break;
                    }
                }
            }
            turn = assignMove(turn, moveSplit);

            return turn;
        }
        static int assignMove(int turn, string[] moveSplit)
        {
            int x = int.Parse(moveSplit[0]);
            int y = int.Parse(moveSplit[1]);

            if (turn % 2 == 0) //player X
            {
                if (!_board[x, y].Contains("X") && !_board[x, y].Contains("O"))
                {
                    _board[x, y] = "X";
                    moveHistory(x, y, "Player X", "X");
                }
            }
            else //player O
            {
                _board[x, y] = "O";
                moveHistory(x, y, "Player O", "O");
            }
            return turn;
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
        static List<int[]> possibleMoves()
        {
            List<int[]>possibleMoves = new List<int[]>();
            int[] coords = new int[] { };

            for(int a = 0; a < 3; a++)
            {
                for(int b = 0; b < 3; b++)
                {
                    coords = new int[] { a, b };
                    possibleMoves.Add(coords);
                }
            }
            
            return possibleMoves;
        }
        static bool winFlag(string player) 
        {
            bool flag = true;
            
            while(flag)
            {
                if (_playerTurn % 2 == 0)
                    player = "X";
                else
                    player = "O";

                for (int x = 0; x < _board.GetLength(0); x++)
                {
                    if (_board[x, 0] == player && _board[x, 1] == player && _board[x, 2] == player
                     || _board[0, x] == player && _board[1, x] == player && _board[2, x] == player) //column
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
                break;
            }

            return flag;
        }
        static bool roundChecker()
        {
            if (_roundCount > 5 || _pX == 3 || _pO == 3)
            {
                writeToFile("Move History.txt");
                return false;
            }
            return true;
        }
        static void displayWinner(string winner)
        {
            if (winner == " ")
            {
                if (_playerTurn % 2 == 0)
                {
                    winner = "X";
                }
                else
                {
                    winner = "O";
                }
            }
            else
                winner = "D";

            if (winner.Contains("X"))
            {
                Console.WriteLine("Player X wins!");
                _moveHistory.Add("Player X wins");
                _moveHistory.Add("\n");
                _pX++;
                _roundCount++;
            }

            else if (winner.Contains("O"))
            {
                Console.WriteLine("Player O wins!");
                _moveHistory.Add("Player O wins");
                _moveHistory.Add("\n");
                _pO++;
                _roundCount++;
            }
            else
            {
                Console.WriteLine("Draw!");
                _moveHistory.Add("Draw for this rounds");
                _moveHistory.Add("\n");
                _roundCount++;
            }
            _playerTurn++;
            
            if(_pX == 3)
            {
                Console.Clear();
                displayBoard();
                Console.WriteLine("Player X beat Player O in Tic Tac Toe!");
                _moveHistory.Add("Player X wins VS Player O");
                _moveHistory.Add("\n");
            }
            else if(_pO == 3)
            {
                Console.Clear();
                displayBoard();
                Console.WriteLine("Player O beat Player X in Tic Tac Toe!");
                _moveHistory.Add("Player O wins VS Player X");
                _moveHistory.Add("\n");
            }

            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
        static void moveHistory (int x, int y, string player, string symbol)
        {
            string history = player + " placed " + symbol + " at (" + x + ", " + y + ")";
            _moveHistory.Add(history);
        }
        static void writeToFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string move in _moveHistory)
                {
                    sw.WriteLine(move);
                }
            }
        }
    }
}
