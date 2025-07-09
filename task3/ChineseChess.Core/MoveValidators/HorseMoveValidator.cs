using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class HorseMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsLShapeMove(piece.Position, to))
            {
                return false;
            }

            if (IsLegBlocked(piece.Position, to, board))
            {
                return false;
            }

            return true;
        }

        private bool IsLShapeMove(Position from, Position to)
        {
            var rowDiff = Math.Abs(from.Row - to.Row);
            var colDiff = Math.Abs(from.Col - to.Col);

            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        private bool IsLegBlocked(Position from, Position to, Board board)
        {
            var rowDiff = to.Row - from.Row;
            var colDiff = to.Col - from.Col;

            Position blockPos;

            if (Math.Abs(rowDiff) == 2) // Moving 2 rows
            {
                blockPos = new Position(from.Row + Math.Sign(rowDiff), from.Col);
            }
            else // Moving 2 cols
            {
                blockPos = new Position(from.Row, from.Col + Math.Sign(colDiff));
            }

            return board.GetPieceAt(blockPos) != null;
        }
    }
} 