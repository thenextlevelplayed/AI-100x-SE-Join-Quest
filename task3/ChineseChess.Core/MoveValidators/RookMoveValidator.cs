using ChineseChess.Core.Models;
using System;

namespace ChineseChess.Core.MoveValidators
{
    public class RookMoveValidator : IMoveValidator
    {
        public bool IsValidMove(Piece piece, Position to, Board board)
        {
            if (!IsOrthogonal(piece.Position, to))
            {
                return false;
            }

            if (HasObstacle(piece.Position, to, board))
            {
                return false;
            }

            return true;
        }

        private bool IsOrthogonal(Position from, Position to)
        {
            return from.Row == to.Row || from.Col == to.Col;
        }

        private bool HasObstacle(Position from, Position to, Board board)
        {
            if (from.Row == to.Row) // Horizontal move
            {
                var startCol = Math.Min(from.Col, to.Col);
                var endCol = Math.Max(from.Col, to.Col);
                for (var c = startCol + 1; c < endCol; c++)
                {
                    if (board.GetPieceAt(new Position(from.Row, c)) != null)
                    {
                        return true;
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
                        return true;
                    }
                }
            }

            return false;
        }
    }
} 