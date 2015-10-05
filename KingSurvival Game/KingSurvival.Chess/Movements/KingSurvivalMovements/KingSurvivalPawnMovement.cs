namespace KingSurvival.Chess.Movements.KingSurvivalMovements
{
    using System;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class KingSurvivalPawnMovement : KingSurvivalBaseMovement, IMovement
    {
        public override void ValidateMove(IFigure figure, IBoard board, Move move)
        {
            var from = move.From;
            var to = move.To;

            if ((from.Row - 1 == to.Row && from.Col + 1 == to.Col) || // bottom right) 
                (from.Row - 1 == to.Row && from.Col - 1 == to.Col)) // bottom left;
            {
                var otherFigure = board.GetFigureAtPosition(to);

                if (otherFigure != null)
                {
                    throw new InvalidOperationException(InvalidMove);
                }

                return;
            }

            throw new InvalidOperationException(InvalidMove);
        }
    }
}