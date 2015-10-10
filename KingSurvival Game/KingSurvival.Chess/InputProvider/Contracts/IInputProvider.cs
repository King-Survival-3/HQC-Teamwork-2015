namespace KingSurvival.Chess.InputProvider.Contracts
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Players.Contracts;

    public interface IInputProvider
    {
        IList<IPlayer> GetPlayers(int numberOfPlayers);

        Move GetNextPlayerMove(IPlayer player);

        GameType GetGameType();
    }
}