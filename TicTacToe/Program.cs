using System;
using System.Text;

namespace TicTacToe
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool quitGame = false;

            do
            {
                Player1 player1 = new Player1();
                Player2 player2 = new Player2();
                StringBuilder gridBuilder = new StringBuilder();

                int[] keyCodes = { 49, 50, 51, 52, 53, 54, 55, 56, 57 };

                Array.Clear(WinningConditions.arrayXO, 0, WinningConditions.arrayXO.Length);
                WinningConditions.scorePlayer1 = 0;
                WinningConditions.scorePlayer2 = 0;

                bool isNotGameOverOrReset = true;
                int squareNumber = 0;
                bool isPlayer1sTurn = true;
                string playerMarker = "";
                string grid = "";

                Console.Clear();
                gridBuilder.Append("[1][2][3]\n");
                gridBuilder.Append("[4][5][6]\n");
                gridBuilder.Append("[7][8][9]\n");

                Console.WriteLine("Välkommen till Tic-Tac-Toe!");
                Console.WriteLine();
                Console.WriteLine(gridBuilder.ToString());

                do
                {
                    int convertedChosenSquare = 0;

                    try
                    {
                        Console.WriteLine("Välj vilken cell du vill markera genom att trycka på [1] - [9], återställ [r] eller avsluta [q]: ");
                        ConsoleKeyInfo chosenSquare = Console.ReadKey();

                        if (chosenSquare.Key == ConsoleKey.R)
                        {
                            isNotGameOverOrReset = false;
                            Console.WriteLine();
                            Console.WriteLine("Spelet återställs...");
                        }

                        if (chosenSquare.Key == ConsoleKey.Q)
                        {
                            isNotGameOverOrReset = false;
                            quitGame = true;

                            Console.WriteLine();
                            Console.WriteLine("Spelet avslutas...");
                        }

                        convertedChosenSquare = Convert.ToInt32(chosenSquare.Key - 48);
                        Console.WriteLine();
                    }
                    catch
                    {
                        Console.WriteLine("Fel format: Ange ett nummer mellan 1 - 9!");
                    }

                    if (convertedChosenSquare >= 1 && convertedChosenSquare <= 9)
                    {
                        squareNumber = convertedChosenSquare;

                        bool marked = Grid.CheckIfSquareIsMarked(squareNumber, gridBuilder.ToString());
                        if (!marked)
                        {
                            if (isPlayer1sTurn)
                            {
                                playerMarker = player1.MarkCell();
                                isPlayer1sTurn = false;
                            }
                            else
                            {
                                playerMarker = player2.MarkCell();
                                isPlayer1sTurn = true;
                            }

                            if (squareNumber != 0)
                            {
                                grid = Grid.UpdateGrid(squareNumber, playerMarker, gridBuilder.ToString());
                                gridBuilder.Clear();
                                gridBuilder.Append(grid);
                                Console.WriteLine(grid);
                                int check = WinningConditions.CheckIfPlayerWon(squareNumber, playerMarker);

                                switch (check)
                                {
                                    case 0:
                                        if (isPlayer1sTurn == true)
                                        {
                                            Console.WriteLine("Spelare 1s tur!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Spelare 2s tur!");
                                        }
                                        break;
                                    case 1:
                                        Console.WriteLine("Spelare 1 vann!");
                                        isNotGameOverOrReset = false;
                                        Console.WriteLine("tryck på valfri tangent för att fortsätta...");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.WriteLine("Spelare 2 vann!");
                                        isNotGameOverOrReset = false;
                                        Console.WriteLine("tryck på valfri tangent för att fortsätta...");
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        Console.WriteLine("Oabgjort. Ingen vann!");
                                        isNotGameOverOrReset = false;
                                        Console.WriteLine("tryck på valfri tangent för att fortsätta...");
                                        Console.ReadKey();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Fel format: Ange ett nummer mellan 1 - 9!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cellen är redan markerad. Välj en annan cell!");
                        }
                    }
                } while (isNotGameOverOrReset);
            } while (!quitGame);
        }
    }

    public static class Grid
    {
        public static string UpdateGrid(int square, string marker, string grid)
        {
            string strSquare = square.ToString();

            if(marker != "")
            {
                grid = grid.Replace(strSquare, marker);
            }

            return grid;
        }

        public static bool CheckIfSquareIsMarked(int square, string grid)
        {
            bool isMarked = false;

            if (!grid.Contains(square.ToString()))
                isMarked = true;

            return isMarked;
        }
    }

    public abstract class Player
    {
        public abstract string MarkCell();
    }

    public class Player1 : Player
    {
        public override string MarkCell()
        {
            return "X";
        }
    }

    public class Player2 : Player
    {
        public override string MarkCell()
        {
            return "O";
        }
    }

    public static class WinningConditions
    {
        private static int[,] winningConditions = new int[8, 3] { { 1, 2, 3 }, { 4, 5, 6 },{ 7, 8, 9 },{ 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } };
        public static char[] arrayXO = new char[9];

        public static int scorePlayer1 = 0;
        public static int scorePlayer2 = 0;

        public static int CheckIfPlayerWon(int number, string marker)
        {
            int[] conditionsToCompareWith = new int[3];
            char convertedMarker = Convert.ToChar(marker);
            arrayXO[number - 1] = convertedMarker;

            foreach(char ch in arrayXO)
            {
                Console.Write(ch + " ");
            }
            Console.WriteLine();

            int upperBound0 = winningConditions.GetUpperBound(0);
            int upperBound1 = winningConditions.GetUpperBound(1);

            int tempIndex = 0;
            for(int i = 0; i <= upperBound0; i++)
            {
                for(int j = 0; j <= upperBound1; j++)
                {
                    conditionsToCompareWith[tempIndex]= winningConditions[i, j];
                    tempIndex++;
                }
                
                foreach(int condition in conditionsToCompareWith)
                {
                    if(arrayXO[condition - 1] == 'X')
                    {
                        scorePlayer1 += 1;
                    }
                    else if(arrayXO[condition - 1] == 'O')
                    {
                        scorePlayer2 += 1;
                    }
                    else
                    {
                        scorePlayer1 += 0;
                        scorePlayer2 += 0;
                    }
                }
                tempIndex = 0;

                if(scorePlayer1 == 3 || scorePlayer2 == 3)
                {
                    break;
                }
                else
                {
                    scorePlayer1 = 0;
                    scorePlayer2 = 0;
                }
            }

            bool isAllMarked = CheckIfAllSquaresAreMarked(arrayXO);

            if(scorePlayer1 < 3 && scorePlayer2 < 3 && isAllMarked == false)
            {
                return 0;
            }
            else if(scorePlayer1 == 3)
            {
                return 1;
            }
            else if(scorePlayer2 == 3)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static bool CheckIfAllSquaresAreMarked(char[] array)
        {
            bool allSquaresMarked = false;
            int markedSquares = 0;

            int arrayIndex = 0;
            foreach(char marker in array)
            {
                if(array[arrayIndex] == 'X' || array[arrayIndex] == 'O')
                {
                    markedSquares += 1;
                }
                arrayIndex++;
            }

            if (markedSquares == 9)
                allSquaresMarked = true;

            return allSquaresMarked;
        }
    }
}
