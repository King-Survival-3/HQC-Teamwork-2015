namespace KingSurvival.Chess.Engine
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    class KingSurvivalEngine : BaseChessEngine, IChessEngine
    {

        public KingSurvivalEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
            : base(renderer, inputProvider, movementStrategy)
        {
        }

        public override void WinnginConditions()
        {
            for (int i = 0; i < this.board.TotalCols; i++)
            {
                var row = this.board.TotalRows;
                var col = (char)(i + 'а');

                var figure = this.board.GetFigureAtPosition(new Position(row, col));
                
                if (figure is Figures.King)
                {
                    this.gameState = GameState.WhiteWon;
                }
            }
        }
    }
}
