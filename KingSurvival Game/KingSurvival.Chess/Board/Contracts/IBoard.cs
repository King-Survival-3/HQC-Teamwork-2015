namespace KingSurvival.Chess.Board.Contracts
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public interface IBoard
    {
        int TotalRows { get; }

        int TotalCols { get; }

        void AddFigure(IFigure figure, Position position);

        void RemoveFigure(Position position);

        void MoveFigureAtPosition(IFigure figure, Position from, Position to);

        void CheckIfSquareIsFree(IFigure figure, Position position);

        Position GetKingPosition(ChessColor color);

        IFigure GetFigureAtPosition(Position position);
    }
}