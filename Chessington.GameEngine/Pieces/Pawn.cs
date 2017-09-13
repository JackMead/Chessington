using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var pawnLocation = board.FindPiece(this);
            if (Player == Player.White)
            {
                return GetAvailableMovesForWhite(board, pawnLocation);
            }
            else
            {
                return GetAvailableMovesForBlack(board, pawnLocation);
            }
        }

        private IEnumerable<Square> GetAvailableMovesForWhite(Board board, Square pawnLocation)
        {
            var availableMoves = new List<Square> {new Square(pawnLocation.Row-1, pawnLocation.Col)};
            return availableMoves;
        }

        private IEnumerable<Square> GetAvailableMovesForBlack(Board board, Square pawnLocation)
        {
            var availableMoves = new List<Square> { new Square(pawnLocation.Row + 1, pawnLocation.Col) };
            return availableMoves;
        }
    }
}