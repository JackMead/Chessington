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
            var availableMoves = new List<Square> { };

            availableMoves = AddDiagonalMoves(availableMoves, pieceLocation);

            return availableMoves;
        }

        private List<Square> AddDiagonalMoves(List<Square> availableMoves, Square pieceLocation)
        {

            
            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                var upAndRight = new Square(pieceLocation.Row - i, pieceLocation.Col + i);
                if (Board.IsValidPosition(upAndRight))
                {
                availableMoves.Add(upAndRight);
                }
                var upAndLeft = new Square(pieceLocation.Row - i, pieceLocation.Col - i);
                if (Board.IsValidPosition(upAndLeft))
                {
                    availableMoves.Add(upAndLeft);
                }
                var downAndRight = new Square(pieceLocation.Row + i, pieceLocation.Col + i);
                if (Board.IsValidPosition(downAndRight))
                {
                    availableMoves.Add(downAndRight);
                }
                var downAndLeft = new Square(pieceLocation.Row + i, pieceLocation.Col - i);
                if (Board.IsValidPosition(downAndLeft))
                {
                    availableMoves.Add(downAndLeft);
                }
            }
            return availableMoves;
        }
        
    }
}