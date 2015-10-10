namespace KingSurvival.Models
{
    using System;

    public class Position
    {
        private const int MaxNumberOfRow = 7;
        private const int MaxNumberOfCol = 7;

        private int row;
        private int col;

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                if (value > MaxNumberOfRow)
                {
                    throw new ArgumentException("Row cannot be bigger than " + MaxNumberOfRow);
                }

                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }

            set
            {
                if (value > MaxNumberOfCol)
                {
                    throw new ArgumentException("Col cannot be bigger than " + MaxNumberOfCol);
                }

                this.col = value;
            }
        }
    }
}
