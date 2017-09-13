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
            var availableMoves = new MoveFinder().GetAvailableMovesBishop(Player,board,pieceLocation);

            return availableMoves;
        }
        
        
    }
}