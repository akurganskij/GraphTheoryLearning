using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFordFulkerson : MonoBehaviour
{
    List<List<int>> pathes = new List<List<int>>();
    List<bool> visited = new List<bool>();
    Timer timer;
    Graph g;
    int pathNum = 0;
    GraphPanelLogic panel;
    int maxFlow = 0;
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.AddListener(EventName.GraphChangedEvent, Refresh);
        panel = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            if (pathes.Count > 0 && pathNum < pathes.Count)
            {
                if (timer.Finished)
                {
                    panel.clearSelection();
                    panel.selectVertexes(pathes[pathNum]);
                    panel.selectEdges(pathes[pathNum]);
                    int amount = int.MaxValue;
                    for (int i = 1; i < pathes[pathNum].Count; i++)
                    {
                        int u, v;
                        u = pathes[pathNum][i];
                        v = pathes[pathNum][i - 1];
                        if (g.containsEdge(u, v))
                        {
                            if(amount > g.getWeightsforEdge(u, v))
                               amount = g.getWeightsforEdge(u, v);
                        }
                        else
                        {
                            if (amount > g.getWeightsforEdge(v, u))
                                amount = g.getWeightsforEdge(v, u);
                        }
                    }
                    for (int i = 1; i < pathes[pathNum].Count; i++)
                    {
                        int u, v;
                        u = pathes[pathNum][i];
                        v = pathes[pathNum][i - 1];
                        if (pathes[pathNum][i] > 0)
                        {
                            if (g.containsEdge(u, v))
                                g.reduceCapacity(u, v, amount);
                            else
                                g.reduceCapacity(v, u, amount);
                        }
                        else
                        {
                            if (g.containsEdge(u, v))
                                g.reduceCapacity(u, v, -amount);
                            else
                                g.reduceCapacity(v, u, -amount);

                        }
                    }
                    panel.RefreshWeights();                    
                    pathNum++;
                    maxFlow += amount;
                    GameObject.Find("MaximumFlow").GetComponent<Text>().text = "Максимальний поток: " + maxFlow;
                    timer.Duration = 5;
                    timer.Run();
                }
            }
        }
    }

    private void Refresh(int a)
    {
        GameObject.Find("MaximumFlow").GetComponent<Text>().text = "Максимальний поток: 0";
        if (timer != null) timer.Stop();
        g = panel.CurrentGraph;
        visited.Clear();
        for(int i = 0; i <= g.V; i++)
        {
            visited.Add(false);
        }
        pathes.Clear();
        generatePathes(1, g.V, new List<int>(), visited);
        pathNum = 0;
        maxFlow = 0;
    }

    private void generatePathes(int parent, int d, List<int> path, List<bool> visited) 
    {
        path.Add(parent);
        if (parent < 0) parent *= -1;
        visited[parent] = true;
        if(parent == d)
        {
            pathes.Add(path);
            return;
        }
        for (int i = 1; i <= g.V; ++i)
        {
            if (g.containsEdge(parent, i))
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    generatePathes(i, d, new List<int>(path), new List<bool>(visited));
                }
            }
            if (g.containsEdge(i, parent))
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    generatePathes(-i, d, new List<int>(path), new List<bool>(visited));
                }
            }
        }
    }

    public void HandleStartAnimationButtonClick()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1;
        timer.Run();
    }
}
