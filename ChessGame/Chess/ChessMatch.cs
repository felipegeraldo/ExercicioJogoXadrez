﻿using System;
using System.Collections.Generic;
using System.Text;
using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            SetPieces();
        }

        public void MovePiece(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoviments();

            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PutPiece(p, destination);
        }

        private void SetPieces()
        {
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}