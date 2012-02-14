using System;
using System.Runtime.InteropServices;


///A chess piece
public class Piece
{
  public IntPtr ptr;
  protected int ID;
  protected int iteration;
  
  public Piece()
  {
  }
  
  public Piece(IntPtr p)
  {
    ptr = p;
    ID = Client.pieceGetId(ptr);
    iteration = BaseAI.iteration;
  }

  public bool validify()
  {
    if(iteration == BaseAI.iteration) return true;
    for(int i = 0; i < BaseAI.pieces.Length; i++)
    {
      if(BaseAI.pieces[i].ID == ID)
      {
        ptr = BaseAI.pieces[i].ptr;
        iteration = BaseAI.iteration;
        return true;
      }
    }
    throw new ExistentialError();
  }
    
    //commands
    
  ///
  public int move(int file, int rank, int type)
  {
    validify();
    return Client.pieceMove(ptr, file, rank, type);
  }
    
    //getters
    
  ///Unique Identifier
  public int getId()
  {
    validify();
    return Client.pieceGetId(ptr);
  }
  ///The owner of the piece
  public int getOwner()
  {
    validify();
    return Client.pieceGetOwner(ptr);
  }
  ///The letter this piece is at (1-8)
  public int getFile()
  {
    validify();
    return Client.pieceGetFile(ptr);
  }
  ///The number this piece is at (1-8)
  public int getRank()
  {
    validify();
    return Client.pieceGetRank(ptr);
  }
  ///1=has moved, 0=has not moved
  public int getHasMoved()
  {
    validify();
    return Client.pieceGetHasMoved(ptr);
  }
  ///The letter that describes this piece's type. K=King, Q=Queen, B=Bishop, N=Knight, R=Rook, P=Pawn
  public int getType()
  {
    validify();
    return Client.pieceGetType(ptr);
  }

}
