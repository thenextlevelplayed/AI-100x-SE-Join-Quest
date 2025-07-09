using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class ElephantMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsTwoStepDiagonal(piece.Position, to))
            {
                return false;
            }

            if (IsCrossingRiver(piece.Color, to))
            {
                return false;
            }

            if (IsMidpointBlocked(piece.Position, to, board))
            {
                return false;
            }

            return true;
        }

        private bool IsTwoStepDiagonal(Position from, Position to)
        {
            var rowDiff = Math.Abs(from.Row - to.Row);
            var colDiff = Math.Abs(from.Col - to.Col);

            return rowDiff == 2 && colDiff == 2;
        }

        private bool IsCrossingRiver(PieceColor color, Position to)
        {
            return color == PieceColor.Red ? to.Row > 5 : to.Row <= 5;
        }

        private bool IsMidpointBlocked(Position from, Position to, Board board)
        {
            var midRow = (from.Row + to.Row) / 2;
            var midCol = (from.Col + to.Col) / 2;
            var midPoint = new Position(midRow, midCol);

            return board.GetPieceAt(midPoint) != null;
        }
    }
} 