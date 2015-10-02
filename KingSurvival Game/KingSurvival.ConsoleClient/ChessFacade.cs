namespace KingSurvival.ConsoleClient
{
    using System;

    using KingSurvival.Chess.Engine;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Engine.Initializations;
    using KingSurvival.Chess.InputProvider;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Renderer;
    using KingSurvival.Chess.Renderer.Contracts;

    public static class ChessFacade
    {
        public static void Start()
        {
            IRenderer renderer = new ConsoleRenderer();
            renderer.RenderMainMenu();

            IInputProvider inputProvider = new ConsoleInputProvider();

            IChessEngine chessEngine = new StandartTwoPlayerEngine(renderer, inputProvider);

            IGameInitializationStrategy gameInitializationStrategy = new StandartStartGameInitializationStrategy();

            chessEngine.Initialize(gameInitializationStrategy);

            chessEngine.Start();

            Console.ReadLine();
        }
    }
}
