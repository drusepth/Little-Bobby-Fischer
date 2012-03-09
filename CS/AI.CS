using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

///The class implementing gameplay logic.
public class AI : BaseAI
{
  public override string username()
  {
    return "eggs";
  }
  public override string password()
  {
    return "thm88253";
  }

  public Hashtable piece_by_coordinate;

  public void Debug(string s)
  {
    //Console.WriteLine(s);
  }

  //This function is called each time it is your turn
  //Return true to end your turn, return false to ask the server for updated information
  public override bool run()
  {
      for (int p = 0; p < pieces.Length; p++)
      {
          pieces[p].ExpectedFile = pieces[p].getFile();
          pieces[p].ExpectedRank = pieces[p].getRank();
      }

      List<MoveReference> valid_moves = new List<MoveReference>();
      for (int p = 0; p < pieces.Length; p++)
      {
          // Only find moves for my pieces
          if (pieces[p].getOwner() == playerID())
          {
              // Get all moves for this piece
              List<Coordinate> moves_for_piece = GetMovesFor((char)pieces[p].getType(), new Coordinate(pieces[p].ExpectedFile, pieces[p].ExpectedRank), this);
              for (int m = 0; m < moves_for_piece.Count; m++)
              {
                  valid_moves.Add(new MoveReference(pieces[p], moves_for_piece[m]));
              }
          }
      }

      // Prune to avoid putting ourselves in check (and staying in check)
      PruneByCheckAvoidance(valid_moves);

      // Pick a random move and make that move
      if (valid_moves.Count > 0)
      {
          MoveReference action = valid_moves[generator.Next(valid_moves.Count - 1)];

          Console.WriteLine("Random move chosen: src ({0}, {1}), dest ({2}, {3})", 
              action.Piece.getFile(), action.Piece.getRank(), action.To.File, action.To.Rank);
          Console.WriteLine("Piece chosen: {0}", (char)action.Piece.getType());
          PrintValidMoves(valid_moves, action);

          action.Piece.move(action.To.File, action.To.Rank, (int)'Q');
      }

      // Print game info
      PrintBoard();
      //GameInfo();

    return true;
  }

  private void PruneByCheckAvoidance(List<MoveReference> move_list)
  {
      for (int i = 0; i < move_list.Count; i++)
      {
          MoveReference mr = move_list[i];

          int rank = mr.Piece.ExpectedRank;
          int file = mr.Piece.ExpectedFile;

          // Remove any piece that might be there
          Piece enemy_piece = GetPieceAt(mr.To);
          int erank = 0;
          int efile = 0;

          // If a piece is being captured
          if (enemy_piece != null)
          {
              erank = enemy_piece.ExpectedRank;
              efile = enemy_piece.ExpectedFile;

              // Move enemy piece off the board
              enemy_piece.ExpectedFile = -1000;
              enemy_piece.ExpectedRank = -1000;
          }

          mr.Piece.ExpectedFile = mr.To.File;
          mr.Piece.ExpectedRank = mr.To.Rank;

          bool in_check = false;
          if (IsKingInCheck())
          {
              in_check = true;
              Console.WriteLine("Pruned {0} ({1}, {2}) to ({3}, {4})",
                  (char)mr.Piece.getType(), file, rank, mr.To.File, mr.To.Rank);
          }

          // Revert state to original
          mr.Piece.ExpectedFile = file;
          mr.Piece.ExpectedRank = rank;
          if (EnemyPieceExistsAt(mr.To))
          {
              GetPieceAt(mr.To).ExpectedFile = efile;
              GetPieceAt(mr.To).ExpectedRank = erank;
          }

          if (in_check)
          {
              // Prune move, doesn't get me out of check
              move_list.RemoveAt(i);
              // Hop back a move
              i--;
          }
      }
  }

  private bool IsKingInCheck()
  {
      Piece king = FindMyKing();

      // Look for rook baddies
      foreach (Coordinate threat in Rook.GetMoves(new Coordinate(king.ExpectedFile, king.ExpectedRank), this, false))
      {
          Piece bad_dude = GetPieceAt(threat);
          if (bad_dude != null && ((char)bad_dude.getType() == 'R' || (char)bad_dude.getType() == 'Q'))
          {
              Console.WriteLine("Uh oh, {0} at ({1}, {2})", (char)bad_dude.getType(), threat.File, threat.Rank);
              return true;
          }
      }

      // Look for bishop baddies
      foreach (Coordinate threat in Bishop.GetMoves(new Coordinate(king.ExpectedFile, king.ExpectedRank), this, false))
      {
          Piece bad_dude = GetPieceAt(threat);
          if (bad_dude != null && ((char)bad_dude.getType() == 'B' || (char)bad_dude.getType() == 'Q'))
          {
              Console.WriteLine("Uh oh, {0} at ({1}, {2})", (char)bad_dude.getType(), threat.File, threat.Rank);
              return true;
          }
      }

      // Look for knight baddies
      foreach (Coordinate threat in Knight.GetMoves(new Coordinate(king.ExpectedFile, king.ExpectedRank), this, false))
      {
          Piece bad_dude = GetPieceAt(threat);
          if (bad_dude != null && (char)bad_dude.getType() == 'N')
          {
              Console.WriteLine("Uh oh, {0} at ({1}, {2})", (char)bad_dude.getType(), threat.File, threat.Rank);
              return true;
          }
      }

      // Look for pawn baddies, they're the worst
      foreach (Coordinate threat in Pawn.GetAttackMoves(new Coordinate(king.ExpectedFile, king.ExpectedRank), this, true))
      {
          Piece bad_dude = GetPieceAt(threat);
          if (GetPieceAt(threat) != null && (char)GetPieceAt(threat).getType() == 'P')
          {
              Console.WriteLine("Uh oh, {0} at ({1}, {2})", (char)bad_dude.getType(), threat.File, threat.Rank);
              return true;
          }
      }

      // Look for other kings, because we're retarded
      foreach (Coordinate threat in King.GetMoves(new Coordinate(king.ExpectedFile, king.ExpectedRank), this, false))
      {
          Piece bad_dude = GetPieceAt(threat);
          if (bad_dude != null && bad_dude.getOwner() != playerID() && (char)bad_dude.getType() == 'K')
          {
              Console.WriteLine("Uh oh, {0} at ({1}, {2})", (char)bad_dude.getType(), threat.File, threat.Rank);
              return true;
          }
      }

      return false;
  }

  public bool IsWhite(Piece p)
  {
      return p.getOwner() == 0;
  }

  public Piece FindMyKing()
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].getOwner() == playerID())
          {
              if ((char)pieces[i].getType() == 'K')
              {
                  return pieces[i];
              }
          }
      }

      return null;
  }

  public bool MyPieceExistsAt(Coordinate c)
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].ExpectedRank == c.Rank && pieces[i].ExpectedFile == c.File)
          {
              if (pieces[i].getOwner() == playerID())
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
      }
      return false;
  }

  public bool IsMyPiece(Piece p)
  {
      return p.getOwner() == playerID();
  }

  public bool EnemyPieceExistsAt(Coordinate c)
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].ExpectedRank == c.Rank && pieces[i].ExpectedFile == c.File)
          {
              if (pieces[i].getOwner() != playerID())
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
      }
      return false;
  }

  public Piece GetPieceAt(Coordinate c)
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].ExpectedRank == c.Rank && pieces[i].ExpectedFile == c.File)
          {
              return pieces[i];
          }
      }
      return null;
  }

  public bool PieceExistsAt(Coordinate c)
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].ExpectedRank == c.Rank && pieces[i].ExpectedFile == c.File)
          {
              return true;
          }
      }
      return false;
  }

  private void PrintValidMoves(List<MoveReference> move_list, MoveReference chosen)
  {
      Console.Write("Other moves: ");
      for (int i = 0; i < move_list.Count; i++)
      {
          MoveReference mv = move_list[i];
          if (mv.Piece.getId() == chosen.Piece.getId())
          {
              Console.Write("({1}, {2})", (char)mv.Piece.getType(), mv.To.File, mv.To.Rank);
          }
      }
      Console.WriteLine();
  }

  public void GameInfo()
  {
      // Looks through information about the players
      for (int p = 0; p < players.Length; p++)
      {
          Console.Write(players[p].getPlayerName());
          // if playerID is 0, you're white, if its 1, you're black
          if (players[p].getId() == playerID())
          {
              Console.Write(" (ME)");
          }
          Console.WriteLine(" time remaining: " + players[p].getTime());
      }
      // if there has been a move, print the most recent move
      if (moves.Length > 0)
      {
          Console.WriteLine("Last Move Was:\n" + moves[0]);
      }
  }

  public void PrintBoard()
  {
      // Print out the current board state
      Console.WriteLine("+---+---+---+---+---+---+---+---+");
      for (int rank = 8; rank > 0; rank--)
      {
          Console.Write("|");
          for (int file = 1; file <= 8; file++)
          {
              bool found = false;
              // Loops through all of the pieces
              for (int p = 0; !found && p < pieces.Length; p++)
              {
                  // determines if that piece is at the current rank and file
                  if (pieces[p].getRank() == rank && pieces[p].getFile() == file)
                  {
                      found = true;
                      // Checks if the piece is black
                      if (pieces[p].getOwner() == 1)
                      {
                          Console.Write("*");
                      }
                      else
                      {
                          Console.Write(" ");
                      }
                      // prints the piece's type
                      Console.Write((char)pieces[p].getType() + " ");
                  }
              }
              if (!found)
              {
                  Console.Write("   ");
              }
              Console.Write("|");
          }
          Console.WriteLine("\n+---+---+---+---+---+---+---+---+");
      }
  }

  //This function is called once, before your first turn
  public override void init()
  {
      generator = new Random();
      piece_by_coordinate = new Hashtable();
  }

  //This function is called once, after your last turn
  public override void end() {}
  
  
  public AI(IntPtr c) : base(c)
  {}

  private Random generator;

  private List<Coordinate> GetMovesFor(char piece, Coordinate at, AI game)
  {
      // K=King, Q=Queen, B=Bishop, N=Knight, R=Rook, P=Pawn
      switch (piece)
      {
          case 'K':
              return King.GetMoves(at, game, false);

          case 'Q':
              return Queen.GetMoves(at, game, false);
              
          case 'B':
              return Bishop.GetMoves(at, game, false);
        
          case 'N':
              return Knight.GetMoves(at, game, false);
               
          case 'R':
              return Rook.GetMoves(at, game, false);
            
          case 'P':
              return Pawn.GetMoves(at, game, false);

          default:
              // Unknown piece, no moves for it. Go directly to jail.
              Debug("Looking for moves for unknown piece type!");
              return new List<Coordinate>();
      }
  }
}
