import com.sun.jna.Library;
import com.sun.jna.Pointer;
import com.sun.jna.Native;

public interface Client extends Library {
  Client INSTANCE = (Client)Native.loadLibrary("client", Client.class);
  Pointer createConnection();
  boolean serverConnect(Pointer connection, String host, String port);

  boolean serverLogin(Pointer connection, String username, String password);
  int createGame(Pointer connection);
  int joinGame(Pointer connection, int id, String playerType);

  void endTurn(Pointer connection);
  void getStatus(Pointer connection);

  int networkLoop(Pointer connection);


    //commands
  int pieceMove(Pointer object, int file, int rank, int type);

    //accessors
  int getTurnNumber(Pointer connection);
  int getPlayerID(Pointer connection);
  int getGameNumber(Pointer connection);
  int getTurnsToStalemate(Pointer connection);

  Pointer getMove(Pointer connection, int num);
  int getMoveCount(Pointer connection);
  Pointer getPiece(Pointer connection, int num);
  int getPieceCount(Pointer connection);
  Pointer getPlayer(Pointer connection, int num);
  int getPlayerCount(Pointer connection);


    //getters
  int moveGetId(Pointer ptr);
  int moveGetFromFile(Pointer ptr);
  int moveGetFromRank(Pointer ptr);
  int moveGetToFile(Pointer ptr);
  int moveGetToRank(Pointer ptr);
  int moveGetPromoteType(Pointer ptr);

  int pieceGetId(Pointer ptr);
  int pieceGetOwner(Pointer ptr);
  int pieceGetFile(Pointer ptr);
  int pieceGetRank(Pointer ptr);
  int pieceGetHasMoved(Pointer ptr);
  int pieceGetType(Pointer ptr);

  int playerGetId(Pointer ptr);
  String playerGetPlayerName(Pointer ptr);
  float playerGetTime(Pointer ptr);


    //properties

}
