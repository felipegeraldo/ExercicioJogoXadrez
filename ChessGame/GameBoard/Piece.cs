using ChessGame.GameBoard.Enumns;

namespace ChessGame.GameBoard
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumnberOfMovements { get; protected set; }
        public Board Board { get; set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            NumnberOfMovements = 0;
        }

        public void IncreaseNumberOfMoviments()
        {
            NumnberOfMovements++;
        }
    }
}
