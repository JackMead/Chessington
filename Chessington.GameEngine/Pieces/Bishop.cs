using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var pieceLocation = board.FindPiece(this);
            var availableMoves = GetAvailableMovesBishop(Player,board,pieceLocation);

            return availableMoves;
        }

        public List<Square> GetAvailableMovesBishop(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square> { };
            availableMoves = MoveFinder.AddDiagonalMoves(availableMoves,board, pieceLocation);

            return availableMoves;
        }


    }
}