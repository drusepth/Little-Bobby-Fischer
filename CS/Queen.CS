using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Queen
{
    public static double Value
    {
        get { return 8.8; }
    }

    public static List<Coordinate> GetMoves(Coordinate current, AI game)
    {
        List<Coordinate> moves = new List<Coordinate>();

        moves.AddRange(Rook.GetMoves(current, game));
        moves.AddRange(Bishop.GetMoves(current, game));

        return moves;
    }
}
