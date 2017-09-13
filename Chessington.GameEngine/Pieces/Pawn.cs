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
            availableMoves.Add(GetPawnSingleVerticalMove(this.Player, board, pawnLocation));
            if (!HasMoved)
            {
            availableMoves.Add(GetPawnDoubleVerticalMove(this.Player,board,pawnLocation));
            }
            return availableMoves;
        }

        

        public Square GetPawnSingleVerticalMove(Player player, Board board, Square pawnLocation)
        {
            if (Player == Player.White)
            {
                return new Square(pawnLocation.Row-1,pawnLocation.Col);
            }
            else
            {
                return new Square(pawnLocation.Row+1,pawnLocation.Col);

            }
        }

        public Square GetPawnDoubleVerticalMove(Player player, Board board, Square pawnLocation)
        {
            if (Player == Player.White)
            {
                return new Square(pawnLocation.Row - 2, pawnLocation.Col);
            }
            else
            {
                return new Square(pawnLocation.Row + 2, pawnLocation.Col);
            }

        }
    }
}