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
        public static List<Square> AddKnightMoves(List<Square> availableMoves, Square pieceLocation)
        {
            var possibleKnightLocations = GetPossibleKnightMoves(pieceLocation);

            foreach (var square in possibleKnightLocations)
            {
                if (Board.IsValidPosition(square))
                {
                    availableMoves.Add(square);
                }
            }
            return availableMoves;
        }

        public static List<Square> GetPossibleKnightMoves(Square pieceLocation)
        {
            var possibleKnightMoves = new List<Square>();

            possibleKnightMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col + 2));
            possibleKnightMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col + 2));
            possibleKnightMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col - 2));
            possibleKnightMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col - 2));
            possibleKnightMoves.Add(new Square(pieceLocation.Row + 2, pieceLocation.Col + 1));
            possibleKnightMoves.Add(new Square(pieceLocation.Row + 2, pieceLocation.Col - 1));
            possibleKnightMoves.Add(new Square(pieceLocation.Row - 2, pieceLocation.Col + 1));
            possibleKnightMoves.Add(new Square(pieceLocation.Row - 2, pieceLocation.Col - 1));

            return possibleKnightMoves;
        }

        public static List<Square> GetPawnVerticalMoves(Player player, Board board, Square pawnLocation)
        {
            var verticalMoves = new List<Square> {};
            verticalMoves=GetPawnSingleVerticalMove(verticalMoves, player, board, pawnLocation);
            if (!verticalMoves.Any())
            {
                return verticalMoves;
            }

            if (!board.GetPiece(pawnLocation).HasMoved)
            {
                verticalMoves=GetPawnDoubleVerticalMove(verticalMoves,player, board, pawnLocation);
            }
            
            return verticalMoves;
        }

        public static List<Square> GetPawnSingleVerticalMove(List<Square> availableMoves, Player player, Board board, Square pawnLocation)
        {

            if (player == Player.White && board.GetPiece(new Square(pawnLocation.Row-1, pawnLocation.Col))==null)
            {
                availableMoves.Add(new Square(pawnLocation.Row - 1, pawnLocation.Col));
            }
            else if(player == Player.Black && board.GetPiece(new Square(pawnLocation.Row + 1, pawnLocation.Col)) == null)
            {
                availableMoves.Add(new Square(pawnLocation.Row + 1, pawnLocation.Col));
            }

            return availableMoves;
        }

        public static List<Square> GetPawnDoubleVerticalMove(List<Square> availableMoves, Player player, Board board, Square pawnLocation)
        {
            if (player == Player.White && board.GetPiece(new Square(pawnLocation.Row - 2, pawnLocation.Col)) == null)
            {
                availableMoves.Add(new Square(pawnLocation.Row - 2, pawnLocation.Col));
            }
            else if (player == Player.Black && board.GetPiece(new Square(pawnLocation.Row + 2, pawnLocation.Col)) == null)
            {
                availableMoves.Add(new Square(pawnLocation.Row + 2, pawnLocation.Col));
            }

            return availableMoves;
        }

        public static List<Square> AddLateralMoves(List<Square> availableMoves, Square pieceLocation)
        {
            availableMoves = AddVerticalMoves(availableMoves, pieceLocation);
            availableMoves = AddHorizontalMoves(availableMoves, pieceLocation);
            return availableMoves;
        }

        public static List<Square> AddDiagonalMoves(List<Square> availableMoves, Square pieceLocation)
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

        public static List<Square> AddHorizontalMoves(List<Square> availableMoves, Square pieceLocation)
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

        public static List<Square> AddVerticalMoves(List<Square> availableMoves, Square pieceLocation)
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

        public static List<Square> GetKingMoves(Square pieceLocation)
        {
            var possibleMoves = GetPossibleKingMoves(pieceLocation);
            var availableMoves = new List<Square>();
            foreach (var square in possibleMoves) 
            {
                if (Board.IsValidPosition(square))
                {
                    availableMoves.Add((square));
                }
            }

            return availableMoves;
        }

        public static List<Square> GetPossibleKingMoves(Square pieceLocation)
        {
            var possibleMoves = new List<Square>();
            

            possibleMoves.Add(new Square(pieceLocation.Row+1,pieceLocation.Col));
            possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col));
            possibleMoves.Add(new Square(pieceLocation.Row, pieceLocation.Col+1));
            possibleMoves.Add(new Square(pieceLocation.Row, pieceLocation.Col-1));
            possibleMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col+1));
            possibleMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col-1));
            possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col+1));
            possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col-1));

            return possibleMoves;
        }
    }
}
