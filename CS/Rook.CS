using System;
using System.Collections.Generic;

class Rook
{
    // 
    public static double Value
    {
        get { return 5.1; }
    }

    // Returns all moves a rook can make from a given coordinate
    public static List<Coordinate> GetMoves(Coordinate current, AI game)
    {
        List<Coordinate> moves = new List<Coordinate>();

        // Up
        for (int rank = current.Rank + 1; rank < 9; rank++)
        {
            Coordinate c = new Coordinate(current.File, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Right
        for (int file = current.File + 1; file < 9; file++)
        {
            Coordinate c = new Coordinate(file, current.Rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Down
        for (int rank = current.Rank - 1; rank > 0; rank--)
        {
            Coordinate c = new Coordinate(current.File, rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        // Left
        for (int file = current.File - 1; file > 0; file--)
        {
            Coordinate c = new Coordinate(file, current.Rank);
            moves.Add(c);
            if (game.PieceExistsAt(c))
            {
                break;
            }
        }

        return moves;
    }
}