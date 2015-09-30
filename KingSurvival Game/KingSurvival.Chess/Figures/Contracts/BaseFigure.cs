namespace KingSurvival.Chess.Figures.Contracts
{
    using KingSurvival.Chess.Common;

    public abstract class BaseFigure
    {
        protected BaseFigure(ChessColor color)
        {
            this.Color = color;
        }

        public ChessColor Color { get; private set; }
    }
}
