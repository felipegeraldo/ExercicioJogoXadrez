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
                ChessMatch match = new ChessMatch();

                Screen.PrintBoard(match.Board);

                Console.WriteLine();
            }
            catch (GameBoardException e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
