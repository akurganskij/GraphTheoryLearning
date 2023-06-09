using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphGenerator
{
    public static Graph generateRandom(int n, bool isWeighted)
    {
        Graph g = new Graph(false, isWeighted);
        System.Random r = new System.Random();
        g.V = n;
        for (int i = 1; i <= g.V; i++)
        {
            for (int j = 0; j <= r.Next(0, g.V); j++)
            {
                int v = r.Next(1, g.V + 1);
                if (v != i && !g.AdjacencyList[i].Contains(v))
                    g.AddEdge(i, v);
            }
        }
        return g;
    }
    public static Graph generateRandomDirected(int n, bool isWeighted)
    {
        Graph g = new Graph(true, isWeighted);
        System.Random r = new System.Random();
        g.V = n;
        for (int i = 1; i <= g.V; i++)
        {
            for (int j = 0; j <= r.Next(0, g.V); j++)
            {
                int v = r.Next(1, g.V + 1);
                if (v != i && !g.AdjacencyList[i].Contains(v) && !g.AdjacencyList[v].Contains(i))
                    g.AddEdge(i, v);
            }
        }
        return g;
    }
    public static Graph generateTree(int n, bool isWeighted)
    {
        Graph g = new Graph(false, isWeighted);
        System.Random r = new System.Random();
        List<int> code = new List<int>();
        List<int> v = new List<int>();
        g.V = n;
        for (int i = 0; i < n - 2; i++)
        {
            code.Add(r.Next(1, g.V + 1));
        }
        for (int i = 0; i < g.V; i++)
        {
            v.Add(i+1);
        }
        int node = 1;
        while(code.Count != 0)
        {
            while (code.Contains(node) || !v.Contains(node))
            {
                node++;
                if (node > g.V)
                {
                    node = 1;
                }
            }
            g.AddEdge(node, code[0]);
            code.RemoveAt(0);
            v.Remove(node);
        }
        if(v.Count == 2)
            g.AddEdge(v[0], v[1]);
        return g;
    }
    public static Graph generateBipartite(int n, bool isWeighted)
    {
        Graph g = new Graph(false, isWeighted);

        return g;
    }
    public static Graph generateMulti(int n, bool isWeighted)
    {
        Graph g = new Graph(false, isWeighted);
        System.Random r = new System.Random();
        g.V = n;
        for (int i = 1; i <= g.V; i++)
        {
            for (int j = 0; j <= r.Next(0, 2 * g.V); j++)
            {
                int v = r.Next(1, g.V + 1);
                g.AddEdge(i, v);
            }
        }
        return g;
    }
    public static Graph generateRegular(int n, int deg, bool isWeighted)
    {
        if(deg >= n) deg = n - 1;
        if (deg == n - 1) return generateComplete(n, isWeighted);
        if (deg % 2 == 1 && n % 2 == 1) deg--;
        Graph g = new Graph(false, isWeighted);
        System.Random r = new System.Random();
        g.V = n;
        int e = 0, t = deg;
        for (int i = 1; i <= g.V; i++)
        {
            while (g.AdjacentVertex(i).Count < deg && deg - e <= t)
            {
                int v = r.Next(1, g.V + 1);
                if (v != i && !g.AdjacencyList[i].Contains(v) && g.AdjacentVertex(v).Count < deg) {
                    g.AddEdge(i, v);
                    e++;
                }
                t = deg;
                foreach(int j in g.AdjacencyList.Keys)
                {
                    if(g.AdjacentVertex(j).Count == deg)
                    {
                        t--;
                    }
                }
            }
            if(g.AdjacentVertex(i).Count < deg)
            {
                return generateEmpty(n);
            }
            t = deg;
            e = 0;
        }
        return g;
    }
    public static Graph generateArborescence(int n, bool isWeighted)
    {
        Graph g = new Graph(true, isWeighted);

        return g;
    }
    public static Graph generateTournment(int n, bool isWeighted)
    {
        Graph g = new Graph(true, isWeighted);
        System.Random r = new System.Random();
        g.V = n;
        for (int i = 1; i <= n; i++)
        {
            for (int j = i + 1; j <= n; j++)
            {
                if(r.Next(0, 2) % 2 == 1)
                    g.AddEdge(i, j);
                else
                    g.AddEdge(j, i);
            }
        }
        return g;
    }
    public static Graph generateEmpty(int n)
    {
        Graph g = new Graph(false);
        g.V = n;
        return g;
    }
    public static Graph generateComplete(int n, bool isWeighted)
    {
        Graph g = new Graph(false, isWeighted);
        g.V = n;
        for(int i = 1; i <= n; i++)
        {
            for(int j = i + 1; j <= n; j++)
            {
                g.AddEdge(i, j);
            }
        }
        return g;
    }
    public static Graph generateNetwork(int n, bool isWeighted)
    {
        Graph g = new Graph(true, isWeighted);

        return g;
    }
    public static Graph generateDAG(int n, bool isWeighted)
    {
        Graph g = new Graph(true, isWeighted);

        return g;
    }
}
