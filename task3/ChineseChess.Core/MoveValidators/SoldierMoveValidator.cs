using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class SoldierMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            var from = piece.Position;

            var forwardStep = piece.Color == PieceColor.Red ? 1 : -1;

            // Backward move is always illegal
            if ((piece.Color == PieceColor.Red && to.Row < from.Row) ||
                (piece.Color == PieceColor.Black && to.Row > from.Row))
            {
                return false;
            }

            var rowDiff = Math.Abs(from.Row - to.Row);
            var colDiff = Math.Abs(from.Col - to.Col);

            // Must move one step
            if (rowDiff + colDiff != 1)
            {
                return false;
            }

            bool hasCrossedRiver = (piece.Color == PieceColor.Red && from.Row > 5) ||
                                   (piece.Color == PieceColor.Black && from.Row <= 5);

            if (hasCrossedRiver)
            {
                // Can move forward or sideways
                return (to.Row == from.Row + forwardStep && to.Col == from.Col) || // Forward
                       (to.Row == from.Row && Math.Abs(to.Col - from.Col) == 1);     // Sideways
            }
            else
            {
                // Can only move forward
                return to.Row == from.Row + forwardStep && to.Col == from.Col;
            }
        }
    }
} 