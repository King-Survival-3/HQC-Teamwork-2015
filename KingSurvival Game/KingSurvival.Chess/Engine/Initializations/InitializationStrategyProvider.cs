namespace KingSurvival.Chess.Engine.Initializations
{
    using System;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;

    public class InitializationStrategyProvider
    {
        public IGameInitializationStrategy GetGameType(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Chess:
                    return new StandartStartGameInitializationStrategy();
                case GameType.KingSurvival:
                    return new KingSurvivalGameInitializationStrategy();
               
                default:
                    throw new ArgumentException();
            }
        }
    }
}
