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
            var availableMoves = new List<Square> { };

            availableMoves = AddVerticalMoves(availableMoves, pieceLocation);
            availableMoves = AddHorizontalMoves(availableMoves,pieceLocation);

            return availableMoves;
        }

        private List<Square> AddHorizontalMoves(List<Square> availableMoves, Square pieceLocation)
        {
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                if (pieceLocation.Col != i)
                {
                    availableMoves.Add(new Square(pieceLocation.Row, i));
                }
            }

            return availableMoves;
        }

        private List<Square> AddVerticalMoves(List<Square> availableMoves, Square pieceLocation)
        {
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                if (pieceLocation.Row != i)
                {
                    availableMoves.Add(new Square(i, pieceLocation.Col));
                }
            }

            return availableMoves;
        }
    }
}