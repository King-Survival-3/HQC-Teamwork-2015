namespace KingSurvival.Chess.Figures
{
    using System.Collections.Generic;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class Rook : BaseFigure, IFigure
    {
        public Rook(ChessColor color)
            : base(color)
        {
        }

        public override ICollection<IMovement> Move(IMovementStrategy strategy)
        {
            throw new System.NotImplementedException();
        }
    }
}
