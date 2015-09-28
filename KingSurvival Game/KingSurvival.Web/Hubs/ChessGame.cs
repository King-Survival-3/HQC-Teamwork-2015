using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingSurvival.Web.Hubs
{
    // http://amitcodes.com/2014/02/04/object-oriented-design-for-chess-game/
    public class ChessGame
    {
        public ChessGame(ChessBoad chessBoad, Player blackPlayer, Player whitePlayer)
        {
            this.ChessBoard = chessBoad;
            this.BlackPlayer = blackPlayer;
            this.WhitePlayer = whitePlayer;
        }

        public ChessBoad ChessBoard { get; set; }

        public Player BlackPlayer { get; set; }

        public Player WhitePlayer { get; set; }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public bool ValidateMove()
        {
            throw new NotImplementedException();
        }

        public bool ValidateBoard()
        {
            throw new NotImplementedException();
        }

        public bool IsGameOver()
        {
            throw new NotImplementedException();
        }
    }
}