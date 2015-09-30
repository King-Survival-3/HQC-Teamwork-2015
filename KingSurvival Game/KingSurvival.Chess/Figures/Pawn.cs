﻿namespace KingSurvival.Chess.Figures
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public class Pawn : BaseFigure, IFigure
    {
        public Pawn(ChessColor color)
            :base(color)
        {
        }
    }
}
