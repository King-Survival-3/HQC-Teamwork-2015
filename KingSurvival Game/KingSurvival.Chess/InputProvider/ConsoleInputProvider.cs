namespace KingSurvival.Chess.InputProvider
{
    using System;
    using System.Collections.Generic;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Common.Console;
    using KingSurvival.Chess.Formatter.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;

    public class ConsoleInputProvider : IInputProvider
    {
        private const string PlayerNameText = "Enter player {0} name: ";

        private IFormatter formatter;

        public ConsoleInputProvider(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public IList<IPlayer> GetPlayers(int numberOfPlayers)
        {
            var players = new List<IPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.Clear();
                ConsoleHelpers.SetCursorAtCenter(PlayerNameText.Length);
                Console.Write(this.formatter.Format(String.Format(PlayerNameText, i)));
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

            var userGameChoice = Console.ReadLine();

            if (string.IsNullOrEmpty(userGameChoice))
            {
                var message = this.formatter.Format("Invalid choice");
                throw new ArgumentException(message);
            }

            GameType userGameTypeChoice = (GameType)Enum.Parse(typeof(GameType), userGameChoice);

            return userGameTypeChoice;   
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
            ConsoleHelpers.ClearRow(ConsoleConstants.ConsoleRowForPlayerMessagesAndIO);
            var message = this.formatter.Format(string.Format("{0} is next: ", player.Name));

            ConsoleHelpers.SetCursorTopCenter(message.Length);
            Console.WriteLine(message);
            ConsoleHelpers.SetCursorReadyToAcceptCommands(message.Length);

            var positionAsString = Console.ReadLine().Trim().ToLower();
            var move = ConsoleHelpers.CreateMoveFromCommand(positionAsString);

            return move;
        }
    }
}
