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
        private IList<IPlayer> players;

        protected BaseChessEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
        {
            this.Renderer = renderer;
            this.MovementStrategy = movementStrategy; 
            this.Input = inputProvider;
            this.Board = new Board();
            this.GameState = GameState.Playing;
        }

        public IList<IPlayer> Players
        {
            get
            {
                return new List<IPlayer>(this.players);
            }

            set
            {
                this.players = value;
            }
        }

        protected IMovementStrategy MovementStrategy { get; set; }

        protected IRenderer Renderer { get; set; }

        protected IInputProvider Input { get; set; }

        protected IBoard Board { get; set; }

        protected int CurrentPlayerIndex { get; set; }

        protected GameState GameState { get; set; }

        public virtual void Initialize(IGameInitializationStrategy gameInitializationStrategy)
        {
            // TODO: Remove
            // using Chess.Players;
            // Added for development purposes only 

            // TODO: If players are changed - board is reversed
            this.players = new List<IPlayer>
                               {
                                   new Player("[Black]", ChessColor.Black),
                                   new Player("[White]", ChessColor.White),
                               };

            this.SetFirstPlayerIndex();

            // Use this
            // var players = this.input.GetPlayers(GlobalConstants.StandartGameNumberOfPlayers);
            gameInitializationStrategy.Initialize(this.players, this.Board);
            this.Renderer.RenderBoard(this.Board);
        }

        public virtual void Play()
        {
            while (true)
            {
                try
                {
                    var player = this.GetNextPlayer();
                    var move = this.Input.GetNextPlayerMove(player);
                    var from = move.From;
                    var to = move.To;
                    var figure = this.Board.GetFigureAtPosition(from);
                    this.CheckIfPlayerOwnsFigure(player, figure, from);
                    this.CheckIfToPositionIsEmpty(figure, to);

                    var availableMovements = figure.Move(this.MovementStrategy);

                    foreach (var movement in availableMovements)
                    {
                        movement.ValidateMove(figure, this.Board, move);
                    }

                    this.Board.MoveFigureAtPosition(figure, from, to);

                    this.Renderer.RenderBoard(this.Board);

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

                    if (this.GameState != GameState.Playing)
                    {
                        this.Renderer.RenderWinningScreen(this.GameState.ToString());

                        break;
                    }
                }
                catch (Exception exception)
                {
                    this.CurrentPlayerIndex--;
                    this.Renderer.PrintErrorMessage(exception.Message);
                }
            }
        }

        public abstract void WinnginConditions();

        protected void SetFirstPlayerIndex()
        {
            for (int i = 0; i < this.players.Count; i++)
            {
                if (this.players[i].Color == ChessColor.White)
                {
                    this.CurrentPlayerIndex = i - 1;
                    return;
                }
            }
        }

        protected IPlayer GetNextPlayer()
        {
            this.CurrentPlayerIndex++;
            if (this.CurrentPlayerIndex >= this.players.Count)
            {
                this.CurrentPlayerIndex = 0;
            }

            return this.players[this.CurrentPlayerIndex];
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
            var figureAtPosition = this.Board.GetFigureAtPosition(to);
            if (figureAtPosition != null && figureAtPosition.Color == figure.Color)
            {
                throw new InvalidOperationException(
                   string.Format("You already have a figure at {0}{1}!", to.Col, to.Row));
            }
        }
    }
}
