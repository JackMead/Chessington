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

            board.gameEnded.Should().Be(Board.GameOver.Stalemate);
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

            board.gameEnded.Should().Be(Board.GameOver.BlackWin);
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

            board.gameEnded.Should().Be(Board.GameOver.WhiteWin);

        }

    }
}
