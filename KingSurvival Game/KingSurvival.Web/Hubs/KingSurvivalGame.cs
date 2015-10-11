namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Movements.Strategies;
    using KingSurvival.Chess.Players;
    using KingSurvival.Data;
    using KingSurvival.Models;
<<<<<<< HEAD
    using KingSurvival.Web.Helpers;
    using KingSurvival.Models.Patterns;
=======
    using KingSurvival.Web.Hubs.Engine;
    using KingSurvival.Web.Hubs.Engine.Initializations;
    using KingSurvival.Web.Hubs.InputProvider;
    using KingSurvival.Web.Hubs.Renderer;
>>>>>>> master

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

<<<<<<< HEAD
            // just updating the state
            if (this.HasKingWon(gameBoard))
            {
                game.State = GameState.GameWonByKing;
            }

            if (this.HasKingLost(gameBoard))
            {
                game.State = GameState.GameWonByPown;
            }

            this.data.SaveChanges();

            // move 
            this.Clients.Group(gameId).move(game.Board);

            // update state 
            this.UpdateState(gameId);
        }

        private bool IsLegalMove(char[,] gameBoard, char figure, Position oldPosition, Position newPosition)
        {
            var emptySpace = this.IsSpaceEmpty(gameBoard, newPosition);
            bool isKing = figure == KingSurvivalGameConstants.King;
            bool canMoveUpLeft = oldPosition.Row + 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col;
            bool canMoveUpRight = oldPosition.Row + 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col;
            bool canMoveDownLeft = oldPosition.Row - 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col;
            bool canMoveDownRight = oldPosition.Row - 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col;

            // check if king can move up left, and the position is empty
            if (isKing && canMoveUpLeft && emptySpace)
            {
                return true;
            }

            // check if king can move up right, and the position is empty
            if (isKing && canMoveUpRight && emptySpace)
            {
                return true;
            }

            // check if figure can move down left, and the position is empty
            if (canMoveDownLeft && emptySpace)
            {
                return true;
            }

            // check if figure can move down right, and the position is empty
            if (canMoveDownRight && emptySpace)
            {
                return true;
            }

            // Illegal move 
            return false;
=======
            gameEngine.Initialize(kingSurvivalInitilizeStrategy);
            gameEngine.Play();
>>>>>>> master
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

<<<<<<< HEAD
        private bool HasKingWon(char[,] gameBoard)
        {
            var kingPosition = this.GetKingPosition(gameBoard);
            var survivalRow = KingSurvivalGameConstants.SurvivalRow;

            if (kingPosition.Row == survivalRow)
            {
                return true;
            }

            return false;
        }

        private bool HasKingLost(char[,] gameBoard)
        {
            var kingPosition = this.GetKingPosition(gameBoard);
            var up = kingPosition.Row + 1;
            var down = kingPosition.Row - 1;
            var left = kingPosition.Col - 1;
            var right = kingPosition.Col + 1;
            var maxRow = gameBoard.GetLength(0);
            var maxCol = gameBoard.GetLength(1);

            // up left
            if (up < maxRow && left >= 0 && gameBoard[up, left] == KingSurvivalGameConstants.EmptySpace)
            {
                return false;
            }

            // up right 
            if (up < maxRow && right < maxCol && gameBoard[up, right] == KingSurvivalGameConstants.EmptySpace)
            {
                return false;
            }

            // down left
            if (down >= 0 && left >= 0 && gameBoard[down, left] == KingSurvivalGameConstants.EmptySpace)
            {
                return false;
            }

            // down right
            if (down >= 0 && right < maxCol && gameBoard[down, right] == KingSurvivalGameConstants.EmptySpace)
            {
                return false;
            }

            return true;
        }

        private Position GetKingPosition(char[,] gameBoard)
        {
            var lastRow = KingSurvivalGameConstants.LastRow;
            var lastCol = KingSurvivalGameConstants.LastCol;

            // check if king is on last row
            for (var row = 0; row < lastRow; row++)
            {
                for (var col = 0; col <= lastCol; col++)
                {
                    if (gameBoard[row, col] == KingSurvivalGameConstants.King)
                    {
                        var factory = new PositionFactory();
                        return factory.Create(row, col);
                        // return new Position(row, col);
                    }
                }
            }

            return null;
        }

        private bool IsSpaceEmpty(char[,] gameBoard, Position position)
        {
            return gameBoard[position.Row, position.Col] == KingSurvivalGameConstants.EmptySpace;
        }

        private char[,] SwapPosition(char[,] board, Position oldPosition, Position newPosition)
        {
            var figure = board[oldPosition.Row, oldPosition.Col];
            board[oldPosition.Row, oldPosition.Col] = KingSurvivalGameConstants.EmptySpace;
            board[newPosition.Row, newPosition.Col] = figure;

            return board;
        }

=======
>>>>>>> master
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