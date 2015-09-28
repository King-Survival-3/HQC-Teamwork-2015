using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingSurvival.Web.Hubs
{
    public class ChessBoad
    {
        public ChessBoad()
        {
            this.Blocks = new List<Block>();
        }

        public IEnumerable<Block> Blocks { get; set; } 
    }
}
