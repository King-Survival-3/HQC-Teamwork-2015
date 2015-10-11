namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.SignalR;

    using KingSurvival.Data;
    using KingSurvival.Models;
    using KingSurvival.Web.Helpers;
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;
    using KingSurvival.Chess.Engine.Initializations;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.InputProvider;
    using KingSurvival.Chess.Movements.Strategies;
    using KingSurvival.Chess.Engine;
    using KingSurvival.Chess.Engine.Contracts;
    using System.Collections.Generic;

    public class KingSurvivalGame : Hub, IRenderer
    {
        private readonly IKingSurvivalData data;

        private IChessEngine gameEngine;

        public KingSurvivalGame()
            : this(new KingSurvivalData(new KingSurvivalDbContext()))
        {

        }

        private Game Game { get; set; }

        public KingSurvivalGame(IKingSurvivalData data)
        {
            this.data = data;
        }

        public void RenderBoard(IBoard board)
        {
            //TODO: board to fen
            var gameId = Game.Id.ToString();
            var oldBord = Game.Board;
            var newBoard = BoardHelper.BoardToFen(board);

            Game.Board = newBoard;

            Game.State = Game.State == KingSurvivalGameState.TurnKing ? KingSurvivalGameState.TurnPown : KingSurvivalGameState.TurnKing;

            this.data.SaveChanges();

            // move 
            this.Clients.Group(gameId).move(Game.Board);

            // update state 
            this.UpdateState(gameId);
        }

        public void PrintErrorMessage(string errorMessage)
        {
            var gameId = Game.Id.ToString();
            var oldFen = Game.Board;
            this.Clients.Group(gameId).move(oldFen);
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
            this.Game = this.GetGame(gameId);
            var playerID = userId;
            var players = this.GetPlayers();
            var firstPlayer = players
                .FirstOrDefault(x => x.Id == this.Game.FirstPlayerId);
            var secondPlayer = players
                .FirstOrDefault(x => x.Id == this.Game.SecondPlayerId);
            var oldFen = Game.Board;

            // if game doesn't exist
            if (Game == null)
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
            if (playerID != Game.FirstPlayerId && playerID != Game.SecondPlayerId)
            {
                //return you are not part of this game
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == Game.FirstPlayerId && Game.State != KingSurvivalGameState.TurnKing)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == Game.SecondPlayerId && Game.State != KingSurvivalGameState.TurnPown)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            var furstPlayer = new Player(firstPlayer.UserName, ChessColor.White);
            var scondPlayer = new Player(secondPlayer.UserName, Chess.Common.ChessColor.White);
            var kingSurvivalInitilizeStrategy = new KingSurvivalGameWebInitializationStrategy(oldFen, furstPlayer, scondPlayer);
            var from = new Position(moveFrom[1] - '0', moveFrom[0]);
            var to = new Position(moveTo[1] - '0', moveTo[0]);
            var move = new Move(from, to);
            var inputProvider = new WebInputProvider(move);
            var movementStrategy = new KingSurvivalMovementStrategy();

            this.gameEngine = new KingSurvivalEngineWeb(this, inputProvider, movementStrategy);

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