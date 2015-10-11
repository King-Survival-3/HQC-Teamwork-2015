namespace KingSurvival.Chess.Engine.Contracts
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Players.Contracts;

    public interface IChessEngine
    {
        IList<IPlayer> Players { get; }

        void Initialize(IGameInitializationStrategy gameInitializationStrategy);

        void Play();

        void WinnginConditions();
    }
}
