﻿using System.Collections;
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
            availableMoves = GetAvailableMovesPawn(Player, board, pawnLocation);
            
            return availableMoves;
        }

        public List<Square> GetAvailableMovesPawn(Player player, Board board, Square pieceLocation)
        {
            var availableMoves = new List<Square>();
            availableMoves.Add(MoveFinder.GetPawnSingleVerticalMove(player, board, pieceLocation));
            if (!board.GetPiece(pieceLocation).HasMoved)
            {
                availableMoves.Add(MoveFinder.GetPawnDoubleVerticalMove(player, board, pieceLocation));
            }
            return availableMoves;
        }



    }
}