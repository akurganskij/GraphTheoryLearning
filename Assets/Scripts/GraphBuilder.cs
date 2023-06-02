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
    }

    private void randomizePoints()
    {
        points.Clear();
        for(int i = 0; i <= g.V; i++)
        {
            points.Add(new Point(min + (float)random.NextDouble() * (max - min), min + (float)random.NextDouble() * (max - min)));
        }
    }

    public List<Point> evaluatePoints(float eps, int k, float l, float delta)
    {
        randomizePoints();
        int t = 1;
        float maxF = 1;
        while(t < k && maxF > eps)
        {
            forces.Clear();
            foreach(int u in g.AdjacencyList.Keys)
            {
                Vector f1 = new Vector(), f2 = new Vector(), v1;
                foreach(int v in g.AdjacentVertex(u))
                {
                    v1 = new Vector(points[u], points[v]);
                    f1.Add(v1.scale(l*l / points[u].dist(points[v])));
                    v1 = new Vector(points[v], points[u]);
                    f2.Add(v1.scale(Mathf.Pow(points[u].dist(points[v]),2) / l));
                }
                
                forces.Add(f1.Add(f2));
            }
            foreach(int u in g.AdjacencyList.Keys)
            {
                points[u] = points[u].Translate(forces[u-1].scale(delta));
            }
            maxF = forces[0].d();
            foreach(Vector v in forces)
            {
                if(v.d() > maxF) maxF = v.d();
            }
            t++;
        }
        for(int i = 0; i < points.Count; i++)
        {
            if(points[i].X == float.NaN) points[i].X = min + (float)random.NextDouble() * (max - min);
            if(points[i].Y == float.NaN) points[i].Y = min + (float)random.NextDouble() * (max - min);
        }
        return points;
    }
}
