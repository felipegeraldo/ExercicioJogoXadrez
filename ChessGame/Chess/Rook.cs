using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class Rook : Piece
    {
        public Rook(Color color, Board board) : base(color, board)
        {
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //up
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            //down
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            //rigth
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }

        private bool MayMove(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p == null || p.Color != Color;
        }



        public override string ToString()
        {
            return "R";
        }
    }
}
