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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Endgame = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
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

        public void ValidadeDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).MayMoveTo(destination))
            {
                throw new GameBoardException("Invalid target position!");
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

            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
        }

        public HashSet<Piece> CapturetedPieces(Color c)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == c)
                {
                    aux.Add(piece);
                }
            }

            return aux;
        }

        public HashSet<Piece> PiecesInMatch(Color c)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in Pieces)
            {
                if (piece.Color == c)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturetedPieces(c));
            return aux;
        }

        public void PutNewPiece(char colunm, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(colunm, line).ToPosition());
            Pieces.Add(piece);
        }

        private void SetPieces()
        {
            PutNewPiece('c', 1, new Rook(Color.White, Board));
            PutNewPiece('c', 2, new Rook(Color.White, Board));
            PutNewPiece('d', 2, new Rook(Color.White, Board));
            PutNewPiece('e', 2, new Rook(Color.White, Board));
            PutNewPiece('e', 1, new Rook(Color.White, Board));
            PutNewPiece('d', 1, new King(Color.White, Board));

            PutNewPiece('c', 7, new Rook(Color.Black, Board));
            PutNewPiece('c', 8, new Rook(Color.Black, Board));
            PutNewPiece('d', 7, new Rook(Color.Black, Board));
            PutNewPiece('e', 7, new Rook(Color.Black, Board));
            PutNewPiece('e', 8, new Rook(Color.Black, Board));
            PutNewPiece('d', 8, new King(Color.Black, Board));         
        }
    }
}
