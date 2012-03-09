using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class King
{
    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Coordinate c;

        // N
        c = new Coordinate(current.File, current.Rank + 1);
        if (current.Rank < 8)
        {
            // Opponent's perspective
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            // Our perspective
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // NE
        c = new Coordinate(current.File + 1, current.Rank + 1);
        if (current.Rank < 8 && current.File < 8)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // E
        c = new Coordinate(current.File + 1, current.Rank);
        if (current.File < 8)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // SE
        c = new Coordinate(current.File + 1, current.Rank - 1);
        if (current.File < 8 && current.Rank > 1)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // S
        c = new Coordinate(current.File, current.Rank - 1);
        if (current.Rank > 1)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // SW
        c = new Coordinate(current.File - 1, current.Rank - 1);
        if (current.Rank > 1 && current.File > 1)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // W
        c = new Coordinate(current.File - 1, current.Rank);
        if (current.File > 1)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        // NW
        c = new Coordinate(current.File - 1, current.Rank + 1);
        if (current.File > 1 && current.Rank < 8)
        {
            if (flip_color)
            {
                if (!game.EnemyPieceExistsAt(c))
                    moves.Add(c);
            }
            else
            {
                if (!game.MyPieceExistsAt(c))
                    moves.Add(c);
            }
        }

        return moves;
    }
}
