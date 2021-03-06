﻿namespace KingSurvival.Chess.Engine.Initializations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Figures;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Players.Contracts;

    public class StandartStartGameInitializationStrategy : IGameInitializationStrategy
    {
       private readonly IList<Type> figureTypes;

        public StandartStartGameInitializationStrategy()
        {
            this.figureTypes = new List<Type>
                                   {
                                        typeof(Rook), 
                                        typeof(Knight), 
                                        typeof(Bishop), 
                                        typeof(Queen), 
                                        typeof(King), 
                                        typeof(Bishop), 
                                        typeof(Knight),
                                        typeof(Rook)
                                   };
        }

        public void Initialize(IList<IPlayer> players, IBoard board)
        {
            this.ValidateStrategy(players, board);

            var firstPlayer = players[0];
            var secondPlayer = players[1];

            this.AddArmyToBoardRow(firstPlayer, board, 8);
            this.AddPawnsToBoardRow(firstPlayer, board, 7);

            this.AddPawnsToBoardRow(secondPlayer, board, 2);
            this.AddArmyToBoardRow(secondPlayer, board, 1);
        }

        private void AddPawnsToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            for (int i = 0; i < GlobalConstants.StandartGameTotalBoardCols; i++)
            {
                var pawn = new Pawn(player.Color);
                player.AddFigure(pawn);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(pawn, position);
            }
        }

        private void AddArmyToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            for (int i = 0; i < GlobalConstants.StandartGameTotalBoardCols; i++)
            {
                var figureType = this.figureTypes[i];
                var figureInstance = (IFigure)Activator.CreateInstance(figureType, player.Color);
                player.AddFigure(figureInstance);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(figureInstance, position);
            }
        }

        private void ValidateStrategy(ICollection<IPlayer> players, IBoard board)
        {
            if (players.Count() != GlobalConstants.StandartGameNumberOfPlayers)
            {
                throw new InvalidOperationException("Standart Play Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != GlobalConstants.StandartGameTotalBoardRows ||
                board.TotalCols != GlobalConstants.StandartGameTotalBoardCols)
            {
                throw new InvalidOperationException("Standart Play Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
