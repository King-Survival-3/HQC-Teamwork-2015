using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingSurvival.Web.Hubs
{
    using KingSurvival.Models;

    public class Pawn : Piece
    {
        public Pawn(Player player)
            : base(player)
        {
            this.pieceType = PieceType.Pawn;
        }

        public override bool IsValidMode(ChessBoad chessBoad, Position fromPosition, Position toPosition)
        {
            throw new NotImplementedException();
        }
    }
}