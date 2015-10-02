﻿namespace KingSurvival.Chess.Figures
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public class Knight : BaseFigure, IFigure
    {
        public Knight(ChessColor color)
            : base(color)
        {
        }

        public override ICollection<IMovement> Move(IMovementStrategy strategy)
        {
            throw new System.NotImplementedException();
        }
    }
}
