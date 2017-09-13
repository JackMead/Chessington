using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;
using System.Windows.Forms;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }
        public GameOver gameEnded = GameOver.StillPlaying;

        public enum GameOver
        {
            StillPlaying,
            Stalemate,
            WhiteWin,
            BlackWin
        }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize];
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public static bool IsValidPosition(Square position)
        {
            if (position.Row < 0 || position.Row >= GameSettings.BoardSize || position.Col < 0 ||
                position.Col >= GameSettings.BoardSize)
            {
                return false;
            }
            return true;
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }

        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }

        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            //Update the piece so we know it has moved - relevant for castling & pawns
            board[to.Row, to.Col].HasMoved = true;

            //Check to see if pawn needs to be promoted
            movingPiece = PromoteIfApplicable(movingPiece, to.Row, to.Col);
            board[to.Row, to.Col] = movingPiece;


            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;

            //Check if stalemate reached
            if (IsStaleMate(CurrentPlayer))
            {
                gameEnded = GameOver.Stalemate;
                GameOverMessage(GameOver.Stalemate);
            }

            if (IsInCheckMate(CurrentPlayer))
            {
                
                if (CurrentPlayer == Player.White)
                {
                    GameOverMessage(GameOver.BlackWin);
                    gameEnded = GameOver.BlackWin;
                }
                else
                {
                    GameOverMessage(GameOver.WhiteWin);
                    gameEnded = GameOver.WhiteWin;
                }
            }


            OnCurrentPlayerChanged(CurrentPlayer);
        }

        private bool IsInCheck(Player player)
        {
            Square kingSquare = GetKingLocation(player);

            foreach (var piece in board)
            {
                if (piece != null && piece.Player != player)
                {
                    if (piece.GetAvailableMoves(this).Contains(kingSquare))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Square GetKingLocation(Player player)
        {
            for (int row = 0; row < GameSettings.BoardSize; row++)
            {
                for (int col = 0; col < GameSettings.BoardSize; col++)
                {
                    if (board[row, col] is King && board[row, col].Player == CurrentPlayer)
                    {
                        return new Square(row, col);
                    }
                }
            }
            return new Square();
        }




        private bool IsInCheckMate(Player currentPlayer)
        {
            if (!IsInCheck(currentPlayer))
            {
                return false;
            }

            foreach (var piece in board)
            {
                if (piece != null && piece.Player == currentPlayer)
                {
                    //Try each of the pieces moves and check if that can stop check.
                    foreach (var move in piece.GetAvailableMoves(this))
                    {
                        Piece[,] copyOfBoard = (Piece[,]) board.Clone();
                        Board copyBoard = new Board(currentPlayer, copyOfBoard);

                        Square pieceSquare = copyBoard.FindPiece(piece);

                        copyBoard.MovePiece(pieceSquare,move);
                        if (!IsInCheck(currentPlayer))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void GameOverMessage(GameOver gameOverType)
        {
            if (gameOverType == GameOver.BlackWin)
            {
                MessageBox.Show("Black wins! Click below to restart");

            }
            else if (gameOverType == GameOver.WhiteWin)
            {
                MessageBox.Show("White wins! Click below to restart");
            }
            else if (gameOverType == GameOver.Stalemate)
            {
                MessageBox.Show("Stalemate, it's a draw! Click below to restart");
            }
        }

        private bool IsStaleMate(Player currentPlayer)
        {
            foreach (var piece in board)
            {
                if (piece != null && piece.Player == currentPlayer && piece.GetAvailableMoves(this).Any())
                {
                    return false;
                }
            }
            return true;
        }

        private Piece PromoteIfApplicable(Piece movingPiece, int row, int col)
        {
            if ((movingPiece.Player == Player.White && row == 0 && movingPiece is Pawn)
                || (movingPiece.Player == Player.Black && row == GameSettings.BoardSize - 1 && movingPiece is Pawn))
            {
                movingPiece = new Queen(movingPiece.Player);
            }

            return movingPiece;
        }

        public delegate void PieceCapturedEventHandler(Piece piece);

        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
