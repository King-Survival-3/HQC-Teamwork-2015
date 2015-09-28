using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvival.Web.Hubs
{
    public class Player
    {
        public Player()
        {
            this.Pieces = new List<Piece>();
        }

        public void PlayTurn()
        {
            throw new NotImplementedException();
        }

        public List<Piece> Pieces { get; set; }
    }
}
