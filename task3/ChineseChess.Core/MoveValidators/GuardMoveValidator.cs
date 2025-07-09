using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class GuardMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsWithinPalace(piece.Color, to))
            {
                return false;
            }

            if (!IsOneStepDiagonal(piece.Position, to))
            {
                return false;
            }

            return true;
        }

        private bool IsWithinPalace(PieceColor color, Position pos)
        {
            if (pos.Col < 4 || pos.Col > 6) return false;

            return color == PieceColor.Red
                ? pos.Row >= 1 && pos.Row <= 3
                : pos.Row >= 8 && pos.Row <= 10;
        }

        private bool IsOneStepDiagonal(Position from, Position to)
        {
            var rowDiff = Math.Abs(from.Row - to.Row);
            var colDiff = Math.Abs(from.Col - to.Col);

            return rowDiff == 1 && colDiff == 1;
        }
    }
} 