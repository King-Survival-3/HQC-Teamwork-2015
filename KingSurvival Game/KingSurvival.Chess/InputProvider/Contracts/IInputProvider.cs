namespace KingSurvival.Chess.InputProvider.Contracts
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Players.Contracts;

    public interface IInputProvider
    {
        IList<IPlayer> GetPlayers(int numberOfPlayers);
    }
}
