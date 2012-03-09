using System;
using System.Collections.Generic;

class MoveReference
{
    Piece piece;
    Coordinate to;

    public MoveReference(Piece _piece, Coordinate _to)
    {
        piece = _piece;
        to = _to;
    }

    public Coordinate To
    {
        get { return to; }
        set { to = value; }
    }

    public Piece Piece
    {
        get { return piece; }
        set { piece = value; }
    }
}