using System;
using System.Collections.Generic;

class Rook
{
    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();

        // Up
        for (int rank = current.Rank + 1; rank < 9; rank++)
        {
            Coordinate c = new Coordinate(current.File, rank);
            if (game.PieceExistsAt(c))
            {
                // If we're looking for pieces from the opponent's perspective
                if (flip_color)
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                }
                // If we're looking for pieces from our own perspective
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
            }
            else
            {
                moves.Add(c);
            }
        }

        // Right
        for (int file = current.File + 1; file < 9; file++)
        {
            Coordinate c = new Coordinate(file, current.Rank);
            if (game.PieceExistsAt(c))
            {
                // If we're looking for pieces from the opponent's perspective
                if (flip_color)
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                }
                // If we're looking for pieces from our own perspective
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
            }
            else
            {
                moves.Add(c);
            }
        }

        // Down
        for (int rank = current.Rank - 1; rank > 0; rank--)
        {
            Coordinate c = new Coordinate(current.File, rank);
            if (game.PieceExistsAt(c))
            {
                // If we're looking for pieces from the opponent's perspective
                if (flip_color)
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                }
                // If we're looking for pieces from our own perspective
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
            }
            else
            {
                moves.Add(c);
            }
        }

        // Left
        for (int file = current.File - 1; file > 0; file--)
        {
            Coordinate c = new Coordinate(file, current.Rank);
            if (game.PieceExistsAt(c))
            {
                // If we're looking for pieces from the opponent's perspective
                if (flip_color)
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                }
                // If we're looking for pieces from our own perspective
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }

                    if (game.EnemyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
            }
            else
            {
                moves.Add(c);
            }
        }

        return moves;
    }
}