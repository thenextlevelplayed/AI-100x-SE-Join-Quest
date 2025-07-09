using ChineseChess.Core.Models;

namespace ChineseChess.Core.MoveValidators;

public interface IMoveValidator
{
    bool IsValidMove(Piece piece, Position to, Board board);
} 