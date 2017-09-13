using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {

            var pieceLocation = board.FindPiece(this);
            var availableMoves = GetAvailableMovesKing(Player, board, pieceLocation);

            return availableMoves;
        }

        private List<Square> GetAvailableMovesKing(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square>();
            availableMoves = MoveFinder.GetKingMoves(pieceLocation);
            return availableMoves;
        }
    }
}