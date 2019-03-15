using System;
using System.Collections.Generic;
using System.Text;
using ChessGame.Chess;
using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines ; i++)
            {
                Console.Write(8-i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char colunm = s[0];
            int line = int.Parse(s[1] + "");

            return new ChessPosition(colunm, line);
        }

        private static void PrintPiece(Piece p)
        {
            if (p.Color == Color.White) 
            {
                Console.Write(p);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(p);
                Console.ForegroundColor = aux;
            }
        }
    }
}
