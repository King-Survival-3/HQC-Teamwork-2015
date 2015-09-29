namespace KingSurvival.Chess.Engine
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Board;
    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Players.Contracts;
    using KingSurvival.Chess.Renderer.Contrats;

    public class StandartTwoPlayerEngine : IChessEngine
    {
        private readonly IList<IPlayer> players;
        private readonly IRenderer renderer;
        private readonly IInputProvider input;

        private readonly IBoard board;


        public StandartTwoPlayerEngine(IRenderer renderer, IInputProvider inputProvider)
        {
            this.renderer = renderer;
            this.input = inputProvider;
            this.board = new Board();
        }

        public void Initialize(IGameInitializationStrategy gameInitializationStrategy)
        {
            var players = this.input.GetPlayers(GlobalConstants.StandartGameNumberOfPlayers);
            gameInitializationStrategy.Initialize(players, this.board);
            this.renderer.RenderBoard(this.board);
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void WinnginConditions()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return new List<IPlayer>(this.players);
            }
        }
    }
}
