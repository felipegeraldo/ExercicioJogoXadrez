using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.GameBoard.Exceptions
{
    class GameBoardException : Exception
    {
        public GameBoardException(string msg) : base(msg)
        {

        }
    }
}
