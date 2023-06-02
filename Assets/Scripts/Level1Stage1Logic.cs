using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Level1Stage1Logic : Graphic
{
    [SerializeField] Canvas canvas;
    List<GameObject> gameObjects = new List<GameObject>();

    Graph graph;
    System.Random r = new System.Random();
    List<Point> points = new List<Point>();
    const float eps = 0.001f, l = 1.0f, delta = 0.99f;
    const int k = 1000000;

    public void HandleNewGraphButtonClickEvent()
    {
        int isOriented = r.Next(0, 2);
        bool oriented = true;
        if (isOriented == 0) oriented = false;
        graph = new Graph(oriented);

        graph.V = r.Next(1, 21);
        for(int i = 0; i < r.Next(0, 2 * graph.V + 1); i++)
        {
            graph.AddEdge(r.Next(1, graph.V + 1), r.Next(1, graph.V + 1));
        }
        points.Clear();
        GraphBuilder graphBuilder = new GraphBuilder(graph, 0, 100);
        points = graphBuilder.evaluatePoints(eps, k, l, delta);
        SetAllDirty();
    }

    private void DrawGraph(VertexHelper vh)
    {
        foreach(int i in graph.AdjacencyList.Keys)
        {
            foreach (int j in graph.AdjacencyList[i])
            {
                UIVertex u = UIVertex.simpleVert;
                u.position = new Vector3(points[i].X, points[i].Y, 0);
                UIVertex v = UIVertex.simpleVert;
                v.position = new Vector3(points[j].X, points[j].Y, 0);
                vh.AddVert(u);
                vh.AddVert(v);
                vh.AddTriangle(vh.currentVertCount - 1, vh.currentVertCount - 2, vh.currentVertCount - 1);

            }
        }

    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (graph != null)
        {
            DrawGraph(vh);
            vh.FillMesh(s_Mesh);
        }
    }

}
