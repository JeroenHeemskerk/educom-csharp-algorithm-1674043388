﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Organizer;

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
            //moveContext.ChangeTracker.Clear();
            //moveContext.Move.Update(move);
            //moveContext.Entry<Move>(move).State = EntityState.Detached;
            //moveContext.SaveChanges();
            moveRating.Move=moveContext.Move.Single(m => m.Id ==moveRating.Move.Id);
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

        public MoveWithRating GetMoveById(int id)
        {
            var move = moveContext.Move.Select(move => new MoveWithRating()
            {
                Move = move,
                AverageRating = move.Ratings.Select(r => r.Rating).DefaultIfEmpty().Average()
            }).Where(move => move.Move.Id == id).First();
            //var move = moveContext.Move.Find(id);
            return move;
        }

        public Move? GetMoveByName(string name)
        {
            var move = moveContext.Move.FirstOrDefault(a => a.Name == name);
            return move;
        }

        public List<MoveWithRating> GetAllMoves()
        {
            var test = moveContext.Move.ToList();
            var moves = moveContext.Move.Select(move => new MoveWithRating()
            {
                Move = move,
                AverageRating =move.Ratings.Select(r => r.Rating).DefaultIfEmpty().Average()
            }).ToList();
            var SHS = new ShiftHighestSort<MoveWithRating>();
            var sortedMoves = SHS.Sort(moves , new RatingsConverter());

            return sortedMoves;
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
