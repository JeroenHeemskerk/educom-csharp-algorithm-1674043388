using System;
using System.Collections.Generic;

namespace Organizer
{
	public class ShiftHighestSort<T>
    {
        private List<T> array = new List<T>();
        private IComparer<T> Comparer { get; set; }
        public int count = 0;

        //public ShiftHighestSort() { }

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<T> Sort(List<T> input, IComparer<T> comperer)
        {
            if (comperer == null)
            {
                throw new ArgumentNullException(nameof(comperer));
            }
            array = new List<T>(input);
            Comparer = comperer;
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
                if (Comparer.Compare(array[i], array[i+1])> 0)
                {
                    T lowerNumber = array[i+1];
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
