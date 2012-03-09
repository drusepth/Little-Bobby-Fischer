using System;
using System.Collections.Generic;

class Pawn
{
    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Piece pawn = game.GetPieceAt(current);

        int color = game.IsWhite(pawn) ? 1 : -1;

        if (flip_color)
        {
            if (color == 1)
                color = -1;
            else
                color = 1;
        }

        if (pawn.ExpectedRank == (color == 1 ? 2 : 7))
        {
            // Hasn't moved, allow SUPER JUMP (assuming nobody is in the way)
            if (!game.PieceExistsAt(new Coordinate(current.File, current.Rank + 1 * color)) &&
                !game.PieceExistsAt(new Coordinate(current.File, current.Rank + 2 * color)))
            {
                moves.Add(new Coordinate(current.File, current.Rank + 2 * color));
            }
        }

        // Straight move forward 1
        if (!game.PieceExistsAt(new Coordinate(current.File, current.Rank + 1 * color)))
        {
            moves.Add(new Coordinate(current.File, current.Rank + 1 * color));
        }

        // Attack!
        moves.AddRange(GetAttackMoves(current, game, false));

        // En Passant
        moves.AddRange(EnPassant(current, game, false));

        return moves;
    }

    public static List<Coordinate> GetAttackMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Piece piece = game.GetPieceAt(current);
        int color = game.IsWhite(piece) ? 1 : -1;

        // Attack!
        if (!flip_color)
        {
            // Left
            if (game.EnemyPieceExistsAt(new Coordinate(current.File - 1, current.Rank + (1 * color))))
            {
                moves.Add(new Coordinate(current.File - 1, current.Rank + (1 * color)));
            }
            // Right
            if (game.EnemyPieceExistsAt(new Coordinate(current.File + 1, current.Rank + (1 * color))))
            {
                moves.Add(new Coordinate(current.File + 1, current.Rank + (1 * color)));
            }
        }
        else
        {
            if (!game.MyPieceExistsAt(new Coordinate(current.File - 1, current.Rank + (1 * color))))
            {
                moves.Add(new Coordinate(current.File - 1, current.Rank + (1 * color)));
            }
            if (!game.MyPieceExistsAt(new Coordinate(current.File + 1, current.Rank + (1 * color))))
            {
                moves.Add(new Coordinate(current.File + 1, current.Rank + (1 * color)));
            }
        }

        return moves;
    }

    public static List<Coordinate> EnPassant(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Piece pawn = game.GetPieceAt(current);
        int color = game.IsWhite(pawn) ? 1 : -1;

        if (flip_color)
        {
            if (color == 1)
                color = -1;
            else
                color = 1;
        }

        if (pawn.ExpectedRank == (color == 1 ? 4 : 5))
        {
            if (pawn.ExpectedFile - 1 == pawn.ExpectedFile || pawn.ExpectedFile + 1 == pawn.ExpectedFile)
            {
                moves.Add(current);
            }
        }

        return moves;
    }
}
