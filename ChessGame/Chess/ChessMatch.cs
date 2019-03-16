using System;
using System.Collections.Generic;
using System.Text;
using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;
using ChessGame.GameBoard.Exceptions;

namespace ChessGame.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Endgame { get; set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Endgame = false;
            SetPieces();
        }

        public void MakeMove(Position origin, Position destination)
        {
            MovePiece(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.Piece(origin) == null)
            {
                throw new GameBoardException("There is no Piece in the chosen origin position!");
            }
            if (CurrentPlayer != Board.Piece(origin).Color)
            {
                throw new GameBoardException("The chosen origin Piece is not yours!");
            }
            if (!Board.Piece(origin).ExistPossibleMovements())
            {
                throw new GameBoardException("There are no possible moves for this piece!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void MovePiece(Position origin, Position destination)
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
