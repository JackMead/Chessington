using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests
{
    [TestFixture]
    public class EndGameTests
    {
        [Test]
        public void StaleMateShouldEndGame()
        {
            var board = new Board();
            var blackPawn = new Pawn(Player.Black);
            var whitePawn = new Pawn(Player.White);

            board.AddPiece(Square.At(4, 5), whitePawn);
            board.AddPiece(Square.At(2, 5), blackPawn);

            board.MovePiece(Square.At(4, 5), Square.At(3, 5));

            board.gameState.Should().Be(Board.GameState.Stalemate);
        }

        [Test]
        public void White_CanBeCheckMated()
        {
            var board = new Board(Player.Black);
            var whiteKing = new King(Player.White);
            var blackKing = new King(Player.Black);
            var blackQueen = new Queen(Player.Black);
            var anotherBlackQueen = new Queen(Player.Black);

            board.AddPiece(Square.At(0, 0), whiteKing);
            board.AddPiece(Square.At(7, 0), blackKing);
            board.AddPiece(Square.At(4, 0), blackQueen);
            board.AddPiece(Square.At(4, 1), anotherBlackQueen);

            board.MovePiece(Square.At(4, 1), Square.At(5, 1));

            board.gameState.Should().Be(Board.GameState.BlackWin);
        }

        [Test]
        public void Black_CanBeCheckMated()
        {
            var board = new Board();
            var blackKing = new King(Player.Black);
            var whiteKing = new King(Player.White);
            var whiteQueen = new Queen(Player.White);
            var secondWhiteQueen = new Queen(Player.White);

            board.AddPiece(Square.At(7, 1), whiteKing);
            board.AddPiece(Square.At(0, 0), blackKing);
            board.AddPiece(Square.At(4, 0), whiteQueen);
            board.AddPiece(Square.At(4, 1), secondWhiteQueen);

            board.MovePiece(Square.At(4, 1), Square.At(5, 1));

            board.gameState.Should().Be(Board.GameState.WhiteWin);

        }

        [Test]
        public void Black_CanBeInCheck()
        {
            var board = new Board();
            var whiteRook = new Rook(Player.White);
            var blackKing = new King(Player.Black);

            board.AddPiece(Square.At(0, 0), whiteRook);
            board.AddPiece(Square.At(7, 0), blackKing);

            foreach (var el in whiteRook.GetAvailableMoves(board))
            {
                Console.WriteLine(el.ToString());
            }
            board.IsInCheck(Player.Black).Should().BeTrue();
        }

        [Test]
        public void White_CanBeInCheck()
        {
            var board = new Board();
            var blackKnight = new Knight(Player.Black);
            var whiteKing = new King(Player.White);

            board.AddPiece(Square.At(0, 0), whiteKing);
            board.AddPiece(Square.At(1, 2), blackKnight);

            board.IsInCheck(Player.White).Should().BeTrue();
        }

        [Test]
        public void White_KingCanBeFound()
        {
            var board = new Board();
            var whiteKing = new King(Player.White);

            board.AddPiece(Square.At(4, 6), whiteKing);

            board.GetKingLocation(Player.White).Should().Be(new Square(4,6));
        }

        [Test]
        public void Black_KingCanBeFound()
        {
            var board = new Board(Player.Black);
            var blackKing = new King(Player.Black);

            board.AddPiece(Square.At(2, 1), blackKing);

            board.GetKingLocation(Player.Black).Should().Be(new Square(2, 1));
        }

    }
}
