namespace KingSurvival.Chess.Movements
{
    using System;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Common;

    public class NormalKingMovement : IMovement
    {
        public void ValidateMove(IFigure figure, IBoard board, Move move)
        {
            var color = figure.Color;
            var other = figure.Color == ChessColor.White ? ChessColor.White : ChessColor.Black;
            var from = move.From;
            var to = move.To;

            throw new NotImplementedException();
        }
    }
}
