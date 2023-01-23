using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using BornToMove.Business;
using BornToMove.DAL;

namespace BornToMove
{
    public class Program
    {
        private int rating { get; set; }
        private BuMove buMove;

        public Program()
        {
            this.buMove = new BuMove();
        }

        public static void Main(string[] args)
        {
            var program = new Program();
            Console.WriteLine("The user needs to move.");
            Console.WriteLine("Type 1 to get a suggestion or type anything else to choose an option of the list.");
            Console.WriteLine();
            String choice = Console.ReadLine();
            Console.WriteLine();
            if (choice == "1")
            {
                Console.WriteLine("You have chosen to get a suggestion.");
                Console.WriteLine();
                //var ConnectToMySql = new ConnectToMySql();
                //Console.WriteLine();
                //ConnectToMySql.ReadAllData();
                //ConnectToMySql.GetRandomMove();
                var move = program.GetRandomMove();
                move.ShowAll();
                program.UserRating();
            }
            else
            {
                Console.WriteLine("You have chosen to choose an option of the list.");
                Console.WriteLine();
                //var ConnectToMySql = new ConnectToMySql();
                //var moves = ConnectToMySql.ReadAllOptions();
                //var number = Program.PickMoveOptionFromList(moves);
                var moves = program.GetAllMovesDictionary();
                program.ShowAllMoves(moves);
                var number = program.PickMoveOptionFromList(moves);
                if (number != 0)
                {
                    var id = moves[number].Id;
                    var move = program.GetMoveById(id);
                    move.ShowAll();
                    program.UserRating();
                } else 
                {
                    program.MakeNewMove();
                }
            }
        }

        private int AskForNumber(int min, int max) 
        {
            var str = Console.ReadLine();
            int value;
            while (!int.TryParse(str, out value) || value < min || value > max) 
            {
                Console.WriteLine($"Number: {value} is not correct. It should be a number between {min} and {max}. Please enter a valid number.");
                str= Console.ReadLine();
            }
            return value;
        }

        private Move GetRandomMove()
        {
            var move = buMove.GetRandomMove();
            return move;
        }

        private Dictionary<int, Move> GetAllMovesDictionary()
        {
            int number = 1;
            var result = new Dictionary<int, Move>();
            var moves = buMove.GetListAllMoves();
            foreach ( var move in moves )
            {
                result.Add(number, move);
                number++;
            }
            return result;
        }

        private void ShowAllMoves (Dictionary<int, Move> moves)
        {
            foreach (KeyValuePair<int, Move> element in moves)
            {
                Console.WriteLine("Number: " + element.Key);
                element.Value.ShowMoveName();
                element.Value.ShowMoveSweatRate();
                Console.WriteLine();
            }
        }

        private Move GetMoveById(int id)
        {
            var move = buMove.GetMoveById(id);
            return move;
        }

        public void UserRating()
        {
            Console.WriteLine("When you are done with the move pls rate it from 1 to 5.");
            Console.WriteLine();
            var moveRating = AskForNumber(1, 5);
            Console.WriteLine($"You have given the move a rating of {moveRating}.");
            Console.WriteLine();
            Console.WriteLine("Please also rate the intensitie from 1 to 5.");
            Console.WriteLine();
            var intensitieRating = AskForNumber(1, 5);
            Console.WriteLine($"You have given the intensitie a rating of {intensitieRating}.");
            Console.WriteLine();

        }

        public int PickMoveOptionFromList(Dictionary<int, Move> moves)
        {
            Console.WriteLine("Pick a number from the the list to get that move.");
            Console.WriteLine("If you choose the number 0 you can make a new move");
            Console.WriteLine();
            var numberInput = AskForNumber(0, moves.Count);
            Console.WriteLine($"You have chosen the number: {numberInput}");
            Console.WriteLine();
            return numberInput;
        }


        public void MakeNewMove()
        {
            Console.WriteLine("Type the name of the new move.");
            Console.WriteLine();
            var moveName = Console.ReadLine();
            Console.WriteLine();

            while (moveName == null)
            {
                Console.WriteLine("The name can not be empty. Choose a name");
                Console.WriteLine();
                moveName= Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine($"Type the description of the move: {moveName}.");
            Console.WriteLine();
            var moveDescription = Console.ReadLine();
            Console.WriteLine();
            while (moveDescription == null)
            {
                Console.WriteLine("The description can not be empty. Choose description");
                Console.WriteLine();
                moveDescription = Console.ReadLine();
                Console.WriteLine();
            }

            Console.WriteLine("Type the sweatRate. It has to be a number ranging from 1 to 5.");
            Console.WriteLine();
            int sweatRateNumber = AskForNumber(1, 5);

            bool succes = buMove.CreateNewMove(moveName, moveDescription, sweatRateNumber);

            while (succes == false)
            {
                Console.WriteLine("The name: {moveName} is already taken. Choose a new name");
                moveName = Console.ReadLine();
                while (moveName == null)
                {
                    Console.WriteLine("The name can not be empty. Choose a name");
                    Console.WriteLine();
                    moveName = Console.ReadLine();
                    Console.WriteLine();
                }
                succes = buMove.CreateNewMove(moveName, moveDescription, sweatRateNumber);
            }
        }
    }
}