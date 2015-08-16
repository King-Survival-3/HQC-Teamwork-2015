namespace KingSurvival.Web.Helpers
{
    using System;
    using System.Text;

    public static class BoardHelper
    {
        public static char[,] FenToBoard(string fen)
        {
            var splitedFen = fen.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var board = new char[8, 8];
            var index = 0;

            for (int row = splitedFen.Length - 1; row >= 0; row--)
            {
                var currentRow = MakeRow(splitedFen[row]);

                for (int col = 0; col < currentRow.Length; col++)
                {
                    board[index, col] = currentRow[col];
                }

                index++;
            }

            return board;
        }

        public static string BoardToFen(char[,] board)
        {
            var fen = new StringBuilder();
            var reversedRow = board.GetLength(0) - 1;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                var empySpace = 0;
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[reversedRow, col] == '-')
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

                        fen.Append(board[reversedRow, col]);
                    }

                    if (col == (board.GetLength(1) - 1) && empySpace != 0)
                    {
                        fen.Append(empySpace);
                    }
                }

                if (row != board.GetLength(0) - 1)
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