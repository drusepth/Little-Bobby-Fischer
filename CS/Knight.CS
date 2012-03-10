using System;
using System.Collections.Generic;

public class Knight
{
    public static double Value
    {
        get { return 3.2; }
    }

    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Coordinate c;
        // Up-Right
        c = new Coordinate(current.File + 1, current.Rank + 2);
        if (current.Rank < 7 && current.File < 8)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Up-Left
        c = new Coordinate(current.File - 1, current.Rank + 2);
        if (current.Rank < 7 && current.File > 1)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Right-Up
        c = new Coordinate(current.File + 2, current.Rank + 1);
        if (current.File < 7 && current.Rank < 8)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Right-Down
        c = new Coordinate(current.File + 2, current.Rank - 1);
        if (current.File < 7 && current.Rank > 1)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Down-Left
        c = new Coordinate(current.File - 1, current.Rank - 2);
        if (current.Rank > 2 && current.File > 1)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Down-Right
        c = new Coordinate(current.File + 1, current.Rank - 2);
        if (current.Rank > 2 && current.File < 8)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Left-Up
        c = new Coordinate(current.File - 2, current.Rank + 1);
        if (current.File > 2 && current.Rank < 8)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        // Left-Down
        c = new Coordinate(current.File - 2, current.Rank - 1);
        if (current.File > 2 && current.Rank > 1)
        {
            if (!game.MyPieceExistsAt(c))
            {
                moves.Add(c);
            }
        }

        return moves;
    }
}
