namespace KingSurvival.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct Coordinate
    {
        private int row;
        private int col;

        public Coordinate(int row, int col)
            : this()
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
