namespace KingSurvival.Chess.Engine.Initializations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Figures;
    using KingSurvival.Chess.Players.Contracts;


    public class KingSurvivalGameInitializationStrategy : IGameInitializationStrategy
    {
        private readonly IList<Type> figureTypes;

        public void Initialize(IList<IPlayer> players, IBoard board)
        {
            this.ValidateStrategy(players, board);

            var firstPlayer = players[0];
            var secondPlayer = players[1];

            this.AddPawnsToBoardRow(firstPlayer, board, 8);

            //this.AddKingToBoardRow(secondPlayer, board, 1, 'd');
            // TODO: remove below. Added only for testing purposes
            this.AddKingToBoardRow(secondPlayer, board, 7, 'h');
        }

        private void AddPawnsToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            // TODO : remove (-2) added for testing purposes
            for (int i = 0; i < GlobalConstants.StandartGameTotalBoardCols - 2; i+=2)
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
                throw new InvalidOperationException("King Survival Play Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != GlobalConstants.StandartGameTotalBoardRows ||
                board.TotalCols != GlobalConstants.StandartGameTotalBoardCols)
            {
                throw new InvalidOperationException("King Survival Play Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
