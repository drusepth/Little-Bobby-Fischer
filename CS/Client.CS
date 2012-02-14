using System;
using System.Runtime.InteropServices;

public class Client {
  [DllImport("client")]
  public static extern IntPtr createConnection();
  [DllImport("client")]
  public static extern int serverConnect(IntPtr connection, string host, string port);

  [DllImport("client")]
  public static extern int serverLogin(IntPtr connection, string username, string password);
  [DllImport("client")]
  public static extern int createGame(IntPtr connection);
  [DllImport("client")]
  public static extern int joinGame(IntPtr connection, int id, string playerType);

  [DllImport("client")]
  public static extern void endTurn(IntPtr connection);
  [DllImport("client")]
  public static extern void getStatus(IntPtr connection);

  [DllImport("client")]
  public static extern int networkLoop(IntPtr connection);


    //commands
  [DllImport("client")]
  public static extern int pieceMove(IntPtr self, int file, int rank, int type);

    //accessors
  [DllImport("client")]
  public static extern int getTurnNumber(IntPtr connection);
  [DllImport("client")]
  public static extern int getPlayerID(IntPtr connection);
  [DllImport("client")]
  public static extern int getGameNumber(IntPtr connection);
  [DllImport("client")]
  public static extern int getTurnsToStalemate(IntPtr connection);

  [DllImport("client")]
  public static extern IntPtr getMove(IntPtr connection, int num);
  [DllImport("client")]
  public static extern int getMoveCount(IntPtr connection);
  [DllImport("client")]
  public static extern IntPtr getPiece(IntPtr connection, int num);
  [DllImport("client")]
  public static extern int getPieceCount(IntPtr connection);
  [DllImport("client")]
  public static extern IntPtr getPlayer(IntPtr connection, int num);
  [DllImport("client")]
  public static extern int getPlayerCount(IntPtr connection);


    //getters
  [DllImport("client")]
  public static extern int moveGetId(IntPtr ptr);
  [DllImport("client")]
  public static extern int moveGetFromFile(IntPtr ptr);
  [DllImport("client")]
  public static extern int moveGetFromRank(IntPtr ptr);
  [DllImport("client")]
  public static extern int moveGetToFile(IntPtr ptr);
  [DllImport("client")]
  public static extern int moveGetToRank(IntPtr ptr);
  [DllImport("client")]
  public static extern int moveGetPromoteType(IntPtr ptr);

  [DllImport("client")]
  public static extern int pieceGetId(IntPtr ptr);
  [DllImport("client")]
  public static extern int pieceGetOwner(IntPtr ptr);
  [DllImport("client")]
  public static extern int pieceGetFile(IntPtr ptr);
  [DllImport("client")]
  public static extern int pieceGetRank(IntPtr ptr);
  [DllImport("client")]
  public static extern int pieceGetHasMoved(IntPtr ptr);
  [DllImport("client")]
  public static extern int pieceGetType(IntPtr ptr);

  [DllImport("client")]
  public static extern int playerGetId(IntPtr ptr);
  [DllImport("client")]
  public static extern IntPtr playerGetPlayerName(IntPtr ptr);
  [DllImport("client")]
  public static extern float playerGetTime(IntPtr ptr);


    //properties

}
