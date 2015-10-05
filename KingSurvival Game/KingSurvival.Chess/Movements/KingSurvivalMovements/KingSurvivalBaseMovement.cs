namespace KingSurvival.Chess.Movements.KingSurvivalMovements
{
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public abstract class KingSurvivalBaseMovement : IMovement
    {
        protected const string InvalidMove = "Invalid move!";

        public abstract void ValidateMove(IFigure figure, IBoard board, Move move);

        protected bool CheckOtherFigureIfValid(IBoard board, Position to, ChessColor color)
        {
            var otherFigure = board.GetFigureAtPosition(to);
            if (otherFigure != null && otherFigure.Color == color)
            {
                return false;
            }

            return true;
        }
    }
}
