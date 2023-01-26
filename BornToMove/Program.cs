using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;

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
                program.MoveShowAll(move);
                program.UserRating(move);
            }
            else
            {
                Console.WriteLine("You have chosen to choose an option of the list.");
                Console.WriteLine();
                //var ConnectToMySql = new ConnectToMySql();
                //var moves = ConnectToMySql.ReadAllOptions();
                //var number = Program.PickMoveOptionFromList(moves);
                var moves = program.GetAllMovesDictionary();
                //var moves = program.GetAllMoves();
                program.ShowAllMoves(moves);
                var number = program.PickMoveOptionFromList(moves);
                if (number != 0)
                {
                    var id = moves[number].Id;
                    var move = program.GetMoveById(id);
                    //program.UpdateMove(move);
                    program.MoveShowAll(move);
                    program.UserRating(move);
                } else 
                {
                    program.MakeNewMove();
                }
            }
        }

        private int AskForNumber(int min, int max) 
        {
            var str = Console.ReadLine();
            Console.WriteLine();
            int value;
            while (!int.TryParse(str, out value) || value < min || value > max) 
            {
                Console.WriteLine($"Value: {str} is not correct. It should be a number between {min} and {max}. Please enter a valid number.");
                Console.WriteLine();
                str= Console.ReadLine();
                Console.WriteLine();
            }
            return value;
        }

        private double AskForNumberDouble(double min, double max)
        {
            var str = Console.ReadLine();
            Console.WriteLine();
            double value;
            while (!double.TryParse(str, out value) || value < min || value > max)
            {
                // ToString("N1") is to have it show 1 decimal.
                Console.WriteLine($"Value: {str} is not correct. It should be a number between {min.ToString("N1")} and {max.ToString("N1")}. Please enter a valid number.");
                Console.WriteLine();
                str = Console.ReadLine();
                Console.WriteLine();
            }
            return value;
        }

        private string AskForString(string nameString)
        {
            var str = Console.ReadLine() ;
            Console.WriteLine();
            while (str.IsNullOrEmpty() )
            {
                Console.WriteLine($"The {nameString} can not be empty. Set a {nameString}.");
                Console.WriteLine() ;
                str= Console.ReadLine();
                Console.WriteLine();
            }
            return str;
        }

        private void MoveShowAll(Move move)
        {
            move.ShowAll();
        }

        private Move GetRandomMove()
        {
            var move = buMove.GetRandomMove();
            return move;
        }

        private Dictionary<int, Move> GetAllMovesDictionary()
        {
            //int number = 1;
            var moves = buMove.GetListAllMoves();
            var result = moves.ToDictionary(move => moves.IndexOf(move)+1, move => move);
            //foreach (var move in moves)
            //{
            //    result.Add(number, move);
            //    number++;
            //}
            return result;
        }

        private void ShowAllMoves(Dictionary<int, Move> moves)
        {
            foreach (KeyValuePair<int, Move> element in moves)
            {
                Console.WriteLine("Number: " + element.Key);
                element.Value.ShowMoveName();
                element.Value.ShowMoveSweatRate();
                element.Value.ShowMoveAverageRating();
                Console.WriteLine();
            }
        }

        //private List<Move> GetAllMoves()
        //{
        //    var moves = buMove.GetListAllMoves();
        //    return moves;
        //}

        //private void ShowAllMoves(List<Move> moves)
        //{
        //    var test = from move in moves
        //               select new { number = moves.IndexOf(move), name = move.Name, sweatRate = move.SweatRate, averageRating = buMove.GetAverageRatingByMoveId(move.Id) }
        //    ;
        //}

        private Move GetMoveById(int id)
        {
            var move = buMove.GetMoveById(id);
            return move;
        }

        public void UserRating(Move move)
        {
            Console.WriteLine("When you are done with the move, pls rate it from 1,0 to 5,0.");
            Console.WriteLine();
            var moveRating = AskForNumberDouble(1.0 , 5.0);
            Console.WriteLine($"You have given the move a rating of {moveRating}.");
            Console.WriteLine();
            Console.WriteLine("Please also rate the intensitie from 1,0 to 5,0.");
            Console.WriteLine();
            var intensitieRating = AskForNumberDouble(1.0 , 5.0);
            Console.WriteLine($"You have given the intensitie a rating of {intensitieRating}.");
            Console.WriteLine();
            buMove.AddRatingAndVote(move, moveRating, intensitieRating);

        }

        public int PickMoveOptionFromList(Dictionary<int, Move> moves)
        {
            Console.WriteLine("Pick a number from the the list to get that move.");
            Console.WriteLine("If you choose the number 0 you can make a new move.");
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
            var moveName = AskForString("name");
            Console.WriteLine($"Type the description of the move: {moveName}.");
            Console.WriteLine();
            var moveDescription = AskForString("description");
            Console.WriteLine("Type the sweatRate. It has to be a number ranging from 1 to 5.");
            Console.WriteLine();
            int sweatRateNumber = AskForNumber(1, 5);

            //bool succes = buMove.CreateNewMove(moveName, moveDescription, sweatRateNumber);
            bool succes = buMove.CheckNewMoveName(moveName);

            while (succes == false)
            {
                Console.WriteLine($"The name: {moveName} is already taken. Choose a new name.");
                Console.WriteLine();
                moveName = AskForString("name");
                //succes = buMove.CreateNewMove(moveName, moveDescription, sweatRateNumber);
                succes = buMove.CheckNewMoveName(moveName);
            }
            buMove.CreateNewMove(moveName, moveDescription, sweatRateNumber);
        }

        public void UpdateMove(Move move)
        {
            int number = -1;
            string moveName = null;
            string moveDescription = null;
            int moveSweatRate = -1;
            while (number != 0) 
            {
                Console.WriteLine("To update the name type: 1, the description type: 2, the sweatRate type: 3.");
                Console.WriteLine("To start the update type: 0");
                Console.WriteLine();
                number = AskForNumber(0, 3);
            
                if (number == 1)
                {
                    Console.WriteLine("You have chosen to update the name. Type the new name.");
                    Console.WriteLine();
                    moveName = AskForString("name");
                    var succes = buMove.CheckNewMoveName(moveName);
                    while (succes == false)
                    {
                        Console.WriteLine($"The name: {moveName} is already taken. Choose a new name.");
                        Console.WriteLine();
                        moveName = AskForString("name");
                        succes = buMove.CheckNewMoveName(moveName);
                    }
                    move.Name = moveName;
                }

                if (number == 2)
                {
                    Console.WriteLine("You have chosen to update the description. Type the new description.");
                    Console.WriteLine();
                    moveDescription = AskForString("description");
                    move.Description = moveDescription;
                }

                if (number == 3)
                {
                    Console.WriteLine("You have chosen to update the sweatRate. Type the new sweatRate.");
                    Console.WriteLine();
                    moveSweatRate = AskForNumber(1,5);
                    move.SweatRate = moveSweatRate;
                }
            }
            if (moveName == null && moveDescription == null && moveSweatRate == -1)
            {
                Console.WriteLine("You have given no values to update.");
                Console.WriteLine("Ending program...");
            } else
            {
                Console.WriteLine("Starting update...");
                buMove.UpdateMove(move);
            }
        }
    }
}