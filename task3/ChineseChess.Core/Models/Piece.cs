namespace ChineseChess.Core.Models;

public class Piece
{
    public PieceType Type { get; }
    public PieceColor Color { get; }
    public Position Position { get; set; }

    public Piece(PieceType type, PieceColor color, Position position)
    {
        Type = type;
        Color = color;
        Position = position;
    }
} 