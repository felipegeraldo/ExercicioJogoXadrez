using System;
using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;
using ChessGame.Chess;
using ChessGame.GameBoard.Exceptions;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChessPosition pos = new ChessPosition('A', 1);
            //Console.WriteLine(pos);
            try
            {
                Board board = new Board(8, 8);

                board.PutPiece(new Rook(Color.Black, board), new Position(0, 0));
                board.PutPiece(new Rook(Color.Black, board), new Position(1, 3));
                board.PutPiece(new King(Color.Black, board), new Position(2, 4));

                board.PutPiece(new Pawn(Color.White, board), new Position(7, 1));
                board.PutPiece(new Queen(Color.White, board), new Position(7, 2));
                board.PutPiece(new Knight(Color.White, board), new Position(7, 3));

                Screen.PrintBoard(board);

                Console.WriteLine();
            }
            catch (GameBoardException e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
