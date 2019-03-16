using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //up
            pos.SetValues(Position.Line - 1, Position.Column);
            SetPosition(ref mat, pos);

            //ne
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            SetPosition(ref mat, pos);

            //rigth
            pos.SetValues(Position.Line , Position.Column + 1);
            SetPosition(ref mat, pos);

            //se
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            SetPosition(ref mat, pos);

            //down
            pos.SetValues(Position.Line + 1, Position.Column);
            SetPosition(ref mat, pos);

            //so
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            SetPosition(ref mat, pos);

            //left
            pos.SetValues(Position.Line , Position.Column - 1);
            SetPosition(ref mat, pos);

            //no
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            SetPosition(ref mat, pos);

            return mat;
        }

        private void SetPosition(ref bool[,] mat, Position pos)
        {
            if (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
        }

        private bool MayMove(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p == null || p.Color != Color;
        }
    }
}
