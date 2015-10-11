namespace KingSurvival.Web.Hubs.Engine.Initializations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Figures;
    using KingSurvival.Chess.Players.Contracts;
    using System.Text;

    public class KingSurvivalGameWebInitializationStrategy : IGameInitializationStrategy
    {
        private const char Pown = 'p';
        private const char King = 'K';
        private const char EmptySpace = '-';

        private readonly string fen;
        private readonly IPlayer firstPlayer;
        private readonly IPlayer secondPlayer;

        public KingSurvivalGameWebInitializationStrategy(string fen, IPlayer firstPlayer, IPlayer secondPlayer)
        {
            this.fen = fen;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
        }

        public void Initialize(IList<IPlayer> players, IBoard board)
        {
            this.ValidateStrategy(players, board);

            //TODO: Change players turn
            players[0] = this.firstPlayer;
            players[1] = this.secondPlayer;

            this.AddFigureToBoard(this.firstPlayer, this.secondPlayer, board, this.fen);
        }

        public void AddFigureToBoard(IPlayer firstPlayser, IPlayer secondPlayer, IBoard board, string fen)
        {
            var splitedFen = fen.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var index = 0;

            for (int row = splitedFen.Length - 1; row >= 0; row--)
            {
                var currentRow = MakeRow(splitedFen[row]);

                for (int col = 0; col < currentRow.Length; col++)
                {
                    if (currentRow[col] == Pown)
                    {
                        var pawn = new Pawn(secondPlayer.Color);
                        secondPlayer.AddFigure(pawn);
                        var position = new Position(index + 1, (char)(col + 'a'));
                        board.AddFigure(pawn, position);
                    }
                    else if (currentRow[col] == King)
                    {
                        var figureInstance = new King(firstPlayser.Color);
                        firstPlayser.AddFigure(figureInstance);
                        var position = new Position(index + 1, (char)(col + 'a'));
                        board.AddFigure(figureInstance, position);
                    }
                }

                index++;
            }

        }

        private string MakeRow(string fenRow)
        {
            StringBuilder row = new StringBuilder();

            for (int index = 0; index < fenRow.Length; index++)
            {
                var currentSymbol = fenRow[index];
                if (Char.IsDigit(currentSymbol))
                {
                    var number = int.Parse(currentSymbol.ToString());
                    row.Append(new String(EmptySpace, number));
                }
                else
                {
                    row.Append(currentSymbol);
                }
            }

            return row.ToString();
        }

        private void ValidateStrategy(ICollection<IPlayer> players, IBoard board)
        {
            if (players.Count() != GlobalConstants.StandartGameNumberOfPlayers)
            {
                throw new InvalidOperationException("King Survival Start Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != GlobalConstants.StandartGameTotalBoardRows ||
                board.TotalCols != GlobalConstants.StandartGameTotalBoardCols)
            {
                throw new InvalidOperationException("King Survival Start Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
