using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    Dictionary<int, List<int>> adjacencyList;
    bool isOriented;
    public Graph(bool isOriented)
    {
        adjacencyList = new Dictionary<int, List<int>>();
        this.isOriented = isOriented;
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
        set { for(int i = 1; i <= value; i++) {
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
            return e;
        }
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

}
