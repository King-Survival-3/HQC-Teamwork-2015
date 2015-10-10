namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Linq;

    using KingSurvival.Data;
    using KingSurvival.Models;
    using KingSurvival.Web.Helpers;

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
        // NOT USED!!!
        // TODO: Remove
        public void JoinRoom(string room)
        {
            if (string.IsNullOrEmpty(room))
            {
                this.Groups.Add(this.Context.ConnectionId, room);
                this.Clients.Caller.joinRoom(room);
                this.UpdateState(room);
            }
        }

        public void GameEngine(string gameId, string userId, string moveFrom, string moveTo)
        {
            var game = this.GetGame(gameId);

            // if game doesn't exist
            if (game == null)
            {
                return;
            }

            var playerID = userId;
            var oldFen = game.Board;
            var gameBoard = BoardHelper.FenToBoard(oldFen);
            var figure = playerID == game.FirstPlayerId ? KingSurvivalGameConstants.King : KingSurvivalGameConstants.Pawn;
            var oldPosition = this.ParceMove(moveFrom);
            var newPosition = this.ParceMove(moveTo);

            // something wrong with position 
            if (oldPosition == null || newPosition == null)
            {
                this.Clients.Group(gameId).move(oldFen);
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

            if (playerID == game.FirstPlayerId && game.State != GameState.TurnKing)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.SecondPlayerId && game.State != GameState.TurnPown)
            {
                // this is not your turn
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            // check for correct figure
            if (playerID == game.FirstPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != KingSurvivalGameConstants.King)
            {
                // you move wrong figure
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.SecondPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != KingSurvivalGameConstants.Pawn)
            {
                // you move wrong figure
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            // check is move is legal
            if (!this.IsLegalMove(gameBoard, figure, oldPosition, newPosition))
            {
                // move is not legal return
                this.Clients.Group(gameId).move(oldFen);
                return;
            }

            // everything is OK, save changes 
            gameBoard = this.SwapPosition(gameBoard, oldPosition, newPosition);
            game.Board = BoardHelper.BoardToFen(gameBoard);
            game.State = game.State == GameState.TurnKing ? GameState.TurnPown : GameState.TurnKing;

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

            // check if king can move up left, and the position is empty
            if (figure == KingSurvivalGameConstants.King && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col
                && emptySpace)
            {
                return true;
            }

            // check if king can move up right, and the position is empty
            if (figure == KingSurvivalGameConstants.King && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col
                && emptySpace)
            {
                return true;
            }

            // check if figure can move down left, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            // check if figure can move down right, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            // Illegal move 
            return false;
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
                        return new Position(row, col);
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

        private Game GetGame(string gameId)
        {
            var game = this.data.Game.All().FirstOrDefault(x => x.Id.ToString() == gameId);

            return game;
        }

        private Position ParceMove(string move)
        {
            Position position;

            // if move is not in boundary of array will throw 
            try
            {
                position = MoveHelper.ParceMove(move);
            }
            catch (ArgumentException)
            {
                // return clients boards in old state
                return null;
            }

            return position;
        }
    }
}