namespace KingSurvival.Chess.Renderer
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Common.Console;
    using KingSurvival.Chess.Formatter.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        private const ConsoleColor DarskSquareConsoleColor = ConsoleColor.DarkGray;
        private const ConsoleColor LightSquareConsoleColor = ConsoleColor.Gray;

        private IFormatter formatter;

        public ConsoleRenderer(IFormatter formatter)
        {
            this.formatter = formatter;
            InitializeConsoleSettings();
        }        

        public void RenderMainMenu()
        {
            this.PrintWelcomeMessage();

            this.PrintSelectGameMenu();
        }

        public void RenderBoard(IBoard board)
        {
            // TODO: Validate Console Dimension
            var startRowPrint = (Console.WindowWidth / 2) - ((board.TotalRows / 2) * ConsoleConstants.CharactersPerRowPerBoardSquare);
            var startColPrint = (Console.WindowHeight / 2) - ((board.TotalCols / 2) * ConsoleConstants.CharactersPerColPerBoardSquare);

            int currentRowPrint = startRowPrint;
            int currentColPrint = startColPrint;

            this.RenderBoarderForBord(startRowPrint, startColPrint, board.TotalRows, board.TotalCols);

            int counter = 1;
            for (int top = 0; top < board.TotalCols; top++)
            {
                for (int left = 0; left < board.TotalRows; left++)
                {
                    currentColPrint = startRowPrint + (left * ConsoleConstants.CharactersPerColPerBoardSquare);
                    currentRowPrint = startColPrint + (top * ConsoleConstants.CharactersPerRowPerBoardSquare);

                    ConsoleColor backgroundColor;

                    if (counter % 2 == 0)
                    {
                        backgroundColor = DarskSquareConsoleColor;
                        Console.BackgroundColor = DarskSquareConsoleColor;
                    }
                    else
                    {
                        backgroundColor = LightSquareConsoleColor;
                        Console.BackgroundColor = LightSquareConsoleColor;
                    }

                    var position = Position.FromArrayCoordinates(top, left, board.TotalRows);

                    var figure = board.GetFigureAtPosition(position);
                    ConsoleHelpers.PrintFigure(figure, backgroundColor, currentRowPrint, currentColPrint);

                    counter++;
                }

                counter++;
            }
        }

        public void PrintErrorMessage(string errorMessage)
        {
            ConsoleHelpers.ClearRow(ConsoleConstants.ConsoleRowForPlayerMessagesAndIO);
            ConsoleHelpers.SetCursorTopCenter(errorMessage.Length);
            Console.Write(this.formatter.Format(errorMessage));
            Thread.Sleep(GlobalConstants.MessageDelayTime);
            ConsoleHelpers.ClearRow(ConsoleConstants.ConsoleRowForPlayerMessagesAndIO);
        }

        public void RenderWinningScreen(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            ConsoleHelpers.SetCursorAtCenter(message.Length);
            Console.WriteLine(this.formatter.Format(message));
            Thread.Sleep(GlobalConstants.MessageDelayTime);
        }

        private static void InitializeConsoleSettings()
        {
            // TODO: Change this something calculated
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.Title = ConsoleConstants.ConsoleChessTitle;
            Console.WindowHeight = ConsoleConstants.ConsoleSettingsHeight;
            Console.WindowWidth = ConsoleConstants.ConsoleSettingsWidth;
            Console.BufferHeight = ConsoleConstants.ConsoleSettingsHeight;
            Console.BufferWidth = ConsoleConstants.ConsoleSettingsWidth;
        }

        private void PrintSelectGameMenu()
        {
            Console.Clear();
            string[] messages = new[] { "Select game by typing the number", "\n", " 1 : Chess", "\n", "2 : King Survival" };

            for (int i = 0; i < messages.Length; i++)
            {
                if (messages[i] != "\n")
                {
                    messages[i] = this.formatter.Format(messages[i]);
                }
            }

            ConsoleHelpers.PrintTextAtCenter(messages);
        }

        private void PrintWelcomeMessage()
        {
            ConsoleHelpers.SetCursorAtCenter(GlobalConstants.LogoTitle.Length);
            var message = this.formatter.Format(GlobalConstants.LogoTitle);
            Console.WriteLine(message);
            Thread.Sleep(1000);
        }

        private void RenderBoarderForBord(int startRowPrint, int startColPrint, int boardTotalRows, int boardTotalCols)
        {
            // Print letters
            var start = startRowPrint + (ConsoleConstants.CharactersPerRowPerBoardSquare / 2);

            for (int i = 0; i < boardTotalCols; i++)
            {
                Console.SetCursorPosition(start + (i * ConsoleConstants.CharactersPerColPerBoardSquare), startColPrint - 1);
                Console.Write((char)('a' + i));

                Console.SetCursorPosition(start + (i * ConsoleConstants.CharactersPerColPerBoardSquare), startColPrint + (boardTotalRows * ConsoleConstants.CharactersPerColPerBoardSquare));
                Console.Write((char)('a' + i));
            }

            // Print numbers
            start = startColPrint + (ConsoleConstants.CharactersPerColPerBoardSquare / 2);
            for (int i = 0; i < boardTotalRows; i++)
            {
                Console.SetCursorPosition(startRowPrint - 1, start + (i * ConsoleConstants.CharactersPerColPerBoardSquare));
                Console.WriteLine(boardTotalRows - i);

                Console.SetCursorPosition(startRowPrint + (boardTotalCols * ConsoleConstants.CharactersPerColPerBoardSquare), start + (i * ConsoleConstants.CharactersPerColPerBoardSquare));
                Console.WriteLine(boardTotalRows - i);
            }

            // Top Boarder Line
            for (int i = startRowPrint - 2;
                 i <= (startRowPrint + (boardTotalRows * ConsoleConstants.CharactersPerRowPerBoardSquare) + 1);
                 i++)
            {
                Console.BackgroundColor = DarskSquareConsoleColor;
                Console.SetCursorPosition(i, startColPrint - 2);
                Console.WriteLine(" ");
            }

            // Bottom Boarder Line
            for (int i = startRowPrint - 2;
                 i <= startRowPrint + (boardTotalRows * ConsoleConstants.CharactersPerRowPerBoardSquare) + 1;
                 i++)
            {
                Console.BackgroundColor = DarskSquareConsoleColor;
                Console.SetCursorPosition(
                    i,
                    startColPrint + (boardTotalRows * ConsoleConstants.CharactersPerRowPerBoardSquare) + 1);
                Console.WriteLine(" ");
            }

            // Right Boarder Line
            for (int i = startColPrint - 2;
                 i <= (startColPrint + (boardTotalCols * ConsoleConstants.CharactersPerColPerBoardSquare) + 1);
                 i++)
            {
                Console.BackgroundColor = DarskSquareConsoleColor;
                Console.SetCursorPosition(
                    startRowPrint + (boardTotalCols * ConsoleConstants.CharactersPerColPerBoardSquare) + 1, 
                    i);
                Console.WriteLine(" ");
            }

            // Left Boarder Line
            for (int i = startColPrint - 2;
                 i <= startColPrint + (boardTotalCols * ConsoleConstants.CharactersPerColPerBoardSquare) + 1;
                 i++)
            {
                Console.BackgroundColor = DarskSquareConsoleColor;
                Console.SetCursorPosition(startRowPrint - 2, i);
                Console.WriteLine(" ");
            }
        }
    }
}