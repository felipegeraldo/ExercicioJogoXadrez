using ChessGame.GameBoard;

namespace ChessGame.Chess
{
    class ChessPosition
    {
        public char Colunm { get; set; }
        public int Line { get; set; }

        public ChessPosition(char colunm, int line)
        {
            Colunm = colunm;
            Line = line;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Colunm - 'a');
        }

        public override string ToString()
        {
            return "" + Colunm + Line;
        }
    }
}
