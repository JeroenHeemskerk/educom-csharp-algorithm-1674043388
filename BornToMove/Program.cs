using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace BornToMove
{
    public class Program
    {
        private int rating { get; set; }

        public static void Main(string[] args)
        {
            var Program = new Program();
            Console.WriteLine("The user needs to move.");
            Console.WriteLine("Type 1 to get a suggestion or type anything else to choose an option of the list.");
            Console.WriteLine();
            String choice = Console.ReadLine();
            Console.WriteLine();
            if (choice == "1")
            {
                Console.WriteLine("You have chosen to get a suggestion.");
                Console.WriteLine();
                var ConnectToMySql = new ConnectToMySql();
                Console.WriteLine();
                //ConnectToMySql.ReadAllData();
                ConnectToMySql.GetRandomMove();
                Program.UserRating();
            }
            else
            {
                Console.WriteLine("You have chosen to choose an option of the list.");
                Console.WriteLine();
                var ConnectToMySql = new ConnectToMySql();
                var moves = ConnectToMySql.ReadAllOptions();
                var number = Program.PickMoveOptionFromList(moves);
                if (number != 0)
                {
                    var id = moves[number].Id;
                    ConnectToMySql.GetChosenMoveById(id);
                    Program.UserRating();
                } else 
                {
                    Program.MakeNewMove(ConnectToMySql, moves);
                }
            }
        }

        public void UserRating()
        {
            Console.WriteLine("When you are done with the move pls rate it from 1 to 5.");
            Console.WriteLine();
            var moveRating = Console.ReadLine();
            Console.WriteLine();
            try
            {
                rating = Convert.ToInt32(moveRating);
                if (rating > 5 || 0 > rating)
                {
                    throw new Exception();
                }

            }
            catch
            {
                rating = RatingError();
            }
            Console.WriteLine($"You have given the move a rating of {rating}.");
            Console.WriteLine();
            Console.WriteLine("Please also rate the intensitie from 1 to 5.");
            Console.WriteLine();
            var intensitieRating = Console.ReadLine();
            Console.WriteLine();
            try
            {
                rating = Convert.ToInt32(intensitieRating);
                if (rating > 5 || 0 > rating)
                {
                    throw new Exception();
                }

            }
            catch
            {
                rating = RatingError();
            }
            Console.WriteLine($"You have given the intensitie a rating of {rating}.");
            Console.WriteLine();

        }

        public int RatingError()
        {
            try
            {
                Console.WriteLine("The rating needs to be a number in the range of 1 to 5");
                Console.WriteLine("Please give a new rating from 1 to 5");
                Console.WriteLine();
                var ratingInput = Console.ReadLine();
                Console.WriteLine();
                rating = int.Parse(ratingInput);
                if (this.rating>5 || 0> this.rating)
                {
                    throw new Exception();
                }
                return this.rating;
            } catch
            {
                rating = RatingError();
                return rating;
            }
        }

        public int PickMoveOptionFromList(Dictionary<int, Move> moves)
        {
            int number;
            Console.WriteLine("Pick a number from the the list to get that move.");
            Console.WriteLine("If you choose the number 0 you can make a new move");
            Console.WriteLine();
            var numberInput = Console.ReadLine();
            Console.WriteLine();
            try
            {
                number = int.Parse(numberInput);
                if (!(moves.Keys.Contains(number) || number == 0)) 
                {
                    throw new Exception();
                }
            }
            catch 
            {
                number = ErrorMoveNumber(moves);
            }
            Console.WriteLine($"You have chosen the number: {number}");
            Console.WriteLine();
            return number;
        }

        public int ErrorMoveNumber(Dictionary<int, Move> moves)
        {
            int number;
            Console.WriteLine("Please choose a valid number.");
            Console.WriteLine("That is a number from the given options or 0 to add your own move if that does not exist");
            Console.WriteLine();
            try
            {
                number = int.Parse(Console.ReadLine());
                if (!(moves.Keys.Contains(number) || number == 0))
                {
                    throw new Exception();
                }
            } catch
            {
                number =ErrorMoveNumber(moves);
            }
            return number;
        }

        public void MakeNewMove(ConnectToMySql connectToMySql, Dictionary<int, Move> moves)
        {
            Console.WriteLine("Type the name of the new move.");
            Console.WriteLine();
            var moveName = Console.ReadLine();
            Console.WriteLine();

            List<string> excistingMoveNames = new List<string>();
            foreach (Move move in moves.Values)
            {
                excistingMoveNames.Add(move.Name);
            }

            while (excistingMoveNames.Contains(moveName))
            {
                Console.WriteLine($"The name: {moveName} already excists. Choose a new name");
                Console.WriteLine();
                moveName= Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine($"Type the description of the move: {moveName}.");
            Console.WriteLine();
            var moveDescription = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Type the sweatRate. It has to be a number ranging from 1 to 5.");
            Console.WriteLine();
            int sweatRateNumber;
            try
            {
                sweatRateNumber = int.Parse(Console.ReadLine());
                Console.WriteLine();
                if (sweatRateNumber > 5 || sweatRateNumber < 1)
                {
                    throw new Exception();
                }
            } catch
            {
                sweatRateNumber = ErrorNewSweatRateNumber();
            }

            connectToMySql.InsertNewMove(moveName, moveDescription, sweatRateNumber);
            


        }

        public int ErrorNewSweatRateNumber()
        {
            int number;
            Console.WriteLine("Please choose a valid sweatRate number.");
            Console.WriteLine("That is a number ranging from 1 to 5");
            Console.WriteLine();
            try
            {
                number = int.Parse(Console.ReadLine());
                Console.WriteLine();
                if (number > 5|| number < 1)
                {
                    throw new Exception();
                }
            }
            catch
            {
                number = ErrorNewSweatRateNumber();
            }
            return number;
        }
    }
}