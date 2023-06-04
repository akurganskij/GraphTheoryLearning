using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphGenerator
{
    public static Graph generateRandom(int n)
    {
        Graph g = new Graph(false);
        System.Random r = new System.Random();
        g.V = n;
        for (int i = 1; i <= g.V; i++)
        {
            for (int j = 0; j <= r.Next(0, g.V); j++)
            {
                int v = r.Next(1, g.V + 1);
                if(v != i && !g.AdjacencyList[i].Contains(v))
                    g.AddEdge(i, v);
            }
        }
        return g;
    }
    public static Graph generateRandomDirected()
    {
        Graph g = new Graph(true);

        return g;
    }
    public static Graph generateTree()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateBipartite()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateMulti()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateRegular()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateArborescence()
    {
        Graph g = new Graph(true);

        return g;
    }
    public static Graph generateTournment()
    {
        Graph g = new Graph(true);

        return g;
    }
    public static Graph generateEmpty()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateComolete()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateNetwork()
    {
        Graph g = new Graph(false);

        return g;
    }
    public static Graph generateDAG()
    {
        Graph g = new Graph(true);

        return g;
    }
}
