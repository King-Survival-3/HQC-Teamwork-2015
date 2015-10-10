namespace KingSurvival.Chess.Movements
{
    using System;
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class NormalKingMovement : IMovement
    {
        private const string KingInvalidMove = "King cannot move this way!";

        public void ValidateMove(IFigure figure, IBoard board, Move move)
        {
            var other = figure.Color == ChessColor.White ? ChessColor.White : ChessColor.Black;
            var from = move.From;
            var to = move.To;

            if ((from.Row == to.Row + 1 && from.Col == to.Col + 1) || // top right
                (from.Row == to.Row + 1 && from.Col == to.Col) ||
                (from.Row == to.Row + 1 && from.Col == to.Col - 1) ||
                (from.Row == to.Row && from.Col == to.Col - 1) ||
                (from.Row == to.Row && from.Col == to.Col + 1) ||
                (from.Row == to.Row - 1 && from.Col == to.Col + 1) || 
               (from.Row == to.Row - 1 && from.Col == to.Col) ||
                (from.Row == to.Row - 1 && from.Col == to.Col - 1)) 
            {
                if (this.CheckOtherFigureIfValid(board, to, other))
                {
                    return;
                }
            }

            throw new InvalidOperationException(KingInvalidMove);
        }

        private bool CheckOtherFigureIfValid(IBoard board, Position to, ChessColor color)
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
