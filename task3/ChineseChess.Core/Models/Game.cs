using ChineseChess.Core.MoveValidators;
using System.Collections.Generic;
using System.Linq;

namespace ChineseChess.Core.Models
{
    public class Game
    {
        public Board Board { get; } = new();
        public PieceColor CurrentPlayer { get; private set; } = PieceColor.Red;
        public bool IsOver { get; private set; }

        private readonly Dictionary<PieceType, IMoveValidator> _moveValidators;

        public Game()
        {
            _moveValidators = new Dictionary<PieceType, IMoveValidator>
            {
                { PieceType.General, new GeneralMoveValidator() },
                { PieceType.Guard, new GuardMoveValidator() },
                { PieceType.Rook, new RookMoveValidator() },
                { PieceType.Horse, new HorseMoveValidator() },
                { PieceType.Cannon, new CannonMoveValidator() },
                { PieceType.Elephant, new ElephantMoveValidator() },
                { PieceType.Soldier, new SoldierMoveValidator() }
                // Other validators will be added here
            };
        }

        public bool MakeMove(Position from, Position to)
        {
            var piece = Board.GetPieceAt(from);
            if (piece == null || piece.Color != CurrentPlayer)
            {
                return false;
            }

            if (_moveValidators.TryGetValue(piece.Type, out var validator))
            {
                if (!validator.IsValidMove(piece, to, Board))
                {
                    return false;
                }
            }
            else
            {
                // No validator for this piece type yet, deny move
                return false;
            }
            
            var targetPiece = Board.GetPieceAt(to);
            if (targetPiece != null)
            {
                if (targetPiece.Color == piece.Color)
                {
                    return false; // Cannot capture own piece
                }
                if (targetPiece.Type == PieceType.General)
                {
                    IsOver = true;
                }
            }

            Board.MovePiece(piece, to);

            if (IsOver)
            {
                // if game is over, no need to switch player
                return true;
            }

            SwitchPlayer();
            return true;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == PieceColor.Red) ? PieceColor.Black : PieceColor.Red;
        }
    }
} 