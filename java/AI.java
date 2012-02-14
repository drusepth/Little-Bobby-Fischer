import com.sun.jna.Pointer;
import java.util.Random;

///The class implementing gameplay logic.
class AI extends BaseAI
{
  public String username()
  {
    return "Shell AI";
  }
  public String password()
  {
    return "password";
  }

  //This function is called each time it is your turn
  //Return true to end your turn, return false to ask the server for updated information
  public boolean run()
  {
    // Print out the current board state
    System.out.println("+---+---+---+---+---+---+---+---+");
    for(int rank=8; rank>0; rank--)
    {
      System.out.print("|");
      for(int file=1; file<=8; file++)
      {
        boolean found = false;
        // Loops through all of the pieces
        for(int p=0; !found && p<pieces.length; p++)
        {
          // determines if that piece is at the current rank and file
          if(pieces[p].getRank() == rank && pieces[p].getFile() == file)
          {
            found = true;
            // Checks if the piece is black
            if(pieces[p].getOwner() == 1)
            {
              System.out.print("*");
            }
            else
            {
              System.out.print(" ");
            }
            // prints the piece's type
            System.out.print((char)pieces[p].getType()+" ");
          }
        }
        if(!found)
        {
          System.out.print("   ");
        }
        System.out.print("|");
      }
      System.out.println("\n+---+---+---+---+---+---+---+---+");
    }

    // Looks through information about the players
    for(int p=0; p<players.length; p++)
    {
      System.out.print(players[p].getPlayerName());
      // if playerID is 0, you're white, if its 1, you're black
      if(players[p].getId() == playerID())
      {
        System.out.print(" (ME)");
      }
      System.out.println(" time remaining: "+players[p].getTime());
    }

    // if there has been a move, print the most recent move
    if(moves.length > 0)
    {
      System.out.println("Last Move Was:\n"+moves[0]);
    }
    // select a random piece and move it to a random position on the board.  Attempts to promote to queen if a promotion happens
    pieces[generator.nextInt(pieces.length-1)].move(generator.nextInt(7)+1, generator.nextInt(7)+1, (int)'Q');
    return true;
  }


  //This function is called once, before your first turn
  public void init()
  {
    generator = new Random();
  }

  //This function is called once, after your last turn
  public void end() {}
  
  
  public AI(Pointer c)
  {
    super(c);
  }

  private Random generator;
}
