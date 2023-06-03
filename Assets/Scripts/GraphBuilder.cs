using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GraphBuilder
{
    Graph g; 
    List<Point> points;
    List<Vector> forces;
    System.Random random;
    float min = 0, max = 100;

    public GraphBuilder(Graph g, float min, float max)
    {
        this.g = g;
        points = new List<Point>();
        forces = new List<Vector>();
        random = new System.Random();
        this.min = min;
        this.max = max;
        for(int i = 0; i < g.V; i++)
        {
            forces.Add(new Vector());
        }
    }

    private void randomizePoints()
    {
        points.Clear();
        for(int i = 0; i < g.V; i++)
        {
            Point p = new Point(min + (float)random.NextDouble() * (max - min), min + (float)random.NextDouble() * (max - min));
            while (points.Contains(p))
            {
                p = new Point(min + (float)random.NextDouble() * (max - min), min + (float)random.NextDouble() * (max - min));
            }
            points.Add(p);
        }
    }

    public List<Point> evaluatePoints(float eps, int k, float l, float delta, float cRep, float cSpring)
    {
        randomizePoints();
        if (g.V > 1)
        {
            int t = 1;
            float maxF = 1;
            while (t < k && maxF > eps)
            {
                clearForces();
                foreach (int u in g.AdjacencyList.Keys)
                {
                    repForces(u, cRep);
                    sprForces(u, l, cSpring);
                }
                translatePoints(delta);
                maxF = maxForce();
                t++;
            }
        }
        return points;
    }
    private void translatePoints(float delta)
    {
        foreach (int u in g.AdjacencyList.Keys)
        {
            points[u - 1] = points[u - 1].Translate(forces[u - 1].scale(delta));
        }
    }
    private float maxForce()
    {
        float temp = forces[0].d();
        foreach (Vector v in forces)
        {
            if (v.d() < temp) temp = v.d();
        }
        return temp;
    }
    private void clearForces()
    {
        for (int i = 0; i < forces.Count; i++) forces[i] = new Vector();
    }
    private void repForces(int u, float cRep)
    {
        Vector v1 = new Vector(), f1 = new Vector();
        List<int> adj = g.AdjacentVertex(u);
        foreach (int v in g.AdjacencyList.Keys)
        {
            if (u == v || adj.Contains(v)) continue;
            v1 = new Vector(points[u - 1], points[v - 1]);
            f1.Add(v1.norm().scale(cRep / points[u - 1].dist(points[v - 1])));
            forces[u-1].Add(f1);
        }
    }
    private void sprForces(int u, float l, float cSpr)
    {
        Vector v1 = new Vector(), f2 = new Vector();
        HashSet<int> adj = new HashSet<int>(g.AdjacentVertex(u));
        foreach (int v in adj)
        {
            if (u == v) continue;
            v1 = new Vector(points[v - 1], points[u - 1]);
            f2.Add(v1.norm().scale(cSpr * Mathf.Log(points[u - 1].dist(points[v - 1])/l)));
            forces[u - 1].Add(f2);
        }
    }
}
