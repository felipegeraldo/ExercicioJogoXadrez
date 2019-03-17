using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "H";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            SetPosition(ref mat, pos);

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            SetPosition(ref mat, pos);

            pos.SetValues(Position.Line - 2, Position.Column - 1);
            SetPosition(ref mat, pos);

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            SetPosition(ref mat, pos);

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            SetPosition(ref mat, pos);

            pos.SetValues(Position.Line + 1, Position.Column - 2);
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
    }
}
