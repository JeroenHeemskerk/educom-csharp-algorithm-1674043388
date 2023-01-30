using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class RatingsConverter : IComparer<MoveWithRating>
    {
        public int Compare(MoveWithRating? x, MoveWithRating? y)
        {
            return y.AverageRating.CompareTo(x.AverageRating) ;
        }
    }
}
