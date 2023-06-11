using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    Dictionary<int, List<int>> adjacencyList;
    Dictionary<KeyValuePair<int, int>, int> weghts;
    bool isOriented, isWeighted;
    public Graph(bool isOriented)
    {
        adjacencyList = new Dictionary<int, List<int>>();
        this.isOriented = isOriented;
        this.isWeighted = false;
    }

    public Graph(bool isOriented, bool isWeighted)
    {

        adjacencyList = new Dictionary<int, List<int>>();
        weghts = new Dictionary<KeyValuePair<int, int>, int>();
        this.isOriented = isOriented;
        this.isWeighted = isWeighted;
    }


    public void AddEdge(int u, int v)
    {
        if (!adjacencyList.ContainsKey(u))
        {
            adjacencyList.Add(u, new List<int>());
        }
        adjacencyList[u].Add(v);

        if (!isOriented)
        {
            if (!adjacencyList.ContainsKey(v))
            {
                adjacencyList.Add(v, new List<int>());
            }
            adjacencyList[v].Add(u);
        }
    }

    public void AddEdge(int u, int v, int w)
    {
        if(weghts == null) weghts = new Dictionary<KeyValuePair<int, int>, int>();
        if (!adjacencyList.ContainsKey(u))
        {
            adjacencyList.Add(u, new List<int>());
        }
        adjacencyList[u].Add(v);

        if (!isOriented)
        {
            if (!adjacencyList.ContainsKey(v))
            {
                adjacencyList.Add(v, new List<int>());
            }
            adjacencyList[v].Add(u);
        }
        if (isWeighted)
        {
            weghts.Add(new KeyValuePair<int, int>(u, v), w);
            if(!isOriented)
                weghts.Add(new KeyValuePair<int, int>(v, u), w);
        }
    }

    public void SetAdjacencyList(Dictionary<int, List<int>> adjacencyList)
    {
        this.adjacencyList = adjacencyList;
    }

    public void SetAdjacencyMatrix(List<List<int>> adjacencyMatrix)
    {
        adjacencyList.Clear();
        for(int i = 0; i < adjacencyMatrix.Count; i++)
        {
            for(int j = 0; j < adjacencyMatrix[i].Count; j++)
            {
                if(adjacencyMatrix[i][j] == 0)
                {
                    if (!adjacencyList.ContainsKey(i))
                    {
                        adjacencyList.Add(i, new List<int>());
                    }
                    adjacencyList[i].Add(j);
                }
            }
        }
    }

    public int V
    {
        get { return adjacencyList.Count; }
        set { 
            for(int i = 1; i <= value; i++) {
                adjacencyList.Add(i, new List<int>());
            } 
        }
    }

    public int E
    {
        get
        {
            int e = 0;
            foreach(int k in this.adjacencyList.Keys)
            {
                e += adjacencyList[k].Count;
            }
            return e / 2;
        }
    }
    public Dictionary<KeyValuePair<int, int>, int> Weights
    {
        get { return weghts; }
    }

    public int getWeightsforEdge(int u, int v)
    {
        if(weghts.ContainsKey(new KeyValuePair<int, int>(u, v)))
        {
            return weghts[new KeyValuePair<int, int>(u, v)];
        }
        return int.MaxValue;
    }

    public Dictionary<int, List<int>> AdjacencyList
    {
        get { return adjacencyList; }
    }

    public List<int> AdjacentVertex(int vertex)
    {
        if (adjacencyList.ContainsKey(vertex)) {
            return adjacencyList[vertex];
        }
        return new List<int>();
    }

    public bool IsOriented
    {
        get { return isOriented; }
    }

    public bool IsWeighted
    {
        get { return isWeighted; }
    }

    public int deg(int u)
    {
        if (adjacencyList.ContainsKey(u))
        {
            return adjacencyList[u].Count;
        }
        return -1;
    }
    public int indeg(int u)
    {
        int res = 0;
        foreach(int i in adjacencyList.Keys)
        {
            if (adjacencyList[i].Contains(u)) res++;
        }
        return res;
    }

    public bool containsEdge(int u, int v)
    {
        if (adjacencyList.ContainsKey(u))
        {
            if (adjacencyList[u].Contains(v)) return true;
        }
        return false;
    }

    public void reduceCapacity(int u, int v, int amount)
    {
        if(weghts.ContainsKey(new KeyValuePair<int, int>(u, v)))
        {
            weghts[new KeyValuePair<int, int>(u, v)] -= amount;
        }
    }

    
}
