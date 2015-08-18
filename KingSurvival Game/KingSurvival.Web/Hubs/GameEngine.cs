namespace KingSurvival.Web.Hubs
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.SignalR;

    using System.Web;
    using Microsoft.AspNet.Identity;

    using KingSurvival.Data;
    using KingSurvival.Models;
    using KingSurvival.Web.Helpers;


    public class GameEngine : Hub
    {
        private IKingSurvivalData data;

        public GameEngine()
            : this(new KingSurvivalData(new KingSurvivalDbContext()))
        {

        }

        public GameEngine(IKingSurvivalData data)
        {
            this.data = data;
        }

        //Name of the room is same like the game Id
        public void JoinRoom(string room)
        {
            if (room != null)
            {
                Groups.Add(Context.ConnectionId, room);
                Clients.Caller.joinRoom(room);
                UpdateState(room);
            }
        }

        public void Moves(string room, string userId, string moveFrom, string moveTo, string fen)
        {
            var game = this.data.Game
                .All()
                .FirstOrDefault(x => x.Id.ToString() == room);

            Position oldPosition;
            Position newPosition;

            var playerID = userId;
            var oldFen = game.Board;
            var gameId = game.Id.ToString();
            var gameBoard = BoardHelper.FenToBoard(oldFen);
            var figure = playerID == game.FirstPlayerId ? 'K' : 'p';

            //if move is not in boundary of array will throw 
            try
            {
                oldPosition = MoveHelper.ParceMove(moveFrom);
                newPosition = MoveHelper.ParceMove(moveTo);
            }
            catch (ArgumentException)
            {
                //return clients boards in old state
                Clients.Group(gameId).move(oldFen);
                return;
            }

            //nothing to move
            if (moveFrom == moveTo)
            {
                return;
            }

            //check If player is 1 or 2 
            if (playerID != game.FirstPlayerId && playerID != game.SecondPlayerId)
            {
                //return you are not part of this game
                Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.FirstPlayerId && game.State != GameState.TurnKing)
            {
                //this is not your turn
                Clients.Group(gameId).move(oldFen);
                return;
            }


            if (playerID == game.SecondPlayerId && game.State != GameState.TurnPown)
            {
                //this is not your turn
                Clients.Group(gameId).move(oldFen);
                return;
            }

            //check for correct figure
            if (playerID == game.FirstPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != 'K')
            {
                // you move wrong figure
                Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.SecondPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != 'p')
            {
                // you move wrong figure
                Clients.Group(gameId).move(oldFen);
                return;
            }

            //check is move is legal
            if (!MoveIsLegal(gameBoard, figure, oldPosition, newPosition))
            {
                //move is not legal return
                Clients.Group(gameId).move(oldFen);
                return;
            }

            //just updating the state
            if (GameEnd(gameBoard))
            {
                //TODO: 
            }

            //everything is OK, save changes 
            gameBoard = this.SwapPosition(gameBoard, oldPosition, newPosition);
            game.Board = BoardHelper.BoardToFen(gameBoard);
            game.State = game.State == GameState.TurnKing ? GameState.TurnPown : GameState.TurnKing;

            this.data.SaveChanges();

            //move 
            Clients.Group(gameId).move(game.Board);

            //update state 
            UpdateState(gameId);
        }

        private bool MoveIsLegal(char[,] gameBoard, char figure, Position oldPosition, Position newPosition)
        {
            var king = 'K';
            var emptySpace = '-';

            //check if king can move up left, and the position is empty
            if (figure == king && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col && gameBoard[newPosition.Row, newPosition.Col] == emptySpace)
            {
                return true;
            }

            //check if king can move up right, and the position is empty
            if (figure == king && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col && gameBoard[newPosition.Row, newPosition.Col] == emptySpace)
            {
                return true;
            }

            //check if figure can move down left, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col && gameBoard[newPosition.Row, newPosition.Col] == '-')
            {
                return true;
            }

            // //check if figure can move down right, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col && gameBoard[newPosition.Row, newPosition.Col] == '-')
            {
                return true;
            }

            //Illegal move 
            return false;
        }

        private bool GameEnd(char[,] gameBoard)
        {
            //TODO
            return false;
        }

        private char[,] SwapPosition(char[,] board, Position oldPosition, Position newPosition)
        {
            var emptySpase = '-';
            var figure = board[oldPosition.Row, oldPosition.Col];
            board[oldPosition.Row, oldPosition.Col] = emptySpase;
            board[newPosition.Row, newPosition.Col] = figure;

            return board;
        }

        private void UpdateState(string gameId)
        {
            var gameState = this.data.Game.All()
                .Select(x => new { Id = x.Id, gameState = x.State })
               .FirstOrDefault(x => x.Id.ToString() == gameId);

            Clients.Group(gameId).updateGameState(gameState);
        }
    }
}