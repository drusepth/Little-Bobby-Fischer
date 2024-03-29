﻿using System;
using System.Collections.Generic;

class Pawn
{
    // 
    public static double Value
    {
        get { return 1.0; }
    }

    public static List<Coordinate> GetMoves(Coordinate current, AI game)
    {
        // The pawn, himself
        Piece pawn = game.GetPieceAt(current);

        // List of moves this pawn can make
        List<Coordinate> moves = new List<Coordinate>();
        
        // Rank multiplier dependant on piece color
        int color = game.IsWhite(pawn) ? 1 : -1;

        // If a pawn is in its corresponding start position (hasn't moved), then we want to handle
        // it being able to move forward two spaces in addition to just one.
        if (pawn.ExpectedRank == (color == 1 ? 2 : 7))
        {
            // There must be no pieces in the way to move forward two
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
        moves.AddRange(GetAttackMoves(current, game));

        // En Passant
        moves.AddRange(EnPassant(current, game));

        return moves;
    }

    public static List<Coordinate> GetAttackMoves(Coordinate current, AI game)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Piece piece = game.GetPieceAt(current);
        int color = game.IsWhite(piece) ? 1 : -1;

        // Attack!
        if (game.EnemyPieceExistsAt(new Coordinate(current.File - 1, current.Rank + (1 * color))))
        {
            moves.Add(new Coordinate(current.File - 1, current.Rank + (1 * color)));
        }
        if (game.EnemyPieceExistsAt(new Coordinate(current.File + 1, current.Rank + (1 * color))))
        {
            moves.Add(new Coordinate(current.File + 1, current.Rank + (1 * color)));
        }

        return moves;
    }

    public static List<Coordinate> EnPassant(Coordinate current, AI game)
    {
        List<Coordinate> moves = new List<Coordinate>();
        Piece pawn = game.GetPieceAt(current);
        int color = game.IsWhite(pawn) ? 1 : -1;

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
