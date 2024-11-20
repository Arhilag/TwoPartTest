using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
}

public struct Edge
{
    public Rectangle First;
    public Rectangle Second;
    public Vector2 Start;
    public Vector2 End;

    public Edge(Rectangle first, Rectangle second, Vector2 start, Vector2 end)
    {
        First = first;
        Second = second;
        Start = start;
        End = end;
    }
}

public struct Rectangle
{
    public Vector2 Min;
    public Vector2 Max;

    public Rectangle(Vector2 min, Vector2 max)
    {
        Min = min;
        Max = max;
    }
}