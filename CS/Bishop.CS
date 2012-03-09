using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Bishop
{
    public static List<Coordinate> GetMoves(Coordinate current, AI game, bool flip_color)
    {
        List<Coordinate> moves = new List<Coordinate>();

        int file, rank;

        // Up-Right
        for (file = current.File + 1, rank = current.Rank + 1; file < 9 && rank < 9; file++, rank++)
        {
            Coordinate c = new Coordinate(file, rank);
            if (game.PieceExistsAt(c))
            {
                // If we're looking from the opponent's perspective for pieces to take
                if (flip_color)
                {
                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.EnemyPieceExistsAt(c))
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

        // Up-Left
        for (file = current.File - 1, rank = current.Rank + 1; file > 0 && rank < 9; file--, rank++)
        {
            Coordinate c = new Coordinate(file, rank);
            if (game.PieceExistsAt(c))
            {
                if (flip_color)
                {
                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.EnemyPieceExistsAt(c))
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

        // Down-Right
        for (file = current.File + 1, rank = current.Rank - 1; file < 9 && rank > 0; file++, rank--)
        {
            Coordinate c = new Coordinate(file, rank);
            if (game.PieceExistsAt(c))
            {
                if (flip_color)
                {
                    if (game.EnemyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.MyPieceExistsAt(c))
                    {
                        moves.Add(c);
                        break;
                    }
                }
                else
                {
                    if (game.MyPieceExistsAt(c))
                    {
                        break;
                    }
                    else if (game.EnemyPieceExistsAt(c))
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

        // Down-Left
        for (file = current.File - 1, rank = current.Rank - 1; file > 0 && rank > 0; file--, rank--)
        {
            Coordinate c = new Coordinate(file, rank);
            if (game.PieceExistsAt(c))
            {
                if (game.PieceExistsAt(c))
                {
                    if (flip_color)
                    {
                        if (game.EnemyPieceExistsAt(c))
                        {
                            break;
                        }
                        else if (game.MyPieceExistsAt(c))
                        {
                            moves.Add(c);
                            break;
                        }
                    }
                    else
                    {
                        if (game.MyPieceExistsAt(c))
                        {
                            break;
                        }
                        else if (game.EnemyPieceExistsAt(c))
                        {
                            moves.Add(c);
                            break;
                        }
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