namespace KingSurvival.Web.Hubs.Renderer
{
    using System;
    using System.Linq;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;
    using KingSurvival.Data;
    using KingSurvival.Models;
    using KingSurvival.Web.Helpers;
    using Microsoft.AspNet.SignalR.Hubs;

    public class WebRenderer : IRenderer
    {
        private Game game;
        private IHubCallerConnectionContext<dynamic> hubCallerConnectionContext;
        private IKingSurvivalData data;

              public WebRenderer(IHubCallerConnectionContext<dynamic> hubCallerConnectionContext, Game game, IKingSurvivalData kingSurvivalData)
        {
            // TODO: Complete member initialization
            this.hubCallerConnectionContext = hubCallerConnectionContext;
            this.game = game;
            this.data = kingSurvivalData;
        }
        public void RenderBoard(IBoard board)
        {
            var gameId = this.game.Id.ToString();
            var oldBord = this.game.Board;
            var newBoard = BoardHelper.BoardToFen(board);

            this.game.Board = newBoard;

            this.game.State = this.game.State == KingSurvivalGameState.TurnKing ? KingSurvivalGameState.TurnPown : KingSurvivalGameState.TurnKing;

            this.data.SaveChanges();

            // move 
            this.hubCallerConnectionContext.Group(gameId).move(this.game.Board);

            // update state 
            this.UpdateState(gameId);
        }

        public void PrintErrorMessage(string errorMessage)
        {
            var gameId = this.game.Id.ToString();
            var oldFen = this.game.Board;
            this.hubCallerConnectionContext.Group(gameId).move(oldFen);
        }

        public void RenderWinningScreen(string message)
        {
            //Win WIn Win
            throw new NotImplementedException();
        }

        public void RenderMainMenu()
        {
            throw new NotImplementedException();
        }

        private void UpdateState(string gameId)
        {
            var gameState =
                this.data.Game.All()
                    .Where(x => x.Id.ToString() == gameId)
                    .Select(x => new { x.Id, gameState = x.State })
                    .FirstOrDefault();

            this.hubCallerConnectionContext.Group(gameId).updateGameState(gameState);
        }
    }
}
