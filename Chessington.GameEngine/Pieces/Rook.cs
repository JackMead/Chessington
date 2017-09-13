using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var pieceLocation = board.FindPiece(this);
            var availableMoves = GetAvailableMovesRook(Player,board, pieceLocation);

            return availableMoves;
        }

        public List<Square> GetAvailableMovesRook(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square>();

            availableMoves = MoveFinder.AddVerticalMoves(availableMoves, pieceLocation);
            availableMoves = MoveFinder.AddHorizontalMoves(availableMoves, pieceLocation);
            return availableMoves;
        }

    }
}