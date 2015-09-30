namespace KingSurvival.Chess.Figures
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public class King : BaseFigure, IFigure
    {
        public King(ChessColor color)
            : base(color)
        {
        }
    }
}
