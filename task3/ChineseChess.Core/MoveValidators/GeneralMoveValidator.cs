using ChineseChess.Core.Models;
using System;
using System.Linq;

namespace ChineseChess.Core.MoveValidators
{
    public class GeneralMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsWithinPalace(piece.Color, to))
            {
                return false;
            }

            if (!IsOneStepOrthogonal(piece.Position, to))
            {
                return false;
            }

            if (AreGeneralsFacing(piece, to, board))
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

        private bool IsOneStepOrthogonal(Position from, Position to)
        {
            var rowDiff = Math.Abs(from.Row - to.Row);
            var colDiff = Math.Abs(from.Col - to.Col);

            return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1);
        }

        private bool AreGeneralsFacing(Piece movingGeneral, Position to, Board board)
        {
            var otherGeneral = board.Pieces.SingleOrDefault(p => p.Type == PieceType.General && p.Color != movingGeneral.Color);
            if (otherGeneral == null)
            {
                return false; // No other general on the board, so they can't be facing.
            }

            // This logic is simplified. It assumes the moving piece is a general.
            if (otherGeneral.Position.Col != to.Col)
            {
                return false;
            }

            var startRow = Math.Min(to.Row, otherGeneral.Position.Row);
            var endRow = Math.Max(to.Row, otherGeneral.Position.Row);

            for (var r = startRow + 1; r < endRow; r++)
            {
                if (board.GetPieceAt(new Position(r, to.Col)) != null)
                {
                    return false; // There's a piece in between
                }
            }

            return true; // Generals are facing with no pieces in between
        }
    }
} 