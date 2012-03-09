using System;

public class Coordinate
{
    int x, y;

    public Coordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int File
    {
        get { return X; }
    }

    public int Rank
    {
        get { return Y; }
    }
}