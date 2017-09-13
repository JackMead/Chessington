using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var pieceLocation = board.FindPiece(this);
            var availableMoves = GetAvailableMovesQueen(Player,board,pieceLocation);


            return availableMoves;
        }

        public List<Square> GetAvailableMovesQueen(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square>();
            availableMoves = MoveFinder.AddDiagonalMoves(availableMoves, pieceLocation);
            availableMoves = MoveFinder.AddLateralMoves(availableMoves, pieceLocation);
            return availableMoves;
        }

    }
}