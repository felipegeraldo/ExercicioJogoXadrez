using ChessGame.GameBoard;
using ChessGame.GameBoard.Enumns;

namespace ChessGame.Chess
{
    class King : Piece
    {
        private ChessMatch Match;
        
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
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

            //#SpecialMovement Castle
            if (NumnberOfMovements == 0 && !Match.Check)
            {
                //#SpecialMovement SmallCastle
                Position posRookSmallCastle = new Position(Position.Line, Position.Column + 3);
                if (TestRookCastle(posRookSmallCastle))
                {
                    Position positionKingRigth = new Position(Position.Line, Position.Column + 1);
                    Position positionKingRigth2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(positionKingRigth) == null && Board.Piece(positionKingRigth2) == null)
                    {
                        mat[positionKingRigth2.Line, positionKingRigth2.Column] = true;
                    }
                }

                //#SpecialMovement BigCastle
                Position posRookBigCastle = new Position(Position.Line, Position.Column -4);
                if (TestRookCastle(posRookBigCastle))
                {
                    Position positionKingRigth = new Position(Position.Line, Position.Column - 1);
                    Position positionKingRigth2 = new Position(Position.Line, Position.Column - 2);
                    Position positionKingRigth3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(positionKingRigth) == null && Board.Piece(positionKingRigth2) == null && Board.Piece(positionKingRigth3) == null)
                    {
                        mat[positionKingRigth2.Line, positionKingRigth2.Column] = true;
                    }
                }
            }
            return mat;
        }

        private void SetPosition(ref bool[,] mat, Position pos)
        {
            if (Board.ValidPosition(pos) && MayMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
        }

        private bool TestRookCastle(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.NumnberOfMovements == 0;
        }
    }
}
