using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class CannonMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsOrthogonal(piece.Position, to))
            {
                return false;
            }

            var screenCount = CountScreens(piece.Position, to, board);
            var targetPiece = board.GetPieceAt(to);

            if (targetPiece == null) // Not a capture
            {
                return screenCount == 0;
            }
            else // Is a capture
            {
                return screenCount == 1;
            }
        }

        private bool IsOrthogonal(Position from, Position to)
        {
            return from.Row == to.Row || from.Col == to.Col;
        }

        private int CountScreens(Position from, Position to, Board board)
        {
            int count = 0;
            if (from.Row == to.Row) // Horizontal move
            {
                var startCol = Math.Min(from.Col, to.Col);
                var endCol = Math.Max(from.Col, to.Col);
                for (var c = startCol + 1; c < endCol; c++)
                {
                    if (board.GetPieceAt(new Position(from.Row, c)) != null)
                    {
                        count++;
                    }
                }
            }
            else // Vertical move
            {
                var startRow = Math.Min(from.Row, to.Row);
                var endRow = Math.Max(from.Row, to.Row);
                for (var r = startRow + 1; r < endRow; r++)
                {
                    if (board.GetPieceAt(new Position(r, from.Col)) != null)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
} 