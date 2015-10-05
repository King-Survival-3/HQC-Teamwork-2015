namespace KingSurvival.Chess.Figures
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Movements.Contracts;

    public class Bishop : BaseFigure, IFigure
    {
        public Bishop(ChessColor color)
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
