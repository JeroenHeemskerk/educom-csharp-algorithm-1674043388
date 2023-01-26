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

        public ICollection<MoveRating> Ratings { get; set; }// = new List<MoveRating>();

        [NotMapped]
        public double AverageRating { get; set; }


        public void ShowAll()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Description: " + Description);
            Console.WriteLine("SweatRate: " + SweatRate);
            Console.WriteLine("Average rating: " + AverageRating);
            Console.WriteLine();
        }

        public void ShowMoveId() { Console.WriteLine("Id: " + Id); }
        public void ShowMoveName() { Console.WriteLine("Name: " + Name); }
        public void ShowMoveDescription() { Console.WriteLine("Description: "+Description); }
        public void ShowMoveSweatRate() { Console.WriteLine("SweatRate: "+SweatRate); }
        public void ShowMoveAverageRating() { Console.WriteLine("Average rating: " + AverageRating); }

    }
}
