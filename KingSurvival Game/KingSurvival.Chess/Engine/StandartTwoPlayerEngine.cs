namespace KingSurvival.Chess.Engine
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Players.Contracts;

    public class StandartTwoPlayerEngine : IChessEngine
    {
        private readonly IEnumerable<IPlayer> players;

        public void Initialize(IGameInitializationStrategy gameInitializationStrategy)
        {
            throw new NotImplementedException();
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
