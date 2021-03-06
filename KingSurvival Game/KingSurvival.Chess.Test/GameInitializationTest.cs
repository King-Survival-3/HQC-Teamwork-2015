﻿namespace KingSurvival.Chess.Test
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Initializations;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameInitializationTest
    {
        [TestMethod]
        public void GameInitializationCheckPlayerCorrectName()
        {
            var playerList = new List<IPlayer> { new Player("[Black]Gosho", ChessColor.Black), new Player("[White]Pesho", ChessColor.White) };
            var board = new Board.Board(GlobalConstants.StandartGameTotalBoardRows, GlobalConstants.StandartGameTotalBoardCols);

            var strategy = new KingSurvivalGameInitializationStrategy();
            strategy.Initialize(playerList, board);

            Assert.AreEqual("[Black]Gosho", playerList[0].Name);
        }

        [TestMethod]
        public void GameInitializationCheckPlayerCorrectColor()
        {
            var playerList = new List<IPlayer> { new Player("[Black]Gosho", ChessColor.Black), new Player("[White]Pesho", ChessColor.White) };
            var board = new Board.Board(GlobalConstants.StandartGameTotalBoardRows, GlobalConstants.StandartGameTotalBoardCols);

            var strategy = new KingSurvivalGameInitializationStrategy();
            strategy.Initialize(playerList, board);

            Assert.AreEqual(ChessColor.Black, playerList[0].Color);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GameInitializationOnlyOnePlayer()
        {
            var playerList = new List<IPlayer> { new Player("[Black]Gosho", ChessColor.Black) };
            var board = new Board.Board(GlobalConstants.StandartGameTotalBoardRows, GlobalConstants.StandartGameTotalBoardCols);

            var strategy = new KingSurvivalGameInitializationStrategy();
            strategy.Initialize(playerList, board);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GameInitializationInvalidBoard()
        {
            var playerList = new List<IPlayer> { new Player("[Black]Gosho", ChessColor.Black), new Player("[White]Pesho", ChessColor.White) };
            var board = new Board.Board(5, GlobalConstants.StandartGameTotalBoardCols);

            var strategy = new KingSurvivalGameInitializationStrategy();
            strategy.Initialize(playerList, board);
        }

        [TestMethod]
        public void CheckIfPlayersAreNotEqual()
        {
            var playerList = new List<IPlayer> { new Player("[Black]Gosho", ChessColor.Black), new Player("[White]Pesho", ChessColor.White) };
            var board = new Board.Board(GlobalConstants.StandartGameTotalBoardRows, GlobalConstants.StandartGameTotalBoardCols);

            var strategy = new KingSurvivalGameInitializationStrategy();
            strategy.Initialize(playerList, board);

            Assert.AreNotEqual(playerList[0], playerList[1]);
        }
    }
}
