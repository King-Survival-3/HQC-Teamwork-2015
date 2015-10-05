namespace KingSurvival.Chess.Engine
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Board;
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    public abstract class BaseChessEngine : IChessEngine
    {
        protected IBoard board;

        protected readonly IMovementStrategy movementStrategy;

        protected IList<IPlayer> players;

        protected readonly IRenderer renderer;

        protected readonly IInputProvider input;

        protected int currentPlayerIndex;

        protected BaseChessEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
        {
            this.renderer = renderer;
            this.movementStrategy = movementStrategy; 
            this.input = inputProvider;
            this.board = new Board();
        }

        public void Initialize(IGameInitializationStrategy gameInitializationStrategy)
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
            this.renderer.RenderBoard(this.board);
        }

        public void Start()
        {
            while (true)
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
                }
                catch (Exception exception)
                {
                    this.currentPlayerIndex--;
                    this.renderer.PrintErrorMessage(exception.Message);
                }
            }
        }

        public abstract void WinnginConditions();

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return new List<IPlayer>(this.players);
            }
        }

        protected void SetFirstPlayerIndex()
        {
            for (int i = 0; i < this.players.Count; i++)
            {
                if (this.players[i].Color == ChessColor.White)
                {
                    this.currentPlayerIndex = i - 1;
                    return;
                }
            }
        }

        protected IPlayer GetNextPlayer()
        {
            this.currentPlayerIndex++;
            if (this.currentPlayerIndex >= this.players.Count)
            {
                this.currentPlayerIndex = 0;
            }

            return this.players[this.currentPlayerIndex];
        }

        protected void CheckIfPlayerOwnsFigure(IPlayer player, IFigure figure, Position from)
        {
            if (figure == null)
            {
                throw new InvalidOperationException(string.Format("Position {0}{1} is empty", from.Col, from.Row));
            }

            if (figure.Color != player.Color)
            {
                throw new InvalidOperationException(
                    string.Format("Figure at this position {0}{1} is not yours", from.Col, from.Row));
            }
        }

        protected void CheckIfToPositionIsEmpty(IFigure figure, Position to)
        {
            var figureAtPosition = this.board.GetFigureAtPosition(to);
            if (figureAtPosition != null && figureAtPosition.Color == figure.Color)
            {
                throw new InvalidOperationException(
                   string.Format("You already have a figure at {0}{1}!", to.Col, to.Row));
            }
        }
    }
}
