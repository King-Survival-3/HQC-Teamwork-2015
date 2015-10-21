namespace KingSurvival.Chess.Engine
{
    using System;
    using System.CodeDom;
    using System.Linq;

    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Engine.Contracts;
    using KingSurvival.Chess.Figures;
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
                    if (figure is King)
                    {
                        this.GameState = GameState.WhiteWon;
                        break;
                    }
                }
                catch (Exception)
                {
                }
            }


            var kingPosition = this.Board.GetKingPosition(ChessColor.White);

            var toUpLeft = new Position(kingPosition.Row + 1, (char)(kingPosition.Col - 1));
            var toUpRight = new Position(kingPosition.Row + 1, (char)(kingPosition.Col + 1));
            var toDownLeft = new Position(kingPosition.Row - 1, (char)(kingPosition.Col - 1));
            var toDownRight = new Position(kingPosition.Row - 1, (char)(kingPosition.Col + 1));

            if (!(this.CheckKingToOptions(toUpLeft) ||
                this.CheckKingToOptions(toUpRight) ||
                this.CheckKingToOptions(toDownLeft) ||
                this.CheckKingToOptions(toDownRight)))
            {
                this.GameState = GameState.BlackWon;
            }

        }

        private bool CheckKingToOptions(Position to)
        {
            var chessColor = ChessColor.White;
            var king = new King(chessColor);

            try
            {
                this.CheckIfToPositionIsEmpty(king, to);
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}
