using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Renderer
    {
        /*
        Rederering Elements of the Game
        */

        private int[,] _gameBoard;

        public Renderer(int[,] GameBoard)
        {
            this._gameBoard = GameBoard;   
        }

        public void WriteToConsole(int i) //Simple code to write to console 
        {
            Console.Clear();

            if (i == 1)
            {
                Console.WriteLine("Player " + (i) + " turn\n---------------------------- -\nRows | Column\n----------------------------"); // The player is playing currently
            }
            else 
            {
                Console.WriteLine("Computers turn\n---------------------------- -\nRows | Column\n----------------------------"); // Its the Computers Turn to play
            }
            

            for (int j = 0; j != _gameBoard.GetLength(0); j++)
            {
                WriteGameConsole(j);
            }
        }

        public void WriteGameConsole(int row)
        {
            for (int j = 0; j < _gameBoard.GetLength(1); j++) Console.Write(string.Format("|{0}| ", _gameBoard[row, j])); // printing in in matrix 
            Console.Write(Environment.NewLine);
            Console.WriteLine("----------------------------");
        }


    }
}
