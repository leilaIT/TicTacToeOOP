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

        //OK - I wanna add color for each player heheh

        static char[,] _board = new char[3, 3];
        static List<string> _moveHistory = new List<string>();
        static int _playerTurn = 0;
        static int _pX = 0;
        static int _pO = 0;
        static int _roundCount = 0;
        static Random _rnd = new Random();
        static void Main(string[] args)
        {   
            while (roundChecker())
            {
                gameStart();
            }
        }
        static bool roundChecker()
        {
            if (_roundCount > 5)
            {
                writeToFile("Move History.txt");
                displayWinner('D');
                return false;
            }
            else if(_pX == 3 || _pO == 3)
            {
                writeToFile("Move History.txt");
                return false;
            }
            return true;
        }
        static bool gameStart()
        {
            bool flag = true;
            string add = "Moves in Round " + _roundCount;
            
            iniBoard();
            _moveHistory.Add(add);

            if(_roundCount % 2 == 0)
                _playerTurn = 0;
            else
                _playerTurn = 1;
            
            while (flag)
            {
                move();
                displayBoard();
                if (winFlag()) //no winner yet
                {
                    _playerTurn++;
                    if(spacesLeft() == 0)
                    {
                        displayWinner('D');
                        return false;
                    }
                }
                else //if may nanalo
                {
                    displayWinner(' ');
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
                    _board[x, y] = ' ';
                }
            }
            displayBoard();
        }
        static void displayBoard()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            if(_roundCount > 5)
                Console.Write("Round: 5");
            else
                Console.Write("Round: " + _roundCount);
            Console.ResetColor();
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Player X: " +  _pX);
            Console.ResetColor();
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Player O: " + _pO);
            
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n\n  0   1   2");
            Console.ResetColor();
            Console.WriteLine("-------------");
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                Console.Write("| ");
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y] == 'X')
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    else if (_board[x, y] == 'O')
                        Console.ForegroundColor= ConsoleColor.DarkRed;

                    Console.Write(_board[x, y]);
                    Console.ResetColor();
                    Console.Write(" | ");
                    if(y == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(x);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine("\n-------------");
            }
        }
        static void move()
        {
            string currMove = "";
            string[] moveSplit = new string[] { };
            int x = 0;
            int y = 0;
            int num = 0;

            Console.WriteLine("Please enter valid coordinates using the following format x-y" +
                             "\nx is the column number (0-2)" +
                             "\ny is the row number (0-2)");

            if (_roundCount % 2 == 0) //if current round even, player x first
            {
                moveSplit = evenRound(currMove, moveSplit, x, y, num);
            }
            else //if current round odd, computer goes first
            {
                moveSplit = oddRound(currMove, moveSplit, x, y, num);
            }
            assignMove(moveSplit);
        }
        static string[] evenRound (string currMove, string[] moveSplit, int x, int y, int num)
        {
            if (_playerTurn % 2 == 0) //player X
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nPlayer X's turn: ");
                Console.ResetColor();
                Console.SetCursorPosition(17, 14);
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

                    if (x < 0 || x > 2 || y < 0 || y > 2 ||
                       _board[x, y] == 'X' || _board[x, y] == 'O')
                    {
                        Console.WriteLine("{0} is not a valid input. . . Press any key to continue. . .", currMove);
                        Console.ReadKey();
                        Console.SetCursorPosition(17, 14);
                        Console.Write(new string(' ', 100));
                        Console.SetCursorPosition(0, 14 + 1);
                        Console.Write(new string(' ', 200));
                        Console.SetCursorPosition(17, 14);
                    }
                    else
                        break;
                }
            }

            else //player 0
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nPlayer O's turn: ");
                Console.SetCursorPosition(17, 14);
                Console.WriteLine("\nPlayer O is thinking. . .");
                Console.ResetColor();
                Thread.Sleep(2000);

                List<int[]> boardCoords = possibleMoves();
                while (true)
                {
                    x = -1;
                    y = -1;
                    if (_playerTurn == 1) //cpu second move
                    {
                        if (_board[1, 1] == ' ') //take center
                        {
                            x = 1;
                            y = 1;
                        }
                    }
                    else //not second move anymore
                    {
                        //row defense
                        for (int i = 0; i < _board.GetLength(0); i++)
                        {
                            if (_board[i, 0] == ' ' || _board[i, 1] == ' ' || _board[i, 2] == ' ')
                            {
                                if (_board[i, 0] == 'X' && _board[i, 1] == 'X' && _board[i, 2] == ' ')
                                {
                                    x = i;
                                    y = 2;
                                }
                                else if (_board[i, 0] == 'X' && _board[i, 1] == ' ' && _board[i, 2] == 'X')
                                {
                                    x = i;
                                    y = 1;
                                }
                                else if (_board[i, 0] == ' ' && _board[i, 1] == 'X' && _board[i, 2] == 'X')
                                {
                                    x = i;
                                    y = 0;
                                }
                            }

                            if (x != -1 && y != -1)
                                break;
                        }

                        //column defense
                        for (int j = 0; j < _board.GetLength(1); j++)
                        {
                            if (_board[0, j] == ' ' || _board[1, j] == ' ' || _board[2, j] == ' ')
                            {
                                if (_board[0, j] == 'X' && _board[1, j] == 'X' && _board[2, j] == ' ')
                                {
                                    x = 2;
                                    y = j;
                                }
                                else if (_board[0, j] == 'X' && _board[1, j] == ' ' && _board[2, j] == 'X')
                                {
                                    x = 1;
                                    y = j;
                                }
                                else if (_board[0, j] == ' ' && _board[1, j] == 'X' && _board[2, j] == 'X')
                                {
                                    x = 0;
                                    y = j;
                                }
                            }

                            if (x != -1 && y != -1)
                                break;
                        }

                        //row offense
                        for (int i = 0; i < _board.GetLength(0); i++)
                        {
                            if (_board[i, 0] == 'O' && _board[i, 1] == 'O' && _board[i, 2] == ' ')
                            {
                                x = i;
                                y = 2;
                            }
                            else if (_board[i, 0] == 'O' && _board[i, 1] == ' ' && _board[i, 2] == 'O')
                            {
                                x = i;
                                y = 1;
                            }
                            else if (_board[i, 0] == ' ' && _board[i, 1] == 'O' && _board[i, 2] == 'O')
                            {
                                x = i;
                                y = 0;
                            }
                        }

                        //column offense
                        for (int j = 0; j < _board.GetLength(1); j++)
                        {
                            if (_board[0, j] == 'O' && _board[1, j] == 'O' && _board[2, j] == ' ')
                            {
                                x = 2;
                                y = j;
                            }
                            else if (_board[0, j] == 'O' && _board[1, j] == ' ' && _board[2, j] == 'O')
                            {
                                x = 1;
                                y = j;
                            }
                            else if (_board[0, j] == ' ' && _board[1, j] == 'O' && _board[2, j] == 'O')
                            {
                                x = 0;
                                y = j;
                            }
                        }
                    }

                    if (x == -1 && y == -1)
                    {
                        num = _rnd.Next(0, boardCoords.Count);
                        x = boardCoords[num][0];
                        y = boardCoords[num][1];
                    }
                    if (_board[x, y] == ' ')
                    {
                        currMove = x.ToString() + "-" + y.ToString();
                        moveSplit = currMove.Split('-');
                        break;
                    }
                    else
                        boardCoords.RemoveAt(num);
                }
            }
            return moveSplit;
        }
        static string[] oddRound(string currMove, string[] moveSplit, int x, int y, int num)
        {
            if (_playerTurn % 2 != 0) //player O
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nPlayer O's turn: ");
                Console.SetCursorPosition(17, 14);
                Console.WriteLine("\nPlayer O is thinking. . .");
                Console.ResetColor();
                Thread.Sleep(2000);

                List<int[]> boardCoords = possibleMoves();
                while (true)
                {
                    x = -1;
                    y = -1;
                    if (_playerTurn == 1) //cpu second move
                    {
                        if (_board[1, 1] == ' ') //take center
                        {
                            x = 1;
                            y = 1;
                        }
                    }
                    else //not second move anymore
                    {
                        //row defense
                        for (int i = 0; i < _board.GetLength(0); i++)
                        {
                            if (_board[i, 0] == ' ' || _board[i, 1] == ' ' || _board[i, 2] == ' ')
                            {
                                if (_board[i, 0] == 'X' && _board[i, 1] == 'X' && _board[i, 2] == ' ')
                                {
                                    x = i;
                                    y = 2;
                                }
                                else if (_board[i, 0] == 'X' && _board[i, 1] == ' ' && _board[i, 2] == 'X')
                                {
                                    x = i;
                                    y = 1;
                                }
                                else if (_board[i, 0] == ' ' && _board[i, 1] == 'X' && _board[i, 2] == 'X')
                                {
                                    x = i;
                                    y = 0;
                                }
                            }

                            if (x != -1 && y != -1)
                                break;
                        }

                        //column defense
                        for (int j = 0; j < _board.GetLength(1); j++)
                        {
                            if (_board[0, j] == ' ' || _board[1, j] == ' ' || _board[2, j] == ' ')
                            {
                                if (_board[0, j] == 'X' && _board[1, j] == 'X' && _board[2, j] == ' ')
                                {
                                    x = 2;
                                    y = j;
                                }
                                else if (_board[0, j] == 'X' && _board[1, j] == ' ' && _board[2, j] == 'X')
                                {
                                    x = 1;
                                    y = j;
                                }
                                else if (_board[0, j] == ' ' && _board[1, j] == 'X' && _board[2, j] == 'X')
                                {
                                    x = 0;
                                    y = j;
                                }
                            }

                            if (x != -1 && y != -1)
                                break;
                        }

                        //row offense
                        for (int i = 0; i < _board.GetLength(0); i++)
                        {
                            if (_board[i, 0] == 'O' && _board[i, 1] == 'O' && _board[i, 2] == ' ')
                            {
                                x = i;
                                y = 2;
                            }
                            else if (_board[i, 0] == 'O' && _board[i, 1] == ' ' && _board[i, 2] == 'O')
                            {
                                x = i;
                                y = 1;
                            }
                            else if (_board[i, 0] == ' ' && _board[i, 1] == 'O' && _board[i, 2] == 'O')
                            {
                                x = i;
                                y = 0;
                            }
                        }

                        //column offense
                        for (int j = 0; j < _board.GetLength(1); j++)
                        {
                            if (_board[0, j] == 'O' && _board[1, j] == 'O' && _board[2, j] == ' ')
                            {
                                x = 2;
                                y = j;
                            }
                            else if (_board[0, j] == 'O' && _board[1, j] == ' ' && _board[2, j] == 'O')
                            {
                                x = 1;
                                y = j;
                            }
                            else if (_board[0, j] == ' ' && _board[1, j] == 'O' && _board[2, j] == 'O')
                            {
                                x = 0;
                                y = j;
                            }
                        }
                    }

                    if (x == -1 && y == -1)
                    {
                        num = _rnd.Next(0, boardCoords.Count);
                        x = boardCoords[num][0];
                        y = boardCoords[num][1];
                    }

                    if (_board[x, y] == ' ')
                    {
                        currMove = x.ToString() + "-" + y.ToString();
                        moveSplit = currMove.Split('-');
                        break;
                    }
                    else
                    {
                        boardCoords.RemoveAt(num);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nPlayer X's turn: ");
                Console.ResetColor();
                Console.SetCursorPosition(17, 14);
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

                    if (x < 0 || x > 2 || y < 0 || y > 2 ||
                        _board[x, y] == 'X' || _board[x, y] == 'O')
                    {
                        Console.WriteLine("{0} is not a valid input. . . Press any key to continue. . .", currMove);
                        Console.ReadKey();
                        Console.SetCursorPosition(17, 14);
                        Console.Write(new string(' ', 100));
                        Console.SetCursorPosition(0, 14 + 1);
                        Console.Write(new string(' ', 200));
                        Console.SetCursorPosition(17, 14);
                    }
                    else
                        break;
                }
            }
            return moveSplit;
        }
        static void assignMove(string[] moveSplit)
        {
            int x = int.Parse(moveSplit[0]);
            int y = int.Parse(moveSplit[1]);

            if (_playerTurn % 2 == 0) //player X
            {
                    _board[x, y] = 'X';
                    moveHistory(x, y, "Player X", "X");
            }
            else //player O
            {
                _board[x, y] = 'O';
                moveHistory(x, y, "Player O", "O");
            }
        }
        static int spacesLeft ()
        {
            int spaceCount = 0;

            for(int x = 0; x < _board.GetLength(0); x++)  
            { 
                for(int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y] == ' ')  
                        spaceCount++;
                }
            }
            return spaceCount;
        }
        static List<int[]> possibleMoves()
        {
            List<int[]>possibleMoves = new List<int[]>();
            int[] coords = new int[] { };

            for(int x = 0; x < _board.GetLength(0); x++)
            {
                for(int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y] == ' ')
                    {
                        coords = new int[] { x, y };
                        possibleMoves.Add(coords);
                    }
                }
            }
            
            return possibleMoves;
        }
        static bool winFlag() 
        {
            char player = ' ';
            bool flag = true;
            
            while(flag)
            {
                if (_playerTurn % 2 == 0)
                    player = 'X';
                else
                    player = 'O';

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
        static void displayWinner(char winner)
        {
            if (winner == ' ')
            {
                if (_playerTurn % 2 == 0)
                    winner = 'X';
                else
                    winner = 'O';
            }
            else
                winner = 'D';

            if (winner == 'X')
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Player X wins!");
                Console.ResetColor();
                _moveHistory.Add("Player X wins");
                _moveHistory.Add("\n");
                _pX++;
                _roundCount++;
            }

            else if (winner == 'O')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Player O wins!");
                Console.ResetColor();
                _moveHistory.Add("Player O wins");
                _moveHistory.Add("\n");
                _pO++;
                _roundCount++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Draw!");
                Console.ResetColor();
                _moveHistory.Add("Draw for this round");
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
            else if(_roundCount > 5)
            {
                Console.Clear();
                displayBoard();
                Console.WriteLine("No one won :(");
                _moveHistory.Add("No one won :(");
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
