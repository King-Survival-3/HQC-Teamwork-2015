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


    public class KingSurvivalGame : Hub
    {
        private const char King = 'K';
        private const char EmptySpace = '-';
        private const char Pown = 'p';

        private IKingSurvivalData data;

        public KingSurvivalGame()
            : this(new KingSurvivalData(new KingSurvivalDbContext()))
        {

        }

        public KingSurvivalGame(IKingSurvivalData data)
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

        public void GameEngine(string gaemId, string userId, string moveFrom, string moveTo)
        {

            var game = GetGame(gaemId);
            var playerID = userId;
            var oldFen = game.Board;
            var gameId = game.Id.ToString();
            var gameBoard = BoardHelper.FenToBoard(oldFen);
            var figure = playerID == game.FirstPlayerId ? King : Pown;
            var oldPosition = ParceMove(moveFrom);
            var newPosition = ParceMove(moveTo);

            //something wrong with position 
            if (oldPosition == null || newPosition == null)
            {
                Clients.Group(gameId).move(oldFen);
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
            if (playerID == game.FirstPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != King)
            {
                // you move wrong figure
                Clients.Group(gameId).move(oldFen);
                return;
            }

            if (playerID == game.SecondPlayerId && gameBoard[oldPosition.Row, oldPosition.Col] != Pown)
            {
                // you move wrong figure
                Clients.Group(gameId).move(oldFen);
                return;
            }

            //check is move is legal
            if (!IsLegalMove(gameBoard, figure, oldPosition, newPosition))
            {
                //move is not legal return
                Clients.Group(gameId).move(oldFen);
                return;
            }


            //everything is OK, save changes 
            gameBoard = this.SwapPosition(gameBoard, oldPosition, newPosition);
            game.Board = BoardHelper.BoardToFen(gameBoard);
            game.State = game.State == GameState.TurnKing ? GameState.TurnPown : GameState.TurnKing;

            //just updating the state
            if (ISKingWon(gameBoard))
            {
                game.State = GameState.GameWonByKing;
            }

            if (IsKingLost(gameBoard))
            {
                game.State = GameState.GameWonByPown;
            }

            this.data.SaveChanges();

            //move 
            Clients.Group(gameId).move(game.Board);

            //update state 
            UpdateState(gameId);
        }

        private bool IsLegalMove(char[,] gameBoard, char figure, Position oldPosition, Position newPosition)
        {
            bool emptySpace = IsSpaceEmpty(gameBoard, newPosition);

            //check if king can move up left, and the position is empty
            if (figure == King && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            //check if king can move up right, and the position is empty
            if (figure == King && oldPosition.Row + 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            //check if figure can move down left, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col - 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            // //check if figure can move down right, and the position is empty
            if (oldPosition.Row - 1 == newPosition.Row && oldPosition.Col + 1 == newPosition.Col && emptySpace)
            {
                return true;
            }

            //Illegal move 
            return false;
        }

        private void UpdateState(string gameId)
        {
            var gameState = this.data.Game.All()
                .Select(x => new { Id = x.Id, gameState = x.State })
               .FirstOrDefault(x => x.Id.ToString() == gameId);

            Clients.Group(gameId).updateGameState(gameState);
        }

        private bool ISKingWon(char[,] gameBoard)
        {
            var kingPosition = GetKingPosition(gameBoard);
            int survivalRoww = 7;

            if (kingPosition.Row == survivalRoww)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsKingLost(char[,] gameBoard)
        {
            var kingPosition = GetKingPosition(gameBoard);
            int up = kingPosition.Row + 1;
            int down = kingPosition.Row - 1;
            int left = kingPosition.Col - 1;
            int right = kingPosition.Col + 1;
            int maxRow = gameBoard.GetLength(0);
            int maxCol = gameBoard.GetLength(1);

            //up left
            if (up < maxRow && left >= 0 && gameBoard[up, left] == EmptySpace)
            {
                return false;
            }

            //up right 
            if (up < maxRow && right < maxCol && gameBoard[up, right] == EmptySpace)
            {
                return false;
            }

            //down left
            if (down >= 0 && left >= 0 && gameBoard[down, left] == EmptySpace)
            {
                return false;
            }

            //down right
            if (down >= 0 && right < maxCol && gameBoard[down, right] == EmptySpace)
            {
                return false;
            }

            return true;
        }

        private Position GetKingPosition(char[,] gameBoard)
        {
            int lastRow = 7;
            int lastCol = 7;

            //check if king is on last row

            for (int row = 0; row < lastRow; row++)
            {
                for (int col = 0; col <= lastCol; col++)
                {
                    if (gameBoard[row, col] == King)
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null;
        }

        private bool IsSpaceEmpty(char[,] gameBoard, Position position)
        {
            return gameBoard[position.Row, position.Col] == EmptySpace;
        }

        private char[,] SwapPosition(char[,] board, Position oldPosition, Position newPosition)
        {
            var figure = board[oldPosition.Row, oldPosition.Col];
            board[oldPosition.Row, oldPosition.Col] = EmptySpace;
            board[newPosition.Row, newPosition.Col] = figure;

            return board;
        }

        private Game GetGame(string gameId)
        {
            var game = this.data.Game
               .All()
               .FirstOrDefault(x => x.Id.ToString() == gameId);

            return game;
        }

        private Position ParceMove(string move)
        {
            Position position;

            //if move is not in boundary of array will throw 
            try
            {
                position = MoveHelper.ParceMove(move);

            }
            catch (ArgumentException)
            {
                //return clients boards in old state
                return null;
            }

            return position;
        }
    }
}