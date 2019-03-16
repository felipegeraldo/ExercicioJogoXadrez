using System;
using System.Collections.Generic;
using ChessGame.Chess;
using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame
{
    class Screen
    {
        public static void PrintChessMacth(ChessMatch chessMatch)
        {
            Screen.PrintBoard(chessMatch.Board);
            Console.WriteLine();
            PrintCapturetedPieces(chessMatch);
            Console.WriteLine();
            Console.WriteLine("Turn: " + chessMatch.Turn);
            Console.WriteLine("Waiting move: " + chessMatch.CurrentPlayer);
        }

        public static void PrintCapturetedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Captureted Pieces: ");
            Console.Write("Whites: ");
            PrintHashset(chessMatch.CapturetedPieces(Color.White));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Blacks: ");
            PrintHashset(chessMatch.CapturetedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintHashset(HashSet<Piece> hashSet)
        {
            Console.Write("[");
            foreach (Piece p in hashSet)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor orinalBackgroud = Console.BackgroundColor;
            ConsoleColor backgroudChanged = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = backgroudChanged;
                    }
                    else
                    {
                        Console.BackgroundColor = orinalBackgroud;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = orinalBackgroud;
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
            if (p == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
