namespace KingSurvival.Chess.Movements.KingSurvivalMovements
{
    using System;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class KingSurvivalKingMovement : KingSurvivalBaseMovement, IMovement
    {
        public override void ValidateMove(IFigure figure, IBoard board, Move move)
        {
            var other = figure.Color == ChessColor.White ? ChessColor.White : ChessColor.Black;
            var from = move.From;
            var to = move.To;

            if ((from.Row + 1 == to.Row && from.Col + 1 == to.Col) || // top right
                (from.Row + 1 == to.Row && from.Col - 1 == to.Col) || // top left
                (from.Row - 1 == to.Row && from.Col + 1 == to.Col) || // bottom right) 
                (from.Row - 1 == to.Row && from.Col - 1 == to.Col)) // bottom left
            {
                if (this.CheckOtherFigureIfValid(board, to, other))
                {
                    return;
                }
            }

            throw new InvalidOperationException(InvalidMove);
        }
    }
}
