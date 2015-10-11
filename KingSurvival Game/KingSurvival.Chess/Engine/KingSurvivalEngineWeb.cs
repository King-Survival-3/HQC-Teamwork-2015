namespace KingSurvival.Chess.Engine.Initializations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
   
    using KingSurvival.Chess.Common;
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
            // TODO: Remove
            // using Chess.Players;
            // Added for development purposes only 

            // TODO: If players are changed - board is reversed
            this.players = new List<IPlayer>
                               {
                                   new Player("[Black]Gosho", ChessColor.Black),
                                   new Player("[White]Pesho", ChessColor.White),
                               };

            this.SetFirstPlayerIndex();

            // Use this
            //var players = this.input.GetPlayers(GlobalConstants.StandartGameNumberOfPlayers);
            gameInitializationStrategy.Initialize(players, this.board);
        }

        public override void Play()
        {
            try
            {
                var player = this.GetNextPlayer();
                var move = this.input.GetNextPlayerMove(player);
                var from = move.From;
                var to = move.To;
                var figure = this.board.GetFigureAtPosition(from);
                this.CheckIfPlayerOwnsFigure(player, figure, from);
                this.CheckIfToPositionIsEmpty(figure, to);

                var availableMovements = figure.Move(this.movementStrategy);

                foreach (var movement in availableMovements)
                {
                    movement.ValidateMove(figure, this.board, move);
                }

                this.board.MoveFigureAtPosition(figure, from, to);

                this.renderer.RenderBoard(this.board);

                // TODO: On every move check if we are in check.
                // TODO: Check pawn on last row
                // TODO: Check is when 
                // ПАТ -> when the last three moves are the same 
                //   Memento?
                // TODO: if not castle - move figure (Check castel (rockade) - check if castle is valid, Check pawn for An-Pasan)
                // TODO: Move figure
                // TODO: Chech check (chess)
                // TODO: If in check - check checkmate
                // TODO: if not in check - check draw
                // TODO: Continue
                this.WinnginConditions();

                if (this.gameState != GameState.Playing)
                {
                    this.renderer.RenderWinningScreen(this.gameState.ToString());
                }
            }
            catch (Exception exception)
            {
                this.currentPlayerIndex--;
                this.renderer.PrintErrorMessage(exception.Message);
            }

        }
             
    }
}
