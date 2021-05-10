using System;
using System.IO;
using System.Linq;
using System.Text;



namespace ConsoleApp1
{
    class NimGame
    {
        
        private readonly Gamboard GameBoard = new Gamboard();

        /*
        Running Code of the Game
        */
        public void GameOfNim()
        {
            Renderer render = new Renderer(GameBoard.GameBoard());
            /*
                Run this in Program.CS 
                eg. 
                NimGame Nim = new NimGame();

                Nim.Begin();

            This will start the game, the code does the rest 

            */

            while (GameBoard._gameover == false) //gameover flag
            {
                render.WriteToConsole(1); // what player it is 1 or 2, 2 being the computer
                PlayerMovement(); //runs when the players want to move

                if (GameBoard._gameover == true)// checking if game is over
                {
                    Console.Clear();
                    Console.WriteLine("Player WON! Skynet isn't happening today!\n---------------------------- -\nRows | Column\n----------------------------"); //Player who has won
                    break;
                }

                DateTime OperationStart = DateTime.Now; // Checking Operation Speed

                int ObjectComparison = ComputerMovement();

                DateTime OperationEnd = DateTime.Now; // Ending timer on operation speed
                TimeSpan TotalTimeTaken = (OperationEnd - OperationStart);

                Console.WriteLine(TotalTimeTaken + " ms for operation to complete.\n");//Writing nthe operation time

                WriteToFile(TotalTimeTaken, ObjectComparison);

                Console.ReadKey();


                if (GameBoard._gameover == true) // checking if game is over
                {
                    Console.Clear();
                    Console.WriteLine("Computer has WON! Skynet is happening today!\n---------------------------- -\nRows | Column\n----------------------------"); //Player who has won
                    break;
                }



                for (int j = 0; j != GameBoard._gameBoard.GetLength(0); j++)
                {
                    render.WriteGameConsole(j);
                }

                Console.ReadKey();
            }
        }


        public int ComputerMovement()
        {
            Renderer render = new Renderer(GameBoard.GameBoard());

            Computer ComputerAI = new Computer(GameBoard.GetRowCount(),GameBoard.GameBoard()); //Declaring Computer to have Objects and items it needs 

            int tempRow = 0;
            int tempObjects = 0; // temp object and row value to print them to console
            Console.Clear();

            (tempObjects, tempRow) = ComputerAI.MakingAMove();

            GameBoard._gameBoard[tempRow, 1] -= tempObjects ; // removing slected amount of objects from the gameboard

            render.WriteToConsole(2);

            Console.WriteLine($"Computer removed {tempObjects} objects from pile {tempRow + 1}\nPress Any Key to continue...");

            GameOver(); // checking if game is over

            return tempObjects; // Returning Amount of Objects to compare with data. 
        }

        public void PlayerMovement() // Players Movement
        {

            Console.Write("\nPick a row you want to make a move at.\nRow: ");

            GameBoard._rowSelected = ParseInt(0, 1000); // values of 0, 1000 choosen as we are only testing if the next character is a int

            GameBoard._rowSelected = NumberCheck(0, GameBoard._gameBoard.GetLength(0) + 1, GameBoard._rowSelected - 1); // plus one because range is min inclusive and max is the limit

            while (true)
            {
                if (GameBoard.CheckRow(GameBoard._rowSelected))//checkin if row has any sticks or objects in it
                {
                    Console.Write("\nThe '{0}' row has no numbers in it. Please select a row with integers in it.\nPlease Check your entered integer and try again: ", GameBoard._rowSelected + 1);//error meesages if row has no integers                                   
                    GameBoard._rowSelected = (ParseInt(0, GameBoard._gameBoard.GetLength(0)) - 1);
                }
                else break;
            }


            Console.Write("\nPick how man elements you want to pick up (remove).\nElements: ");

            GameBoard._amountOfPiecesSelected = ParseInt(0, 1000); // values of 0, 1000 choosen as we are only testing if the next character is a int

            GameBoard._amountOfPiecesSelected = NumberCheck(1, GameBoard._gameBoard[GameBoard._rowSelected, 1], GameBoard._amountOfPiecesSelected);

            GameBoard._gameBoard[GameBoard._rowSelected, 1] -= GameBoard._amountOfPiecesSelected; // removing slected amount of objects from the gameboard

            GameOver(); // checking if game is over
        }

        public void GameOver()
        {
            int rowEmptyCount = 0; //row empty count
            for (int i = 0; i < GameBoard._gameBoard.GetLength(0); i++) // CHECKING IF GAME IS OVER IF ALL THE INTEGERS IN THE ARRAY ARE 0
            {
                if (GameBoard._gameBoard[i, 1] == 0) // checking all rows that have 0 in them 
                {
                    rowEmptyCount++; // adding to a count to check against length 
                    if (rowEmptyCount == GameBoard._gameBoard.GetLength(0)) GameBoard._gameover = true;
                }
            }
        }


        public void WriteToFile(TimeSpan _timeTaken, int _Operations )
        {

            string path = @"c:\DELETE\Assignment3Calculations.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Time/Operations");
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(_timeTaken); //appending time and operation to graph it to see the relation between the two
                sw.WriteLine(_Operations);
            }


        }


        /*
         
        Number and Integer Checks

         */
        private int NumberCheck(int min, int max, int checkableNumber)
        {

            GameBoard._rowCheck = false;

            while (true)
            {
                if (Enumerable.Range(min, max).Contains(checkableNumber)) return checkableNumber; // checking if number is in range
                else
                {
                    Console.Write("\n'{0}' is not within the given range, min: '{1}' or max: '{2}'.\nPlease Check your entered integer and try again: ", checkableNumber, min, max); //error meesages for incorrect integers 
                }


                checkableNumber = ParseInt(min, max); // iif number isn't in range it makes users input it again
            }
        }

        public int ParseInt(int min, int max) // checking if entered string is an integer
        {
            //local variables
            int checkableNumber = 0;
            string parseCheckableNumber;
            bool tryCheck = false;


            while (tryCheck == false)
            {
                parseCheckableNumber = Console.ReadLine(); //takes a string from console
                tryCheck = int.TryParse(parseCheckableNumber, out checkableNumber); //makes sure that element entered is a integer
                if (tryCheck == false)
                {
                    Console.Write("\n'{0}' is not an Integer or is greater then '{1}' or less then '{2}'.\nPlease Check your entered integer and try again: ", parseCheckableNumber, max, min); //error meesages for incorrect integers 
                }
            }
            return checkableNumber;
        }


    }
}
