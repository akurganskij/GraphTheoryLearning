using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector
{
    float x;
    float y;

    public Vector(Point a, Point b)
    {
        x = b.X - a.X;
        y = b.Y - a.Y;
    }
    public Vector()
    {
        x = 0;
        y = 0;
    }

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

    public Vector scale(float lambda)
    {
        x *= lambda;
        y *= lambda;
        return this;
    }
    public Vector Add(Vector other)
    {
        x += other.x;
        y += other.y;
        return this;
    }

    public float d()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public Vector norm()
    {
        float di = d();
        x /= di*di;
        y /= di*di;
        return this;
    }
}
