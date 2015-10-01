namespace KingSurvival.Chess.InputProvider
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Common.Console;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;

    public class ConsoleInputProvider : IInputProvider
    {
        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            var players = new List<IPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.Clear();
                var message = String.Format("Enter player {0} name: ", i);
                ConsoleHelpers.SetCursorAtCenter(message.Length);
                Console.Write(message);
                string name = Console.ReadLine();
                var player = new Player(name, (ChessColor)i - 1);
                players.Add(player);
            }

            return players;
        }

        /// <summary>
        /// Command is in format 
        ///     a5-c5
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Move GetNextPlayerMove(IPlayer player)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            var message = string.Format("{0} is next: ", player.Name);

            ConsoleHelpers.SetCursorTopCenter(message.Length);
            Console.WriteLine(message);
            ConsoleHelpers.SetCursorReadyToAcceptCommands(message.Length);

            var positionAsString = Console.ReadLine().Trim().ToLower();
            var move = ConsoleHelpers.CreateMoveFromCommand(positionAsString);

            return move;
        }
    }
}
