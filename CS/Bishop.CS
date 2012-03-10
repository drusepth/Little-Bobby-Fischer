using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Bishop
{
    public static double Value
    {
        get { return 3.33; }
    }

    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();

        int file, rank;

        // Up-Right
        for (file = current.File + 1, rank = current.Rank + 1; file < 9 && rank < 9; file++, rank++)
        {
            Coordinate c = new Coordinate(file, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Up-Left
        for (file = current.File - 1, rank = current.Rank + 1; file > 0 && rank < 9; file--, rank++)
        {
            Coordinate c = new Coordinate(file, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Down-Right
        for (file = current.File + 1, rank = current.Rank - 1; file < 9 && rank > 0; file++, rank--)
        {
            Coordinate c = new Coordinate(file, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Down-Left
        for (file = current.File - 1, rank = current.Rank - 1; file > 0 && rank > 0; file--, rank--)
        {
            Coordinate c = new Coordinate(file, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        return moves;
    }
}