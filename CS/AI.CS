using System;
using System.Runtime.InteropServices;

///The class implementing gameplay logic.
class AI : BaseAI
{
  public override string username()
  {
    return "eggs";
  }
  public override string password()
  {
    return "thm89253";
  }

  //This function is called each time it is your turn
  //Return true to end your turn, return false to ask the server for updated information
  public override bool run()
  {
    // Print out the current board state
    Console.WriteLine("+---+---+---+---+---+---+---+---+");
    for(int rank=8; rank>0; rank--)
    {
      Console.Write("|");
      for(int file=1; file<=8; file++)
      {
        bool found = false;
        // Loops through all of the pieces
        for(int p=0; !found && p<pieces.Length; p++)
        {
          // determines if that piece is at the current rank and file
          if(pieces[p].getRank() == rank && pieces[p].getFile() == file)
          {
            found = true;
            // Checks if the piece is black
            if(pieces[p].getOwner() == 1)
            {
              Console.Write("*");
            }
            else
            {
              Console.Write(" ");
            }
            // prints the piece's type
            Console.Write((char)pieces[p].getType()+" ");
          }
        }
        if(!found)
        {
          Console.Write("   ");
        }
        Console.Write("|");
      }
      Console.WriteLine("\n+---+---+---+---+---+---+---+---+");
    }

    // Looks through information about the players
    for(int p=0; p<players.Length; p++)
    {
      Console.Write(players[p].getPlayerName());
      // if playerID is 0, you're white, if its 1, you're black
      if(players[p].getId() == playerID())
      {
        Console.Write(" (ME)");
      }
      Console.WriteLine(" time remaining: "+players[p].getTime());
    }
    // if there has been a move, print the most recent move
    if(moves.Length > 0)
    {
      Console.WriteLine("Last Move Was:\n"+moves[0]);
    }
    // select a random piece and move it to a random position on the board.  Attempts to promote to queen if a promotion happens
    pieces[generator.Next(pieces.Length-1)].move(generator.Next(7)+1, generator.Next(7)+1, (int)'Q');
    return true;
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
}
