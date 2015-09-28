using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvival.Web.Hubs
{
    public class Block
    {
        private Piece piece;

        public Block(Piece piece)
        {
            this.piece = piece;
        }

        public Piece getPiece()
        {
            return this.piece;
        }
        public void setPiece(Piece pieceToSet)
        {
            this.piece = pieceToSet;
        }
    }
}
