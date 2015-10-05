namespace KingSurvival.Chess.Engine.Initializations
{
    using System;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Movements.Strategies;

    public class InitializationGameProvider
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

        public IMovementStrategy GetMovementStrategy(GameType gameType)
        {

            switch (gameType)
            {
                case GameType.Chess:
                    return new NormalMovementStrategy();
                case GameType.KingSurvival:
                    return new KingSurvivalMovementStrategy();
                default:
                    throw new ArgumentException();
            }

        }
    }
}
