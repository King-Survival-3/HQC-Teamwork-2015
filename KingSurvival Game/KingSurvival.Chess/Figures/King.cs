namespace KingSurvival.Chess.Figures
{
    using System.Collections.Generic;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class King : BaseFigure, IFigure
    {
        public King(ChessColor color)
            : base(color)
        {
        }

        public override ICollection<IMovement> Move(IMovementStrategy strategy)
        {
            var movememts = strategy.GetMovements(this.GetType().Name);

            return movememts;
        }
    }
}
