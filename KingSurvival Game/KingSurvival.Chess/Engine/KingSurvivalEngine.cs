namespace KingSurvival.Chess.Engine
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Engine.Contracts;
    
    using KingSurvival.Chess.Players.Contracts;

    class KingSurvivalEngine : IChessEngine
    {
        private readonly IEnumerable<IPlayer> players;

        public void Initialize()
        {
            throw new System.NotImplementedException();
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
