using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Computer
    {

        private int RowCount;
        private int[,] Gameboard;


        public Computer(int RowCount, int[,] Gameboard)
        {
            this.RowCount = RowCount;
            this.Gameboard = Gameboard;
        }

        /* https://github.com/MeStock/nim/blob/master/nim/Bot.cs
         
            inspirartion taken from this Githud code
         */


        private int CalculateNimSum()
        {
            int _nimSum = Gameboard[0, 1];
            for (int i = 1; i < RowCount; i++)
            {
                _nimSum ^= Gameboard[i, 1];
            }
            return _nimSum; //XOR calculation 
        }


        public (int,int) MakingAMove()
        {

            int _nimSum = CalculateNimSum(); // calculating NimSum

            int[] remainingObjects = new int[RowCount]; // declaring sized of array of row count

            for (int i = 0; i < RowCount; i++)
            {
                remainingObjects[i] = Gameboard[i, 1]; // adding game objects to an array to check later on
            }

            if (_nimSum != 0)
            {
                for (int i = 0; i < remainingObjects.Length; i++)
                {
                    if ((remainingObjects[i] ^ _nimSum) < remainingObjects[i]) // checking if remaining objects are greater then the nimsum check
                    {
                        int amountToRemove = remainingObjects[i] - (remainingObjects[i] ^ _nimSum); 

                        
                        //Update Objects in NimGame
                        return (amountToRemove, i);
                    }                    
                }
                
            }
            else
            {
                //remove random amount as it can't get a nim sum of 0, so it will try anything to get out of a loosing position

                int rowSelected = 0;
                //choosing random row
                while (true)
                {
                    rowSelected = RandomNumber(0, RowCount);
                    if (rowSelected != 0) //Checking if row isn't 0 , if it is will will chose a new row number
                    {
                        break;
                    }
                }

                int sticksToRemove = RandomNumber(1, remainingObjects[rowSelected] + 1); // selecting random amount of sticks in row we randomly slected
                
                return (sticksToRemove, rowSelected + 1); //returning that to remove in game
            }

            return (1, 1);
        }

        private Random _generateRandom = new Random(); // random number generator 

        public int RandomNumber(int min, int max)
        {
            return _generateRandom.Next(min, max); //generating a random number in a range
        }

    }
}
