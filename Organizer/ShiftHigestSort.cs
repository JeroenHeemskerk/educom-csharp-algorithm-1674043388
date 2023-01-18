using System;
using System.Collections.Generic;

namespace Organizer
{
	public class ShiftHighestSort
    {
        private List<int> array = new List<int>();
        public int count = 0;

        //public ShiftHighestSort() { }

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<int> Sort(List<int> input)
        {
            array = new List<int>(input);

            SortFunction(0, array.Count - 1);
            return array;
        }

        /// <summary>
        /// Sort the array from low to high
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {

            for (int i = low; i <= high-1; i++) 
            {
                if (array[i] > array[i+1])
                {
                    int lowerNumber = array[i+1];
                    array[i+1] = array[i];
                    array[i] = lowerNumber;
                }
            }
            count++;
            while (count < (array.Count - 1))
            {
                SortFunction(low, high-1);
            }
            //throw new NotImplementedException();
        }    
    }
}
