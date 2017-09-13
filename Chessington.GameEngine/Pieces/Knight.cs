using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {

            var pieceLocation = board.FindPiece(this);
            var availableMoves = GetAvailableMovesKnight(Player, board, pieceLocation);

            return availableMoves;
        }

        public List<Square> GetAvailableMovesKnight(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square>();
            availableMoves = MoveFinder.GetKnightMoves(availableMoves, board, pieceLocation);
            return availableMoves;
        }

    }
}