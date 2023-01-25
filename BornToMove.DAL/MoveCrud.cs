using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class MoveCrud
    {
        private MoveContext moveContext;
        public MoveCrud() 
        {
            this.moveContext = new MoveContext();

        }

        public void Create(string moveName, string moveDescription, int sweatRateNumber) 
        {
            var move = new Move { Name = moveName, Description = moveDescription, SweatRate = sweatRateNumber };
            moveContext.Add(move);
            moveContext.SaveChanges();
        }

        public void CreateMoveRating(MoveRating moveRating)
        {
            moveContext.Add(moveRating);
            moveContext.SaveChanges();
        }

        public void Update(Move move) 
        {
            moveContext.Move.Update(move);
            moveContext.SaveChanges();
        }

        public void Delete(int id) 
        {
            var move = new Move { Id = id };
            moveContext.Remove(move);
            moveContext.SaveChanges();
        }

        public Move GetMoveById(int id) 
        {
            var move = moveContext.Move.Single(a =>a.Id == id);
            return move;
        }

        public Move? GetMoveByName(string name)
        {
            var move = moveContext.Move.FirstOrDefault(a => a.Name == name);
            return move;
        }

        public List<Move> GetAllMoves() 
        {
            List<Move> moves = new List<Move>(); 
            moves = moveContext.Move.ToList();
            return moves;
        }

        public double GetAverageRatingByMoveId(int moveId)
        {
            var ratings = moveContext.MoveRating.ToList()
                //.Where(rating => rating.Move.Id == moveId)
                //.Select(rating => rating.Rating)
                //.Average()
                ;

            var averageRating = (from rating in ratings // moveContext.MoveRating.ToList()
                                 where (rating.Move.Id == moveId)
                                select rating.Rating).DefaultIfEmpty().Average();

            return averageRating;
        }
    }
}
