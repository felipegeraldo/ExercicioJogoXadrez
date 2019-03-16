using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board)
        {
        }

        public override bool[,] PossibleMovements()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "H";
        }
    }
}
