using System;

namespace KingSurvival.Chess.Renderer
{
    using System.Threading;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common.Console;
    using KingSurvival.Chess.Renderer.Contrats;

    public class ConsoleRenderer : IRenderer
    {
        private const string Logo = "Just Chess";

        public void RenderMainMenu()
        {
            ConsoleHelpers.SetCursorAtCenter(Logo.Length);
            Console.WriteLine(Logo);

            // TODO: Add main menu
            Thread.Sleep(1000);
        }

        public void RenderBoard(IBoard board)
        {
            throw new System.NotImplementedException();
        }
    }
}
