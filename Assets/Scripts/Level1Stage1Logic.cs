using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Level1Stage1Logic : MonoBehaviour
{
    [SerializeField] GameObject edge;
    [SerializeField] GameObject vertex;
    [SerializeField] Transform tr;


    Graph graph;
    System.Random r = new System.Random();
    List<Point> points = new List<Point>();
    float minX = 5, maxX = 100, minY = 5, maxY=100;
    const float eps = 0.001f, l = 1.0f, delta = 0.999f, cRep = 2.5f, cSpr = 1.0f;
    const int k = 8000;

    private void Awake()
    {
        Vector3[] corners = new Vector3[4];
        tr.GetComponent<RectTransform>().GetWorldCorners(corners);
        minX = corners[1].x * 0.95f;
        minY = corners[3].y * 0.95f;
        maxX = corners[3].x * 0.95f;
        maxY = corners[1].y * 0.95f;
    }

    public void HandleNewGraphButtonClickEvent()
    {
        int isOriented = r.Next(0, 2);
        bool oriented = true;
        if (isOriented == 0) oriented = false;
        graph = new Graph(oriented);

        graph.V = r.Next(1, 8);
        for(int i = 1; i <= graph.V; i++)
        {
            for (int j = 0; j <= r.Next(0, graph.V + 1); j++)
            {
                graph.AddEdge(i, r.Next(1, graph.V + 1));
            }
        }
        points.Clear();
        GraphBuilder graphBuilder = new GraphBuilder(graph, 0, 100);
        points = graphBuilder.evaluatePoints(eps, k, l, delta, cRep, cSpr);
        if(graph.V > 1) scalePoints();
        else
        {
            points[0] = new Point((maxX + minX) / 2, (maxY + minY) / 2);
        }
        DrawGraph();
    }

    private void DrawGraph()
    {
        foreach(Transform child in tr)
        {
            if(child != null)
            {
                Destroy(child.gameObject);
            }
        }
        foreach(int i in graph.AdjacencyList.Keys)
        {
            foreach (int j in graph.AdjacencyList[i])
            {
                GameObject go = GameObject.Instantiate(edge, tr);
                go.GetComponent<LineRenderer>().SetPositions(new Vector3[]
                {
                    new Vector3(points[i-1].X, points[i-1].Y, 10),
                    new Vector3(points[j-1].X, points[j-1].Y, 10)
                });
            }
        }
        foreach (int j in graph.AdjacencyList.Keys)
        {
            GameObject go = GameObject.Instantiate(vertex,
                new Vector3(points[j-1].X, points[j-1].Y, 0), Quaternion.identity, tr);
            go.transform.localScale = new Vector3(10, 10, 1);
        }
    }

    private void scalePoints()
    {
        float dx, dy;
        float dxmin, dxmax, dymin, dymax;
        dxmin = points[0].X;
        dymin = points[0].Y;
        dxmax = points[0].X;
        dymax = points[0].Y;
        foreach(Point p in points)
        {
            if(p.X < dxmin) dxmin = p.X;
            if(p.Y < dymin) dymin = p.Y;
            if(p.X > dxmax) dxmax = p.X;
            if(p.Y > dymax) dymax = p.Y;
        }
        dx = dxmax - dxmin;
        dy = dymax - dymin;
        foreach(Point p in points)
        {
            p.X -= dxmin;
            p.Y -= dymin;
            p.X /= dx;
            p.Y /= dy;
            p.X *= (maxX - minX);
            p.Y *= (maxY - minY);
            p.X += minX;
            p.Y += minY;
        }
        
    }
}
