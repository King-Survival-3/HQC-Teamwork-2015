namespace KingSurvival.Chess.Engine
{
    using KingSurvival.Chess.Board;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    // TODO: Revemo class?
    class KingSurvivalEngine : BaseChessEngine, IChessEngine
    {
        public KingSurvivalEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
            : base(renderer, inputProvider, movementStrategy)
        {
        }

        public override void WinnginConditions()
        {
            throw new System.NotImplementedException();
        }
    }
}
