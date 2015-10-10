namespace KingSurvival.Chess.Engine.Contracts
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Players.Contracts;

    public interface IGameInitializationStrategy
    {
        void Initialize(IList<IPlayer> players, IBoard board);
    }
}
