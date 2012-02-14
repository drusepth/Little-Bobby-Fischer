using System;
using System.Runtime.InteropServices;


///A chess move
public class Move
{
  public IntPtr ptr;
  protected int ID;
  protected int iteration;
  
  public Move()
  {
  }
  
  public Move(IntPtr p)
  {
    ptr = p;
    ID = Client.moveGetId(ptr);
    iteration = BaseAI.iteration;
  }

  public bool validify()
  {
    if(iteration == BaseAI.iteration) return true;
    for(int i = 0; i < BaseAI.moves.Length; i++)
    {
      if(BaseAI.moves[i].ID == ID)
      {
        ptr = BaseAI.moves[i].ptr;
        iteration = BaseAI.iteration;
        return true;
      }
    }
    throw new ExistentialError();
  }
    
    //commands
    
    
    //getters
    
  ///Unique Identifier
  public int getId()
  {
    validify();
    return Client.moveGetId(ptr);
  }
  ///The initial file location
  public int getFromFile()
  {
    validify();
    return Client.moveGetFromFile(ptr);
  }
  ///The initial rank location
  public int getFromRank()
  {
    validify();
    return Client.moveGetFromRank(ptr);
  }
  ///The final file location
  public int getToFile()
  {
    validify();
    return Client.moveGetToFile(ptr);
  }
  ///The final rank location
  public int getToRank()
  {
    validify();
    return Client.moveGetToRank(ptr);
  }
  ///The type of the piece for pawn promotion. Q=Queen, B=Bishop, N=Knight, R=Rook
  public int getPromoteType()
  {
    validify();
    return Client.moveGetPromoteType(ptr);
  }

}
