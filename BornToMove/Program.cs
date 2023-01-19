using System;
using System.Collections;
using System.Collections.Generic;

namespace BornToMove
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("The user needs to move.");
            Console.WriteLine("Type 1 to get a suggestion or type 2 to choose an option of the list.");
            String choice = Console.ReadLine();
            if (choice == "1" ) 
            {
                Console.WriteLine("You have chosen to get a suggestion.");
            }
            else {
                Console.WriteLine("You have chosen to choose an option of the list.");
            }

            var ConnectToMySql = new ConnectToMySql();
            ConnectToMySql.ReadAllData();
            
        }
    }
}