using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GraphPanelLogic : IntEventInvoker
{
    [SerializeField] int minVertex;
    [SerializeField] int maxVertex;
    [SerializeField] GraphTypes graphType;
    [SerializeField] bool isWeighted;

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
        minX = -tr.GetComponent<RectTransform>().rect.width / 2 * 0.9f;
        minY = tr.GetComponent<RectTransform>().rect.y * 0.9f;
        maxX = tr.GetComponent<RectTransform>().rect.width / 2 * 0.9f;
        maxY = (tr.GetComponent<RectTransform>().rect.y + tr.GetComponent<RectTransform>().rect.height) * 0.9f;
    }

    private void Start()
    {
        unityEvents.Add(EventName.GraphChangedEvent, new GraphChangedEvent());
        EventManager.AddInvoker(EventName.GraphChangedEvent, this);
        HandleNewGraphButtonClickEvent();
    }

    public void HandleNewGraphButtonClickEvent()
    {
        Awake();
        createGraph();
        unityEvents[EventName.GraphChangedEvent].Invoke(0);
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
                    new Vector3(points[i-1].X, points[i-1].Y, -100),
                    new Vector3(points[j-1].X, points[j-1].Y, -100)
                });
                if (graph.IsOriented)
                {
                    Vector3 v1 = new Vector3(points[j - 1].X - points[i - 1].X,
                        points[j - 1].Y - points[i - 1].Y, -101);
                    float phi;
                    phi = Mathf.Atan2(v1.y, v1.x);
                    
                    GameObject go1 = GameObject.Instantiate(arrow, new Vector3(
                        points[j - 1].X, points[j - 1].Y, -101), 
                        Quaternion.EulerAngles(0, 0, Mathf.PI + phi), tr);
                    go1.transform.localPosition = new Vector3(
                        points[j - 1].X, points[j - 1].Y, -101);
                }
            }
        }
        foreach (int j in graph.AdjacencyList.Keys)
        {
            GameObject go = GameObject.Instantiate(vertex,
                new Vector3(points[j-1].X, points[j-1].Y, -101), Quaternion.identity, tr);
            go.transform.localPosition = new Vector3(points[j - 1].X, points[j - 1].Y, -101);
            go.transform.localScale = new Vector3(10, 10, -101);

            GameObject txt = GameObject.Instantiate(vertexInfo, new Vector3(points[j - 1].X, 
                points[j - 1].Y, -103), Quaternion.identity, tr);
            txt.transform.localPosition = new Vector3(points[j - 1].X, points[j -1].Y, -103);
            txt.GetComponent<Text>().text = j.ToString();
            
        }
    }

    private void createGraph()
    {
        switch (graphType)
        {
            case GraphTypes.Random:
                {
                    graph = GraphGenerator.generateRandom(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.RandomDirected:
                {
                    graph = GraphGenerator.generateRandomDirected(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Tree:
                {
                    graph = GraphGenerator.generateTree(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Bipartite:
                {
                    graph = GraphGenerator.generateBipartite(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Multi:
                {
                    graph = GraphGenerator.generateMulti(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Regular:
                {
                    int k = 0;
                    graph = null;
                    while((graph == null || graph.E == 0) && k < 4)
                        graph = GraphGenerator.generateRegular(r.Next(minVertex, maxVertex + 1), r.Next(0, maxVertex - 1), isWeighted);
                    break;
                }
            case GraphTypes.Arborescence:
                {
                    graph = GraphGenerator.generateArborescence(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Tournment:
                {
                    graph = GraphGenerator.generateTournment(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Empty:
                {
                    graph = GraphGenerator.generateEmpty(r.Next(minVertex, maxVertex + 1));
                    break;
                }
            case GraphTypes.Complete:
                {
                    graph = GraphGenerator.generateComplete(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.Network:
                {
                    graph = GraphGenerator.generateNetwork(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
            case GraphTypes.DAG:
                {
                    graph = GraphGenerator.generateDAG(r.Next(minVertex, maxVertex + 1), isWeighted);
                    break;
                }
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

    public Graph CurrentGraph
    {
        get { return graph; }
    }
}
