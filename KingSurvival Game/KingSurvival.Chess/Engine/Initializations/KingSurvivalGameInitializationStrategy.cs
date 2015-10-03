using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingSurvival.Chess.Board.Contracts;
using KingSurvival.Chess.Common;
using KingSurvival.Chess.Engine.Contracts;
using KingSurvival.Chess.Figures;
using KingSurvival.Chess.Figures.Contracts;
using KingSurvival.Chess.Players.Contracts;

namespace KingSurvival.Chess.Engine.Initializations
{
    public class KingSurvivalGameInitializationStrategy : IGameInitializationStrategy
    {
        private const int StandartGameTotalRows = 8;
        private const int StandartGameTotalCols = 8;

        private readonly IList<Type> figureTypes;

        public KingSurvivalGameInitializationStrategy()
        {
          
        }

        public void Initialize(IList<IPlayer> players, IBoard board)
        {
            this.ValidateStrategy(players, board);

            var firstPlayer = players[0];
            var secondPlayer = players[1];

            this.AddPawnsToBoardRow(firstPlayer, board, 8);

            this.AddKingToBoardRow(secondPlayer, board, 1, 'e');
        }

        private void AddPawnsToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            for (int i = 0; i < StandartGameTotalCols; i+=2)
            {
                var pawn = new Pawn(player.Color);
                player.AddFigure(pawn);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(pawn, position);
            }
        }

        private void AddKingToBoardRow(IPlayer player, IBoard board, int chessRow, char chessCol)
        {
                var figureInstance = new King(player.Color);
                player.AddFigure(figureInstance);
                var position = new Position(chessRow, chessCol);
                board.AddFigure(figureInstance, position);
        }

        private void ValidateStrategy(ICollection<IPlayer> players, IBoard board)
        {
            if (players.Count() != GlobalConstants.StandartGameNumberOfPlayers)
            {
                throw new InvalidOperationException("King Survival Start Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != StandartGameTotalRows ||
                board.TotalCols != StandartGameTotalCols)
            {
                throw new InvalidOperationException("King Survival Start Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
