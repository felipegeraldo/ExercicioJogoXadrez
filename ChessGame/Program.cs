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
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Endgame)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.WriteLine();
                    Console.WriteLine("Turn: " + match.Turn);
                    Console.WriteLine("Waiting move: " + match.CurrentPlayer);
               
                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    match.MakeMove(origin, destination);
                }

                Console.WriteLine();
            }
            catch (GameBoardException e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
