using System;
using System.Collections;
using System.Collections.Generic;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Program = new Program();
            var ShiftHighestSort = new ShiftHighestSort();

            List<int> rtnList = Program.ListRandomIntegers(10);
            List<int> sortedList = ShiftHighestSort.Sort(rtnList);
            Console.WriteLine("Hello World!");
            ShowList("Random list: ", rtnList);
            ShowList("Sorted list: ", sortedList);
            Console.WriteLine("Sorted list valid: " + Program.IsListSorted(sortedList));

            // Press <F5> to run this code, when "Hello World!" appears in a black box, remove the line below and write your code below.
            // Console.WriteLine("Hello World!");
            //ShowList("Example of ShowList", new List<int>() { -33, 3, 2, 2, 3, 34, 34, 32, 1, 3, 5, 3, -22, -99, 33, -22, 11, 3, 33, 12, -2, -21, 4, 34, 22, 15, 34, -22 });
        }

        public  List<int> ListRandomIntegers(int n)
        {
            var rand = new Random();
            var rtnList = new List<int>();

            for (int i =0; i < n; i++)
            {
                rtnList.Add(rand.Next(-99, 99));
            }

            return rtnList;
        }

        public bool IsListSorted(List<int> list)
        {
            bool sorted = true;
            
            for (int i = 1; i < list.Count - 1; i++)
            {
                if (!(list[i] >= list[i-1]))
                {
                    sorted = false; break;
                }
                
            }
            return sorted;
        }

        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 100)
            {
                count = 300; // Do not show more than 300 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
