using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
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
