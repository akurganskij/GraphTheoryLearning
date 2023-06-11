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
    [SerializeField] GameObject weightInfo;
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

    Dictionary<KeyValuePair<int, int>, GameObject> weights = new Dictionary<KeyValuePair<int, int>, GameObject>();

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
        EventManager.AddListener(EventName.ReloadGraph, HandleNewGraphButtonClickEvent);
        HandleNewGraphButtonClickEvent();
    }

    public void HandleNewGraphButtonClickEvent(int a = 0)
    {
        Awake();
        createGraph();
        unityEvents[EventName.GraphChangedEvent].Invoke(0);
        points.Clear();
        weights.Clear();
        GraphBuilder graphBuilder = new GraphBuilder(graph, 0, 100);
        points = graphBuilder.evaluatePoints(eps, k, l, delta, cRep, cSpr);
        if(graph.V > 1) scalePoints();
        else
        {
            points[0] = new Point((maxX + minX) / 2, (maxY + minY) / 2);
        }
        DrawGraph();
    }
    private void HandleNextStepEvent(int a = 0)
    {
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
                    new Vector3(points[i-1].X, points[i-1].Y, 0),
                    new Vector3(points[j-1].X, points[j-1].Y, 0),
                });
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.parent = tr;
                if (graph.IsOriented)
                {
                    Vector3 v1 = new Vector3(points[j - 1].X - points[i - 1].X,
                        points[j - 1].Y - points[i - 1].Y, -101);
                    float phi;
                    phi = Mathf.Atan2(v1.y, v1.x);
                    
                    GameObject go1 = GameObject.Instantiate(arrow, new Vector3(
                        points[j - 1].X, points[j - 1].Y, -2), 
                        Quaternion.EulerAngles(0, 0, Mathf.PI + phi), tr);
                    go1.transform.localPosition = new Vector3(
                        points[j - 1].X, points[j - 1].Y, -2);
                    go1.transform.parent = tr;
                }
                if (graph.IsWeighted)
                {
                    Vector3 cord = new Vector3(
                        (points[j - 1].X + points[i - 1].X) / 2,
                        (points[j - 1].Y + points[i - 1].Y) / 2,
                        -99
                    );
                    GameObject go2 = GameObject.Instantiate(weightInfo, cord, Quaternion.identity, tr);
                    go2.transform.localPosition = cord;
                    go2.GetComponentInChildren<Text>().text = graph.getWeightsforEdge(i, j).ToString();
                    weights.Add(new KeyValuePair<int, int> (i, j), go2);

                }
            }
        }
        foreach (int j in graph.AdjacencyList.Keys)
        {
            GameObject go = GameObject.Instantiate(vertex,
                new Vector3(points[j-1].X, points[j-1].Y, -1), Quaternion.identity, tr);
            go.transform.localPosition = new Vector3(points[j - 1].X, points[j - 1].Y, -1);
            go.transform.localScale = new Vector3(10, 10, -1);

            GameObject txt = GameObject.Instantiate(vertexInfo, new Vector3(points[j - 1].X, 
                points[j - 1].Y, -10), Quaternion.identity, tr);
            txt.transform.localPosition = new Vector3(points[j - 1].X, points[j -1].Y, -10);
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
                    graph = GraphGenerator.generateNetwork(r.Next(minVertex, maxVertex + 1));
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

    public void selectVertexes(List<int> verts)
    {
        foreach(SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            Vector3 cord = sprite.transform.parent.transform.localPosition;
            Point p = new Point(cord.x, cord.y);
            int i = int.MinValue;
            for(int j = 0; j < points.Count; j++)
            {
                if(points[j] == p)
                {
                    i = j;
                    break;
                }
            }
            if (verts.Contains(i + 1) || verts.Contains(-i - 1))
            {
                sprite.color = Color.magenta;
            }
        }
    }
    public void selectEdges(List<int> verts)
    {
        foreach (LineRenderer edge in gameObject.GetComponentsInChildren<LineRenderer>())
        {
            Vector3 cord1 = edge.GetPosition(0);
            Vector3 cord2 = edge.GetPosition(1);
            Point p1 = new Point(cord1.x, cord1.y);
            Point p2 = new Point(cord2.x, cord2.y);
            int i1 = int.MinValue;
            int i2 = int.MinValue;
            for (int j = 0; j < points.Count; j++)
            {
                if (points[j] == p1)
                {
                    i1 = j;
                }
                if (points[j] == p2)
                {
                    i2 = j;
                }
            }
            int v1i = verts.IndexOf(i1 + 1);
            if(v1i == -1) v1i = verts.IndexOf(-i1 + -1);
            int v2i = verts.IndexOf(i2 + 1);
            if (v2i == -1) v2i = verts.IndexOf(-i2 + -1);

            if (Mathf.Abs(v1i - v2i) == 1 && v1i != -1 && v2i != -1)
            {
                edge.SetColors(Color.green, Color.green);
            }
        }
    }

    public void clearSelection()
    {
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color = Color.blue;
        }
        foreach (LineRenderer edge in gameObject.GetComponentsInChildren<LineRenderer>())
        {
            edge.SetColors(Color.black, Color.black);
        }
    }

    public void RefreshWeights()
    {
        foreach(int i in graph.AdjacencyList.Keys)
        {
            foreach(int j in graph.AdjacencyList[i])
            {
                weights[new KeyValuePair<int, int>(i, j)].GetComponentInChildren<Text>().text = graph.getWeightsforEdge(i, j).ToString();
            }
        }
    }
}
