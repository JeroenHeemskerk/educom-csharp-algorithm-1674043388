using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    [Table(name:"move")]
    public class Move
    {
        public int Id { get; set; }
        public  string Name { get; set; } = "";
        public  string Description { get; set; } = "";
        public  int SweatRate { get; set; }

        public virtual ICollection<MoveRating> Ratings { get; set; } = new List<MoveRating>();

        //[NotMapped]
        //public double AverageRating { get; set; }




    }
}
