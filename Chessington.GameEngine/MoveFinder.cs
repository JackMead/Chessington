using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    class MoveFinder
    {
        private List<Square> availableMoves;

        public List<Square> GetAvailableMovesPawn(Player player, Board board, Square pieceLocation)
        {
            availableMoves = new List<Square>();
            availableMoves.Add(GetPawnSingleVerticalMove(player, board, pieceLocation));
            if (!board.GetPiece(pieceLocation).HasMoved)
            {
                availableMoves.Add(GetPawnDoubleVerticalMove(player, board, pieceLocation));
            }
            return availableMoves;
        }
    
        public List<Square> GetAvailableMovesQueen(Player player, Board board, Square pieceLocation)
        {
            availableMoves = new List<Square>();
            availableMoves = AddDiagonalMoves(availableMoves, pieceLocation);
            availableMoves = AddLateralMoves(availableMoves, pieceLocation);
            return availableMoves;
        }

        public List<Square> GetAvailableMovesRook(Player player, Board board, Square pieceLocation)
        {
            availableMoves= new List<Square>();

            availableMoves = AddVerticalMoves(availableMoves, pieceLocation);
            availableMoves = AddHorizontalMoves(availableMoves, pieceLocation);
            return availableMoves;
        }

        public List<Square> GetAvailableMovesBishop(Player player, Board board, Square pieceLocation)
        {
            availableMoves = new List<Square> { };

            availableMoves = AddDiagonalMoves(availableMoves, pieceLocation);
            return availableMoves;
        }


        public Square GetPawnSingleVerticalMove(Player player, Board board, Square pawnLocation)
        {
            if (player == Player.White)
            {
                return new Square(pawnLocation.Row - 1, pawnLocation.Col);
            }
            else
            {
                return new Square(pawnLocation.Row + 1, pawnLocation.Col);

            }
        }

        public Square GetPawnDoubleVerticalMove(Player player, Board board, Square pawnLocation)
        {
            if (player == Player.White)
            {
                return new Square(pawnLocation.Row - 2, pawnLocation.Col);
            }
            else
            {
                return new Square(pawnLocation.Row + 2, pawnLocation.Col);
            }

        }

        private List<Square> AddLateralMoves(List<Square> availableMoves, Square pieceLocation)
        {
            availableMoves = AddVerticalMoves(availableMoves, pieceLocation);
            availableMoves = AddHorizontalMoves(availableMoves, pieceLocation);
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
