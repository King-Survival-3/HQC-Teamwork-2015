namespace KingSurvival.ConsoleClient
{
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Engine.Initializations;
    using KingSurvival.Chess.InputProvider;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Renderer;
    using KingSurvival.Chess.Renderer.Contracts;
    using KingSurvival.Chess.Formatter;

    public static class ChessFacade
    {
        public static void Start()
        {
            while (true)
            {
                var formatter = new FancyFormatter(); // new FancyFormatter(); new StandardFormatter();

                IRenderer renderer = new ConsoleRenderer(formatter);
                renderer.RenderMainMenu();

                IInputProvider inputProvider = new ConsoleInputProvider(formatter);
                var gameType = inputProvider.GetGameType();

                InitializationGameProvider initializationGameProvider = new InitializationGameProvider();

                IMovementStrategy movementStrategy = initializationGameProvider.GetMovementStrategy(gameType);

                IChessEngine chessEngine = initializationGameProvider.GetEngine(gameType, renderer, inputProvider, movementStrategy);

                IGameInitializationStrategy gameInitializationStrategy = initializationGameProvider.GetGameType(gameType);

                chessEngine.Initialize(gameInitializationStrategy);

                chessEngine.Play();
            }
        }
    }
}
