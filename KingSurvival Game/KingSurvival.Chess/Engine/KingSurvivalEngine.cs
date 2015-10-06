namespace KingSurvival.Chess.Engine
{
    using System;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;

    public class KingSurvivalEngine : BaseChessEngine, IChessEngine
    {

        public KingSurvivalEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
            : base(renderer, inputProvider, movementStrategy)
        {
        }

        public override void WinnginConditions()
        {
            for (int i = 0; i < this.board.TotalCols; i++)
            {
                try
                {
                    var figure = this.board.GetFigureAtPosition(new Position(this.board.TotalRows, (char)('a' + i)));
                    if (figure is Figures.King)
                    {
                        this.GameState = GameState.WhiteWon;
                        break;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
