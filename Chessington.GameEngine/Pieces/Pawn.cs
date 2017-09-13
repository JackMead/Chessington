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
            var availableMoves = new List<Square> { };
            availableMoves = new MoveFinder().GetAvailableMovesPawn(Player, board, pawnLocation);
            
            return availableMoves;
        }

        

    }
}