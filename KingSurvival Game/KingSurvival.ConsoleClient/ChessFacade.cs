namespace KingSurvival.ConsoleClient
{
    using System;

    using KingSurvival.Chess.Board;
    using KingSurvival.Chess.Engine;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Engine.Initializations;
    using KingSurvival.Chess.InputProvider;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Movements.Strategies;
    using KingSurvival.Chess.Renderer;
    using KingSurvival.Chess.Renderer.Contracts;

    public static class ChessFacade
    {
        public static void Start()
        {
            IRenderer renderer = new ConsoleRenderer();
            renderer.RenderMainMenu();

            IInputProvider inputProvider = new ConsoleInputProvider();
            var gameType = inputProvider.GetGameType();

            InitializationGameProvider initializationGameProvider = new InitializationGameProvider();

            IMovementStrategy movementStrategy = initializationGameProvider.GetMovementStrategy(gameType);

            IChessEngine chessEngine = initializationGameProvider.GetEngine(gameType, renderer, inputProvider, movementStrategy);

            IGameInitializationStrategy gameInitializationStrategy = initializationGameProvider.GetGameType(gameType);

            chessEngine.Initialize(gameInitializationStrategy);

            chessEngine.Play();

            Console.ReadLine();
        }
    }
}
