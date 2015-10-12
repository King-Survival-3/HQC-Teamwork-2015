namespace KingSurvival.Chess.Engine
{
    using System;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.InputProvider.Contracts;
    using KingSurvival.Chess.Movements.Contracts;
    using KingSurvival.Chess.Renderer.Contracts;
    using KingSurvival.Chess.Figures.Contracts;

    public class KingSurvivalEngine : BaseChessEngine, IChessEngine
    {
        public KingSurvivalEngine(IRenderer renderer, IInputProvider inputProvider, IMovementStrategy movementStrategy)
            : base(renderer, inputProvider, movementStrategy)
        {
        }

        public override void WinnginConditions()
        {
            for (int i = 0; i < this.Board.TotalCols; i++)
            {
                try
                {
                    var figure = this.Board.GetFigureAtPosition(new Position(this.Board.TotalRows, (char)('a' + i)));
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

            //for (int row = 0; row < this.Board.TotalCols; row++)
            //{
            //    for (int col = 0; col < this.Board.TotalRows; col++)
            //    {
            //        var currentRow = row;
            //        var currentCol = (char) ('a' + col);
            //        var position = new Position(currentRow, currentCol);
            //        var figureAtPosition = this.Board.GetFigureAtPosition(position);
            //        if (figureAtPosition is Figures.King)
            //        {
            //            CheckForAvailableMoves(currentRow, currentCol);
            //        }
            //    }
            //}
        }

        private void CheckForAvailableMoves(int row, char col)
        {
            var upAndLeft = new Position(row + 1, (char)(col - 1));
            var upAndRight = new Position(row + 1, (char)(col + 1));
            var downAndLeft = new Position(row - 1, (char)(col - 1));
            var downAndRight = new Position(row - 1, (char)(col + 1));

            //if (!CheckMove(upAndLeft) && !CheckMove(upAndRight) && !CheckMove(downAndLeft) && !CheckMove(downAndRight))
            //{
            //    this.GameState = GameState.BlackWon;
            //}
        }

        private bool CheckMove(Position upAndLeft)
        {
            return true;

            //try
            //{
            //    Position.ChechIfValid(upAndLeft);

            //    var figure = this.Board.GetFigureAtPosition(upAndLeft);
            //    if (figure != null)
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}

            //return true;
        }
    }
}
