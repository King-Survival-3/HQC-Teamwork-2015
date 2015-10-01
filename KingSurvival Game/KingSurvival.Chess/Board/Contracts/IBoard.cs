namespace KingSurvival.Chess.Board.Contracts
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public interface IBoard
    {
        int TotalRows { get; }

        int TotalCols { get; }

        void AddFigure(IFigure figure, Position position);

        void RemoveFigure(Position position);

        IFigure GetFigureAtPosition(Position position);
    }
}