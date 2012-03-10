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

    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();

        moves.AddRange(Rook.GetMoves(current, game, flip_color));
        moves.AddRange(Bishop.GetMoves(current, game, flip_color));

        return moves;
    }
}
