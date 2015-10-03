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
        private const string PlayerNameText = "Enter player {0} name: ";

        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            var players = new List<IPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.Clear();
                ConsoleHelpers.SetCursorAtCenter(PlayerNameText.Length);
                Console.Write(PlayerNameText, i);
                string name = Console.ReadLine();
                var player = new Player(name, (ChessColor)i - 1);
                players.Add(player);
            }

            return players;
        }

        public GameType GetGameType()
        {
            // TODO: Fix magic [5] number
            int top = (Console.WindowHeight / 2) + 5;
            int left = Console.WindowWidth / 2;
            Console.SetCursorPosition(left, top);
            
            GameType userGameChoice = (GameType) Enum.Parse(typeof (GameType), Console.ReadLine());

            return userGameChoice;
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
