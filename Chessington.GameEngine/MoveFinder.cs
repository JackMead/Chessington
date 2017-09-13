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
        public enum Directions
        {
            Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight
        };

        public enum PawnMove
        {
            Single, Double
        }

        public static List<Square> GetKnightMoves(List<Square> availableMoves, Board board, Square pieceLocation)
        {
            var possibleKnightLocations = GetPossibleKnightMoves(pieceLocation);

            foreach (var square in possibleKnightLocations)
            {
                if (Board.IsValidPosition(square))
                {
                    if (board.GetPiece(square) != null &&
                        board.GetPiece(square).Player == board.GetPiece(pieceLocation).Player)
                    {
                        continue;
                    }
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
        
        public static List<Square> GetValidVerticalPawnMoves(List<Square> availableMoves, Player player, Board board, Square pawnLocation, PawnMove singleOrDouble)
        {
            var jump = 1;
            if (singleOrDouble == PawnMove.Double)
            {
                jump = 2;
            }

            if (player == Player.White)
            {
                var newLocation = new Square(pawnLocation.Row - jump, pawnLocation.Col);
                if (Board.IsValidPosition(newLocation) && board.GetPiece(newLocation) == null)
                {
                    availableMoves.Add(newLocation);
                }
            }
            else if (player == Player.Black)
            {
                var newLocation = new Square(pawnLocation.Row + jump, pawnLocation.Col);

                if (Board.IsValidPosition(newLocation) && board.GetPiece(newLocation) == null)
                {
                    availableMoves.Add(newLocation);
                }
            }
            return availableMoves;
        }
        
    public static List<Square> AddLateralMoves(List<Square> availableMoves, Board board, Square pieceLocation)
    {
        availableMoves = AddVerticalMoves(availableMoves, board, pieceLocation);
        availableMoves = AddHorizontalMoves(availableMoves, board, pieceLocation);
        return availableMoves;
    }

    public static List<Square> AddDiagonalMoves(List<Square> availableMoves, Board board, Square pieceLocation)
    {
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.UpRight);
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.UpLeft);
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.DownRight);
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.DownLeft);

        return availableMoves;
    }

    public static List<Square> AddHorizontalMoves(List<Square> availableMoves, Board board, Square pieceLocation)
    {
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.Left);
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.Right);

        return availableMoves;
    }

    public static List<Square> AddVerticalMoves(List<Square> availableMoves, Board board, Square pieceLocation)
    {
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.Up);
        availableMoves = AddMovesUntilCollision(availableMoves, board, pieceLocation, Directions.Down);

        return availableMoves;
    }

    public static List<Square> AddMovesUntilCollision(List<Square> availableMoves, Board board,
        Square pieceLocation, Directions direction)
    {
        for (int i = 1; i < GameSettings.BoardSize; i++)
        {
            var newPosition = GetNewPositionByDirectionAndDistance(pieceLocation, direction, i);

            if (!Board.IsValidPosition(newPosition))
            {
                break;
            }

            if (board.GetPiece(newPosition) == null)
            {
                availableMoves.Add(newPosition);
            }
            else if (board.GetPiece(newPosition).Player != board.GetPiece(pieceLocation).Player)
            {
                availableMoves.Add(newPosition);
                break;
            }
            else
            {
                break;
            }
        }
        return availableMoves;
    }

    private static Square GetNewPositionByDirectionAndDistance(Square pieceLocation, Directions direction, int i)
    {
        switch (direction)
        {
            case Directions.Up:
                return new Square(pieceLocation.Row - i, pieceLocation.Col);

            case Directions.Down:
                return new Square(pieceLocation.Row + i, pieceLocation.Col);

            case Directions.Left:
                return new Square(pieceLocation.Row, pieceLocation.Col - i);

            case Directions.Right:
                return new Square(pieceLocation.Row, pieceLocation.Col + i);

            case Directions.UpRight:
                return new Square(pieceLocation.Row - i, pieceLocation.Col + i);

            case Directions.DownRight:
                return new Square(pieceLocation.Row + i, pieceLocation.Col + i);

            case Directions.UpLeft:
                return new Square(pieceLocation.Row - i, pieceLocation.Col - i);

            case Directions.DownLeft:
                return new Square(pieceLocation.Row + i, pieceLocation.Col - i);

        }
        return new Square();
    }

    public static List<Square> GetKingMoves(Square pieceLocation, Board board)
    {

        var possibleKingLocations = GetPossibleKingMoves(pieceLocation);
        var availableMoves = new List<Square>();

        foreach (var square in possibleKingLocations)
        {
            if (Board.IsValidPosition(square))
            {
                if (board.GetPiece(square) != null &&
                    board.GetPiece(square).Player == board.GetPiece(pieceLocation).Player)
                {
                    continue;
                }
                availableMoves.Add(square);
            }
        }
        return availableMoves;
        }

    public static List<Square> GetPossibleKingMoves(Square pieceLocation)
    {
        var possibleMoves = new List<Square>();


        possibleMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col));
        possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col));
        possibleMoves.Add(new Square(pieceLocation.Row, pieceLocation.Col + 1));
        possibleMoves.Add(new Square(pieceLocation.Row, pieceLocation.Col - 1));
        possibleMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col + 1));
        possibleMoves.Add(new Square(pieceLocation.Row + 1, pieceLocation.Col - 1));
        possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col + 1));
        possibleMoves.Add(new Square(pieceLocation.Row - 1, pieceLocation.Col - 1));

        return possibleMoves;
    }
}
}
