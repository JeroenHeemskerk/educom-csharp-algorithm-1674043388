using BornToMove.DAL;

namespace BornToMove.Business
{
    public class BuMove
    {
        private MoveCrud moveCrud;

        public BuMove() 
        {
            this.moveCrud = new MoveCrud();
        }

        public MoveWithRating GetRandomMove() 
        {
            var moves = moveCrud.GetAllMoves();
            var countMoves = moves.Count;
            var rand = new Random();
            var move = moves[rand.Next(0, countMoves)];
            return move;
        }

        public List<MoveWithRating> GetListAllMoves()
        {
            var moves = moveCrud.GetAllMoves();
            return moves;
        }

        public void CreateNewMove(string moveName, string moveDescription, int sweatRateNumber)
        {
            //var moves = moveCrud.GetMoveByName(moveName);
            //if (moves != null) { return false; }

            moveCrud.Create(moveName, moveDescription, sweatRateNumber);
            //return true;
        }

        public void UpdateMove (Move move)
        {
            moveCrud.Update(move);
        }

        public MoveWithRating GetMoveById(int id)
        {
            var move = moveCrud.GetMoveById(id);
            return move;
        }

        public bool CheckNewMoveName (string moveName)
        {
            var moves = moveCrud.GetMoveByName(moveName);
            if (moves != null) { return false; }
            return true;
        }

        public void AddRatingAndVote(MoveWithRating move, double rating, double vote)
        {
            var moveRating = new MoveRating { Move = move.Move, Rating = rating, Vote = vote };
            //move.Ratings.Add(moveRating);
            moveCrud.CreateMoveRating(moveRating);
        }

        public double GetAverageRatingByMoveId(int moveId)
        {
            var averageRating = moveCrud.GetAverageRatingByMoveId(moveId);
            return averageRating;
        }
    }
}