using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    float x;
    float y;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public Point() : this(0, 0) { }

    public float X
    {
        get { return x; }
        set { x = value; }
    }
    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    public float dist(Point other)
    {
        return Mathf.Sqrt(Mathf.Pow(x - other.x, 2) + Mathf.Pow(y - other.y, 2));
    }

    public Point Translate(Vector u)
    {
        x += u.X;
        y += u.Y;
        return this;
    }

    public static bool operator ==(Point a, Point b) {
        return Mathf.Abs(a.x - b.x) < 0.2f && Mathf.Abs(a.y - b.y) < 0.2f;
    }
    public static bool operator !=(Point a, Point b)
    {
        return Mathf.Abs(a.x - b.x) > 0.2f || Mathf.Abs(a.y - b.y) > 0.2f; ;
    }
}
