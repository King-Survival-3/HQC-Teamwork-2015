namespace KingSurvival.Chess.Players.Contracts
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public interface IPlayer
    {
        string Name { get; }

        ChessColor Color { get; }

        void AddFigure(IFigure figure);

        void RemoveFigure(IFigure figure);
    }
}
