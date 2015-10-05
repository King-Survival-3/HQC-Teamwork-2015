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

    public class KingSurvivalGame : Hub, IRenderer
    {
        private readonly IKingSurvivalData data;

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

           // Game.Board = BoardHelper.BoardToFen(board);
            Game.State = Game.State == KingSurvivalGameState.TurnKing ? KingSurvivalGameState.TurnPown : KingSurvivalGameState.TurnKing;

            // just updating the state
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
            return;
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

            // if game doesn't exist
            if (Game == null)
            {
                return;
            }

            var playerID = userId;
            var oldFen = Game.Board;
            var gameBoard = BoardHelper.FenToBoard(oldFen);
            var figure = playerID == Game.FirstPlayerId ? KingSurvivalGameConstants.King : KingSurvivalGameConstants.Pawn;
            
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

            var furstPlayer = new Player("pesho", ChessColor.White);
            var secondPlayer = new Player("Gosho", Chess.Common.ChessColor.White);
            var kingSurvivalInitilizeStrategy = new KingSurvivalGameWebInitializationStrategy(oldFen, furstPlayer, secondPlayer);
            var move = new Move(new Position(moveFrom[1] - '0', moveFrom[0]), new Position(moveTo[1] - '0', moveTo[0]));
            var inputProvider = new WebInputProvider(move);
            var movementStrategy = new KingSurvivalMovementStrategy();

            var kingSurvivalGameEngine = new KingSurvivalEngine(this, inputProvider, movementStrategy);

            kingSurvivalGameEngine.Initialize(kingSurvivalInitilizeStrategy);
            kingSurvivalGameEngine.Play();

            // everything is OK, save changes 
            // gameBoard = this.SwapPosition(gameBoard, oldPosition, newPosition);

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

        private bool IsSpaceEmpty(char[,] gameBoard, Position position)
        {
            return gameBoard[position.Row, position.Col] == KingSurvivalGameConstants.EmptySpace;
        }


        private Game GetGame(string gameId)
        {
            var game = this.data.Game.All().FirstOrDefault(x => x.Id.ToString() == gameId);

            return game;
        }

        //private Position ParceMove(string move)
        //{
        //    Position position;

        //    // if move is not in boundary of array will throw 
        //    try
        //    {
        //        position = MoveHelper.ParceMove(move);
        //    }
        //    catch (ArgumentException)
        //    {
        //        // return clients boards in old state
        //        return null;
        //    }

        //    return position;
        //}





    }
}