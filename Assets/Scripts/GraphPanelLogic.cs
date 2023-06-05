using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GraphPanelLogic : MonoBehaviour
{
    [SerializeField] GameObject edge;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject vertex;
    [SerializeField] GameObject vertexInfo;
    [SerializeField] Transform tr;
    [SerializeField] float eps = 0.001f;
    [SerializeField] float l = 10.0f;
    [SerializeField] float delta = 0.999f;
    [SerializeField] float cRep = 1.5f;
    [SerializeField] float cSpr = 1.0f;
    [SerializeField] int k = 10000;

    Graph graph;
    System.Random r = new System.Random();
    List<Point> points = new List<Point>();
    float minX = 5, maxX = 100, minY = 5, maxY=100;

    private void Awake()
    {
        Vector3[] corners = new Vector3[4];
        tr.GetComponent<RectTransform>().GetWorldCorners(corners);
        minX = corners[1].x * 0.9f;
        minY = corners[3].y * 0.9f;
        maxX = corners[3].x * 0.9f;
        maxY = corners[1].y * 0.9f;
    }

    public void HandleNewGraphButtonClickEvent()
    {
        graph = GraphGenerator.generateRandomDirected(r.Next(1, 10));
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
                if (graph.IsOriented)
                {
                    Vector3 v1 = new Vector3(points[j - 1].X - points[i - 1].X,
                        points[j - 1].Y - points[i - 1].Y, 0);
                    float phi;
                    phi = Mathf.Atan2(v1.y, v1.x);
                    
                    GameObject go1 = GameObject.Instantiate(arrow, new Vector3(
                        points[j - 1].X, points[j - 1].Y, 10), 
                        Quaternion.EulerAngles(0, 0, Mathf.PI + phi), tr);
                }
            }
        }
        foreach (int j in graph.AdjacencyList.Keys)
        {
            GameObject go = GameObject.Instantiate(vertex,
                new Vector3(points[j-1].X, points[j-1].Y, 0), Quaternion.identity, tr);
            go.transform.localScale = new Vector3(10, 10, 1);
            go.AddComponent<Text>();
            go.GetComponent<Text>().text = j.ToString();

            Vector3 shift = new Vector3(0, 0, 0);
            foreach(int i in graph.AdjacencyList[j])
            {
                shift += new Vector3()
            }
            GameObject txt = GameObject.Instantiate(vertexInfo, new Vector3(points[j - 1].X, 
                points[j - 1].Y, 1) + shift, Quaternion.identity, tr);
            txt.GetComponent<Text>().text = j.ToString();
            
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