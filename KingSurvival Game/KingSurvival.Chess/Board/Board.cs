﻿using System;
using System.Linq.Expressions;

namespace KingSurvival.Chess.Board
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Board.Contracts;
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;

    public class Board : IBoard
    {
        private readonly IFigure[,] board;

        public Board(
            int rows = GlobalConstants.StandartGameTotalBoardRows,
            int cols = GlobalConstants.StandartGameTotalBoardCols)
        {
            this.TotalRows = rows;
            this.TotalCols = cols;
            this.board = new IFigure[rows, cols];
        }

        public int TotalRows { get; private set; }

        public int TotalCols { get; private set; }

        public void AddFigure(IFigure figure, Position position)
        {
            ObjectValidator.CheckIfObjectIsNull(figure, GlobalErrorMessages.NullFigureErrorMessage);

            Position.ChechIfValid(position);

            int arrRow = this.GetArrayRow(position.Row);
            int arrCol = this.GetArrayCol(position.Col);
            this.board[arrRow, arrCol] = figure;
        }

        public void CheckIfSquareIsFree(IFigure figure, Position position)
        {
            try
            {
                ObjectValidator.CheckIfObjectIsNull(figure, GlobalErrorMessages.NullFigureErrorMessage);

                Position.ChechIfValid(position);

            }
            catch (Exception)
            {
                throw new ArgumentException(GlobalErrorMessages.InvalidMove);
            }
        }

        public void RemoveFigure(Position position)
        {
            Position.ChechIfValid(position);

            int arrRow = this.GetArrayRow(position.Row);
            int arrCol = this.GetArrayCol(position.Col);
            this.board[arrRow, arrCol] = null;
        }

        public IFigure GetFigureAtPosition(Position position)
        {
            int arrRow = this.GetArrayRow(position.Row);
            int arrCol = this.GetArrayCol(position.Col);

            return this.board[arrRow, arrCol];
        }

        public void MoveFigureAtPosition(IFigure figure, Position from, Position to)
        {
            int arrFromRow = this.GetArrayRow(from.Row);
            int arrFromCol = this.GetArrayCol(from.Col);
            this.board[arrFromRow, arrFromCol] = null;

            int arrToRow = this.GetArrayRow(to.Row);
            int arrToCol = this.GetArrayCol(to.Col);
            this.board[arrToRow, arrToCol] = figure;
        }

        public Position GetKingPosition(ChessColor color)
        {
            var currentRow = 0;
            var currentCol = (char)('a');

            for (int row = 0; row < this.TotalCols; row++)
            {
                for (int col = 0; col < this.TotalRows; col++)
                {
                    currentRow = row;
                    currentCol = (char)('a' + col);
                    var position = new Position(currentRow, currentCol);
                    var figureAtPosition = this.GetFigureAtPosition(position);
                    if (figureAtPosition is Figures.King && figureAtPosition.Color == color)
                    {
                        return new Position(currentRow, currentCol);
                    }
                }
            }

            return new Position(currentRow, currentCol);
        }
        

        private int GetArrayRow(int chessRow)
        {
            return this.TotalRows - chessRow;
        }

        private int GetArrayCol(char chessCol)
        {
            // TODO: lower & uppercase
            return chessCol - 'a';
        }
    }
}