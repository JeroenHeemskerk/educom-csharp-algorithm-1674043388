using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace BornToMove.DAL
{
    public class MoveRating
    {
        public int Id { get; set; }
        public virtual Move Move { get; set; }
        public double Rating { get; set; }
        public double Vote { get; set; }
    }
}
