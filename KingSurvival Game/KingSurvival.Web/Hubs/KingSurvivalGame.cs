namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Movements.Strategies;
    using KingSurvival.Chess.Players;
    using KingSurvival.Data;
    using KingSurvival.Models;
    using KingSurvival.Web.Hubs.Engine;
    using KingSurvival.Web.Hubs.Engine.Initializations;
    using KingSurvival.Web.Hubs.InputProvider;
    using KingSurvival.Web.Hubs.Renderer;

    using Microsoft.AspNet.SignalR;

    public class KingSurvivalGame : Hub
    {
        private readonly IKingSurvivalData data;

        public KingSurvivalGame()
            : this(new KingSurvivalData(new KingSurvivalDbContext()))
        {
        }

        public KingSurvivalGame(IKingSurvivalData data)
        {
            this.data = data;
        }

        // Name of the room is same like the game Id
        public void JoinRoom(string room)
        {
            if (!string.IsNullOrEmpty(room))
            {
                this.Groups.Add(this.Context.ConnectionId, room);
                this.Clients.Caller.joinRoom(room);
                this.UpdateState(room);
            }
        }

        public void GameEngine(string gameId, string userId, string moveFrom, string moveTo)
        {
            var game = this.GetGame(gameId);
            var playerID = userId;
            var players = this.GetPlayers();
            var firstPlayer = players
                .FirstOrDefault(x => x.Id == game.FirstPlayerId);
            var secondPlayer = players
                .FirstOrDefault(x => x.Id == game.SecondPlayerId);
            var oldFen = game.Board;

            // if game doesn't exist
            if (game == null)
            {
                return;
            }

            if (firstPlayer == null || secondPlayer == null)
            {
                return;
            }

            // nothing to move
            if (moveFrom == moveTo)
            {
                return;
            }

            // check If player is 1 or 2 
            if (playerID != game.FirstPlayerId && playerID != game.SecondPlayerId)
            {
                // return you are not part of this game
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.FirstPlayerId && game.State != KingSurvivalGameState.TurnKing)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.SecondPlayerId && game.State != KingSurvivalGameState.TurnPown)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            var renderer = new WebRenderer(this.Clients, game, this.data);
            var furstPlayer = new Player(firstPlayer.UserName, ChessColor.White);
            var scondPlayer = new Player(secondPlayer.UserName, Chess.Common.ChessColor.White);
            var kingSurvivalInitilizeStrategy = new KingSurvivalGameWebInitializationStrategy(oldFen, furstPlayer, scondPlayer);
            var from = new Position(moveFrom[1] - '0', moveFrom[0]);
            var to = new Position(moveTo[1] - '0', moveTo[0]);
            var move = new Move(from, to);
            var inputProvider = new WebInputProvider(move);
            var movementStrategy = new KingSurvivalMovementStrategy();

            var gameEngine = new KingSurvivalEngineWeb(renderer, inputProvider, movementStrategy);

            gameEngine.Initialize(kingSurvivalInitilizeStrategy);
            gameEngine.Play();
        }

        private void UpdateState(string gameId)
        {
            var gameState =
                this.data.Game.All()
                    .Where(x => x.Id.ToString() == gameId)
                    .Select(x => new { x.Id, gameState = x.State })
                    .FirstOrDefault();

            this.Clients.Group(gameId).updateGameState(gameState);
        }

        private Game GetGame(string gameId)
        {
            var game = this.data.Game.All().FirstOrDefault(x => x.Id.ToString() == gameId);

            return game;
        }

        private IEnumerable<KingSurvivalUser> GetPlayers()
        {
            var players = this.data.Users
                .All()
                .ToList();

            return players;
        }
    }
}