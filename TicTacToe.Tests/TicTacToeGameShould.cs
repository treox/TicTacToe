using System;
using Xunit;

namespace TicTacToe.Tests
{
    public class TicTacToeGameShould
    {
        [Fact]
        public void UpdateGrid()
        {
            int testSquareNumber = 5;
            string testPlayerMarker = "X";
            string testGrid = "[1][2][3]\n[4][5][6]\n[7][8][9]\n";
            string expectedGrid = "[1][2][3]\n[4][X][6]\n[7][8][9]\n";

            string actualGrid = Grid.UpdateGrid(testSquareNumber, testPlayerMarker, testGrid);

            Assert.Equal(expectedGrid, actualGrid);
        }

        [Fact]
        public void NotUpdateGridIfMarkerIsNotValid()
        {
            int testSquareNumber = 5;
            string testPlayerMarker = "";
            string testGrid = "[1][2][3]\n[4][5][6]\n[7][8][9]\n";
            string expectedGrid = "[1][2][3]\n[4][5][6]\n[7][8][9]\n";

            string actualGrid = Grid.UpdateGrid(testSquareNumber, testPlayerMarker, testGrid);

            Assert.Equal(expectedGrid, actualGrid);
        }

        [Fact]
        public void ReturnTrueIfSquareIsMarked()
        {
            int testSquare = 1;
            string testGrid = "[X][2][3]\n[4][5][6]\n[7][8][9]\n";

            bool marked = Grid.CheckIfSquareIsMarked(testSquare, testGrid);

            Assert.True(marked);
        }

        [Fact]
        public void ReturnFalseIfSquareIsNotMarked()
        {
            int testSquare = 1;
            string testGrid = "[1][2][3]\n[4][5][6]\n[7][8][9]\n";
        
            bool marked = Grid.CheckIfSquareIsMarked(testSquare, testGrid);
        
            Assert.False(marked);
        }

        [Fact]
        public void Return0IfGameIsNotOverOrReset()
        {
            int testGridNumber = 5;
            string testPlayerMarker = "X";
            int expectedReturnValue = 0;

            int actualReturnValue = WinningConditions.CheckIfPlayerWon(testGridNumber, testPlayerMarker);

            Assert.Equal(expectedReturnValue, actualReturnValue);
        }

        [Fact]
        public void ReturnTrueIfAllSquaresAreMarked()
        {
            char[] testArray = new char[9] { 'X', 'O', 'X', 'X', 'O', 'O', 'X', 'X', 'O', };

            bool actualBool = WinningConditions.CheckIfAllSquaresAreMarked(testArray);

            Assert.True(actualBool);
        }

        [Fact]
        public void ReturnFalseIfAllSquaresAreNotMarked()
        {
            char[] testArray = new char[9] { 'X', 'O', 'X', 'X', '\0', 'O', 'X', 'X', 'O', };

            bool actualBool = WinningConditions.CheckIfAllSquaresAreMarked(testArray);

            Assert.False(actualBool);
        }
    }
}
