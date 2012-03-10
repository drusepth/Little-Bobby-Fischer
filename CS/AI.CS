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
    Console.WriteLine(s);
  }

  public override bool run()
  {
      // Initialize Expected Files and Ranks for every piece
      for (int p = 0; p < pieces.Length; p++)
      {
          pieces[p].ExpectedFile = pieces[p].getFile();
          pieces[p].ExpectedRank = pieces[p].getRank();
      }

      // List of all valid moves, populated as we go
      List<MoveReference> valid_moves = new List<MoveReference>();

      // Populate valid_moves with board-legal moves
      foreach (Piece piece in pieces)
      {
          // Only find moves for my pieces
          if (piece.getOwner() == playerID())
          {
              // Get all moves for this piece at its current location
              Coordinate here = new Coordinate(piece.ExpectedFile, piece.ExpectedRank);
              foreach (Coordinate destination in GetMovesFor((char)piece.getType(), here, this))
              {
                  MoveReference move = new MoveReference(piece, destination);
                  if (IsValidMove(move))
                  {
                      valid_moves.Add(move);
                  }
              }
          }
      }

      // Order moves by Iterative-Deepening Depth-Limited MiniMax with a material advantage state 
      // eval heuristic


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

  // Determines whether a move is valid given the current board state, looking at
  // whether it ends up on an ally piece and whether it ends with us in check.
  private bool IsValidMove(MoveReference move)
  {
      // Piece that is moving
      Piece piece = move.Piece;

      // Where the piece is coming from (so we can revert later)
      Coordinate origin = new Coordinate(piece.ExpectedFile, piece.ExpectedRank);

      // Where this piece is moving to
      Coordinate destination = move.To;

      // Find any pieces that might be where we're going
      Piece target_piece = GetPieceAt(destination);
      if (target_piece != null)
      {
          // First, make sure the piece at that destination isn't ours!
          if (target_piece.getOwner() == playerID())
          {
              // If it's ours, we clearly cannot make this move
              return false;
          }

          // Temporarily update the board state to take this piece off the board
          target_piece.ExpectedRank = -1;
          target_piece.ExpectedFile = -1;
      }

      // We want to make sure making this move will not place us in check. To do that,
      // we're going to look outward from the King after making the move and see if
      // any enemy pieces are putting us in check.
      piece.ExpectedFile = destination.File;
      piece.ExpectedRank = destination.Rank;
      if (IsKingInCheck())
      {
          Debug(String.Format("Pruned {0} ({1}, {2}) to ({3}, {4})", 
              (char)piece.getType(), origin.File, origin.Rank, destination.File, destination.Rank));

          // Revert the board back to its original state
          piece.ExpectedFile = origin.File;
          piece.ExpectedRank = origin.Rank;

          if (target_piece != null)
          {
              target_piece.ExpectedRank = destination.Rank;
              target_piece.ExpectedFile = destination.File;
          }

          // Don't make any moves that end with us in check
          return false;
      }

      // If the move doesn't leave us in check, it's a valid move. Reverting back to the
      // original board state before returning true.
      if (target_piece != null)
      {
          target_piece.ExpectedRank = destination.Rank;
          target_piece.ExpectedFile = destination.File;
      }

      // And finally, revert the moving piece back to its original state so we can properly
      // look at other moves
      piece.ExpectedFile = origin.File;
      piece.ExpectedRank = origin.Rank;

      // Great success!
      return true;
  }

  // Given the current board state, look outward from the king and determine if any enemy
  // pieces can take our fearless leader.
  private bool IsKingInCheck()
  {
      // Grab our king's current (expected) location
      Piece king = FindMyKing();
      Coordinate king_location = new Coordinate(king.ExpectedFile, king.ExpectedRank);

      // Dictionary of moves to look for, spanning outward from king
      var threats = new Dictionary<char, List<Coordinate>>() 
      {
          { 'R', Rook.GetMoves(king_location, this) },
          { 'B', Bishop.GetMoves(king_location, this) },
          { 'N', Knight.GetMoves(king_location, this) },
          { 'P', Pawn.GetAttackMoves(king_location, this) },
          { 'K', King.GetMoves(king_location, this) },
          { 'Q', Queen.GetMoves(king_location, this) }
      };

      // For every piece type...
      foreach (char piece_type in new List<Char>() { 'R', 'B', 'N', 'P', 'K', 'Q' })
      {
          // Look up threats in the dictionary directly rather than using a foreach
          // to avoid duplicating move lists in memory.
          var threat = threats[piece_type];

          // ...look at all moves they could make if they were in the king's location...
          foreach (Coordinate destination in threat) 
          {
              // ...and see if there's actually an enemy piece there that could attack back
              Piece bad_dude = GetPieceAt(destination);

              // If (a piece exists there) and (it's of the right type) and (it's not ours)
              if (bad_dude != null && (char)bad_dude.getType() == piece_type && bad_dude.getOwner() != playerID())
              {
                  // Prune!
                  return true;
              }
          }
      }

      return false;
  }

  // Returns whether a piece's owner is White
  public bool IsWhite(Piece p) { return p.getOwner() == 0; }

  // Looks through all pieces and finds the king; returns a reference to it.
  public Piece FindMyKing()
  {
      for (int i = 0; i < pieces.Length; i++)
      {
          if (pieces[i].getOwner() == playerID() && (char)pieces[i].getType() == 'K')
          {
              return pieces[i];
          }
      }

      // We should be guaranteed a king. This is chess, after all.
      throw new Exception("No king found!");
  }

  public bool MyPieceExistsAt(Coordinate c)
  {
      Piece piece = GetPieceAt(c);
      return piece != null && piece.getOwner() == playerID();
  }

  public bool IsMyPiece(Piece p)
  {
      return p != null && p.getOwner() == playerID();
  }

  public bool EnemyPieceExistsAt(Coordinate c)
  {
      Piece piece = GetPieceAt(c);
      return piece != null && piece.getOwner() != playerID();
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
      return GetPieceAt(c) != null;
  }

  private void PrintValidMoves(List<MoveReference> move_list, MoveReference chosen)
  {
      Console.Write("Other moves: ");
      for (int i = 0; i < move_list.Count; i++)
      {
          MoveReference mv = move_list[i];
          // #todo don't use getId() lolol
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
              return King.GetMoves(at, game);

          case 'Q':
              return Queen.GetMoves(at, game);
              
          case 'B':
              return Bishop.GetMoves(at, game);
        
          case 'N':
              return Knight.GetMoves(at, game);
               
          case 'R':
              return Rook.GetMoves(at, game);
            
          case 'P':
              return Pawn.GetMoves(at, game);

          default:
              // Unknown piece, no moves for it. Go directly to jail.
              throw new Exception("Looking for moves for unknown piece type!");
      }
  }
}
