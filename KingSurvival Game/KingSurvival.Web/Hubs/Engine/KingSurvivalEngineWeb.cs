﻿namespace KingSurvival.Web.Hubs.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    public class KingSurvivalEngineWeb : KingSurvivalEngine
    {
        public KingSurvivalEngineWeb(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
            : base(renderer, inputProvider, movementStrategy)
        {
        }

        public override void Initialize(IGameInitializationStrategy gameInitializationStrategy)
        {
            this.players = new List<IPlayer>
                               {
                                   new Player("[Black]Gosho", ChessColor.Black),
                                   new Player("[White]Pesho", ChessColor.White),
                               };

            this.SetFirstPlayerIndex();

            gameInitializationStrategy.Initialize(this.players, this.board);
        }

        public override void Play()
        {
            try
            {
                var player = this.GetNextPlayer();
                var move = this.Input.GetNextPlayerMove(player);
                var from = move.From;
                var to = move.To;
                var figure = this.board.GetFigureAtPosition(from);
                this.CheckIfPlayerOwnsFigure(player, figure, from);
                this.CheckIfToPositionIsEmpty(figure, to);

                var availableMovements = figure.Move(this.MovementStrategy);

                foreach (var movement in availableMovements)
                {
                    movement.ValidateMove(figure, this.board, move);
                }

                this.board.MoveFigureAtPosition(figure, from, to);

                this.Renderer.RenderBoard(this.board);

                this.WinnginConditions();

                if (this.GameState != GameState.Playing)
                {
                    this.Renderer.RenderWinningScreen(this.GameState.ToString());
                }
            }
            catch (Exception exception)
            {
                this.currentPlayerIndex--;
                this.Renderer.PrintErrorMessage(exception.Message);
            }
        }
    }
}
