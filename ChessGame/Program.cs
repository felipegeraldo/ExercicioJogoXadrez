﻿using System;
using ChessGame.Chess;
using ChessGame.GameBoard;
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
                    try
                    {
                        Console.Clear();
                        Screen.PrintChessMacth(match);

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidadeDestinationPosition(origin, destination);

                        match.MakeMove(origin, destination);
                    }
                    catch (GameBoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.PrintChessMacth(match);
            }
            catch (GameBoardException e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
