using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Gamboard
    {
        private Random _generateRandom = new Random();

        public int[,] _gameBoard; // retrieving and setting 2d array values for the rows and columns for the board
        //private int _objects; // getting and setting random values for game objects Neverm used

        public int _rowSelected { get; set; } // what row the player has selected
        public int _amountOfPiecesSelected { get; set; }// how many elements in the game of nim you want to remove

        public int[] _zeroedRows { get; set; } // tracks the rows that have 0 in them so users can't access them anymore

        public bool _rowCheck { get; set; } // checks if row has no values

        public bool _gameover { get; set; } //Gameover status 

        public int _rowCount { get; set; } // Row count


        public Gamboard()
        {
            this._rowCount = RandomNumber(2, 5); // generating random amount of columns 
            this._gameBoard = new int[_rowCount, 2]; // declaring amount of columns in the game
            this._zeroedRows = new int[_rowCount]; // this will keep track of rows with no value

            this._gameover = false;

            startGame();

            this._amountOfPiecesSelected = 0;
            this._rowSelected = 0;
            this._zeroedRows[0] = 0;

        }

        public void startGame() // starting game and creating random rows and playing objects
        {
            for (int i = 0; i != _rowCount; i++)
            {
                _gameBoard[i, 0] = i + 1; // counting through cols
                _gameBoard[i, 1] = (2 * (RandomNumber(1, 6)) + 1); // adding random amount of game objects to game with formula of 2N + 1
            }
        }


        public int[] GetRows()
        {
            int[] _rowsRemaining = new int[_rowCount]; // declaring array the same size as a 2d gameboard
            for (int i = 0; i <= _gameBoard.GetLength(0); i++)
            {
                if (_gameBoard[i, 1] != 0)
                {
                    _rowsRemaining[i] = _gameBoard[i, 1]; // getting and returning rows when called for the computer to analisae 
                }

            }

            return _rowsRemaining;
        }


        // Functions for the bot

        public int GetRowCount() // getting row count
        {
            return _rowCount;
        }

        public int[,] GameBoard() // gets anmount of objects or sticks in the row
        {
            return _gameBoard;
        }

        public bool CheckRow(int row) // checking if row has sticks or objects avaliable 
        {
            for (int i = 0; i < _gameBoard.GetLength(0); i++)
            {
                if (_gameBoard[row, 1].Equals(0)) //Lazy way of checking if row is 0zed 
                {
                    _zeroedRows[i] = row - 1;
                    return true;
                }
            }

            return false;
        }

        public int RandomNumber(int min, int max)
        {
            return _generateRandom.Next(min, max); //generating a random number in a range
        }


    }
}
