namespace KingSurvival.Web.Hubs.InputProvider
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Players.Contracts;

    public class WebInputProvider : IInputProvider
    {
        private Move move;

        public WebInputProvider(Move move)
        {
            this.move = move;
        }

        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            throw new NotImplementedException();
        }

        public Move GetNextPlayerMove(IPlayer player)
        {
            return this.move;
        }

        public GameType GetGameType()
        {
            throw new NotImplementedException();
        }
    }
}
