using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphFromAdjListLoader : MonoBehaviour
{
    Graph g;
    public void HandleDrawButtonClickEvent()
    {
        string v = GameObject.FindGameObjectWithTag("VertexNum").GetComponent<Text>().text;
        string l = GameObject.FindGameObjectWithTag("AdjList").GetComponent<Text>().text;
        try
        {
            g = new Graph(true);
            int n = int.Parse(v);
            g.V = n;
            string[] s = l.Split('\n');
            string k;
            for (int i = 0; i < n; i++)
            {
                k = s[i];
                k.Remove(0, k.IndexOf(':'));
                string[] vs = k.Split(' ');
                for (int j = 0; j < vs.Length; j++)
                {
                    g.AddEdge(i + 1, int.Parse(vs[j]));
                }
            }
        }
        catch (System.Exception e)
        {

        }
    }
}
