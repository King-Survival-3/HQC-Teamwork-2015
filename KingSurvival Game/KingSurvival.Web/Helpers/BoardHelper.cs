namespace KingSurvival.Web.Helpers
{
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using System;
    using System.Text;
    using KingSurvival.Chess.Figures;

    public static class BoardHelper
    {
        public static string BoardToFen(IBoard board)
        {
            var fen = new StringBuilder();
            var reversedRow = board.TotalRows ;
            for (int row = 1; row <= board.TotalRows; row++)
            {
                var empySpace = 0;
                for (int col = 0; col < board.TotalCols; col++)
                {
                    var figure = board.GetFigureAtPosition(new Position(reversedRow, (char)('a' + col)));

                    if (figure == null)
                    {
                        empySpace++;
                    }
                    else
                    {
                        if (empySpace != 0)
                        {
                            fen.Append(empySpace);
                            empySpace = 0;
                        }

                        //fen.Append(board[reversedRow, col]);
                        if (figure is King)
                        {
                            fen.Append('K');
                        }
                        else
                        {
                            fen.Append('p');
                        }
                    }

                    if (col == (board.TotalCols - 1) && empySpace != 0)
                    {
                        fen.Append(empySpace);
                    }
                }

                if (row != board.TotalRows)
                {
                    fen.Append('/');
                }

                reversedRow--;
            }

            return fen.ToString();
        }

        private static string MakeRow(string fenRow)
        {
            StringBuilder row = new StringBuilder();

            for (int index = 0; index < fenRow.Length; index++)
            {
                var currentSymbol = fenRow[index];
                if (Char.IsDigit(currentSymbol))
                {
                    var number = int.Parse(currentSymbol.ToString());
                    row.Append(new String('-', number));
                }
                else
                {
                    row.Append(currentSymbol);
                }
            }

            return row.ToString();
        }
    }
}