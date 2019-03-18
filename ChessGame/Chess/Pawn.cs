using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class Pawn : Piece
    {
        private ChessMatch chessMatch;

        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            chessMatch = match;
        }

        public override string ToString()
        {
            return "P";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && FreePositoin(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && NumnberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                //#SpecialMovement EnPassant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistOpponent(left) && Board.Piece(left) == chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position rigth = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(rigth) && ExistOpponent(rigth) && Board.Piece(rigth) == chessMatch.VulnerableEnPassant)
                    {
                        mat[rigth.Line - 1, rigth.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && FreePositoin(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && NumnberOfMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ExistOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ExistOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //#SpecialMovement EnPassant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistOpponent(left) && Board.Piece(left) == chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position rigth = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(rigth) && ExistOpponent(rigth) && Board.Piece(rigth) == chessMatch.VulnerableEnPassant)
                    {
                        mat[rigth.Line + 1, rigth.Column] = true;
                    }
                }
            }
            return mat;
        }

        private bool ExistOpponent(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool FreePositoin(Position pos)
        {
            return Board.Piece(pos) == null;
        }
    }
}
