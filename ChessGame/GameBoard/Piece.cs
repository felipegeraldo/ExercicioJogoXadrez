using ChessGame.GameBoard.Enumns;

namespace ChessGame.GameBoard
{
    abstract class Piece
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

        public bool ExistPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] PossibleMovements();
    }
}
