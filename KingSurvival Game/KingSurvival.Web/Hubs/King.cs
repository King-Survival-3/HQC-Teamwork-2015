using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingSurvival.Web.Hubs
{
    using KingSurvival.Models;

    public class King : Piece
    {
        public King(Player player)
            : base(player)
        {
            this.pieceType = PieceType.King;
        }

        public override bool IsValidMode(ChessBoad chessBoad, Position fromPosition, Position toPosition)
        {
            throw new NotImplementedException();
        }
    }
}