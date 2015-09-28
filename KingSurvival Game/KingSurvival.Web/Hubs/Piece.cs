using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingSurvival.Web.Hubs
{
    using KingSurvival.Models;
    using KingSurvival.Web.Helpers;

    public abstract class Piece
    {
        protected Player player;

        protected PieceType pieceType;

        protected Piece(Player player)
        {
            this.player = player;
        }

        public abstract bool IsValidMode(ChessBoad chessBoad, Position fromPosition, Position toPosition);
    }
}