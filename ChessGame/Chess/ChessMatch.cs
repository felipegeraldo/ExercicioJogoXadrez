using System.Collections.Generic;
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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Endgame = false;
            Check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            SetPieces();
            VulnerableEnPassant = null;
        }

        public void MakeMove(Position origin, Position destination)
        {
            Piece p = MovePiece(origin, destination);

            if (IsCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, p);
                throw new GameBoardException("You can not put yourself in check!");
            }
            if (IsCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (TestCheckmate(Opponent(CurrentPlayer)))
            {
                Endgame = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            Piece pieceEnPassant = Board.Piece(destination);
            //#SpecialMovement EnPassant
            if (pieceEnPassant is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = pieceEnPassant;
            }
            else
            {
                VulnerableEnPassant = null;
            }

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
            if (!Board.Piece(origin).PossibleMovement(destination))
            {
                throw new GameBoardException("Invalid target position!");
            }
        }

        public HashSet<Piece> CapturetedPieces(Color c)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in capturedPieces)
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

            foreach (Piece piece in pieces)
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
            pieces.Add(piece);
        }

        public bool IsCheck(Color c)
        {
            Piece K = King(c);
            if (K == null)
            {
                throw new GameBoardException("There is no " + c + " king on the macth!");
            }
            foreach (Piece p in PiecesInMatch(Opponent(c)))
            {
                bool[,] mat = p.PossibleMovements();
                if (mat[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckmate(Color c)
        {
            if (!IsCheck(c))
            {
                return false;
            }
            foreach (Piece p in PiecesInMatch(c))
            {
                bool[,] mat = p.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturetedPiece = MovePiece(origin, destination);
                            bool testCheck = IsCheck(c);
                            UndoMove(origin, destination, capturetedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Color Opponent(Color c)
        {
            if (c == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color c)
        {
            foreach (Piece p in PiecesInMatch(c))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
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

        private Piece MovePiece(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoviments();

            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PutPiece(p, destination);

            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }

            //#SpecialMovement SmallCastle
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position orignRook = new Position(origin.Line, origin.Column + 3);
                Position destinationRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(orignRook);
                rook.IncreaseNumberOfMoviments();
                Board.PutPiece(rook, destinationRook);
            }

            //#SpecialMovement BigCastle
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position orignRook = new Position(origin.Line, origin.Column - 4);
                Position destinationRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(orignRook);
                rook.IncreaseNumberOfMoviments();
                Board.PutPiece(rook, destinationRook);
            }

            //#SpecialMovement EnPassant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position pawnPosition;
                    if (p.Color == Color.White)
                    {
                        pawnPosition = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destination.Line - 1, destination.Column);
                    }
                    capturedPiece = Board.RemovePiece(pawnPosition);
                    capturedPieces.Add(capturedPiece);
                }
            }



            return capturedPiece;
        }

        private void UndoMove(Position origin, Position destination, Piece capturetedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecrementNumberOfMoviments();

            if (capturetedPiece != null)
            {
                Board.PutPiece(capturetedPiece, destination);
                capturedPieces.Remove(capturetedPiece);
            }
            Board.PutPiece(p, origin);

            //#SpecialMovement SmallCastle
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position orignRook = new Position(origin.Line, origin.Column + 3);
                Position destinationRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(destination);
                rook.DecrementNumberOfMoviments();
                Board.PutPiece(rook, orignRook);
            }

            //#SpecialMovement BigCastle
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position orignRook = new Position(origin.Line, origin.Column - 4);
                Position destinationRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(destination);
                rook.DecrementNumberOfMoviments();
                Board.PutPiece(rook, orignRook);
            }

            //#SpecialMovement EnPassant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturetedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPosition;
                    if (p.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destination.Column);
                    }
                    Board.PutPiece(pawn, pawnPosition);
                }
            }
        }

        private void SetPieces()
        {
            PutNewPiece('a', 1, new Rook(Color.White, Board));
            PutNewPiece('b', 1, new Knight(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board, this));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('g', 1, new Knight(Color.White, Board));
            PutNewPiece('h', 1, new Rook(Color.White, Board));
            PutNewPiece('a', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('b', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('c', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('d', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('e', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('f', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('g', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('h', 2, new Pawn(Color.White, Board, this));

            PutNewPiece('a', 8, new Rook(Color.Black, Board));
            PutNewPiece('b', 8, new Knight(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board, this));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('g', 8, new Knight(Color.Black, Board));
            PutNewPiece('h', 8, new Rook(Color.Black, Board));
            PutNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    }
}
