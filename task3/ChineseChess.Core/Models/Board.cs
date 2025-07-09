using System.Collections.Generic;
using System.Linq;

namespace ChineseChess.Core.Models;

public class Board
{
    public List<Piece> Pieces { get; } = new();

    public Piece? GetPieceAt(Position position)
    {
        return Pieces.FirstOrDefault(p => p.Position.Equals(position));
    }

    public void AddPiece(Piece piece)
    {
        Pieces.Add(piece);
    }

    public void MovePiece(Piece piece, Position to)
    {
        var capturedPiece = GetPieceAt(to);
        if (capturedPiece != null)
        {
            Pieces.Remove(capturedPiece);
        }
        piece.Position = to;
    }
} 