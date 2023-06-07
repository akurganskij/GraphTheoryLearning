using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1Stage1Logic : MonoBehaviour
{
    Graph graph;
    private void Start()
    {
        List<string> strings = CSVReader.ReadCSV("Level1Stage1Text.csv", 22);
        GameObject.Find("TMP").GetComponent<TMP_Text>().text = String.Join("\n", strings);
        graph = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph;
    }
    private void Update()
    {
        if(graph != GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph)
        {
            graph = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph;
            generateTask1();
            generateTask2();
            generateTask3();
        }
    }

    private void generateTask1()
    {
        GameObject go = GameObject.Find("Task1Panel");
        int k = 7;
        if(graph.E < 7) k = graph.E;
        for(int i = 0; i < k; i++)
        {
            InputField inp = Instantiate(go.GetComponentInChildren<InputField>(), go.GetComponent<Transform>());
            
        }
    }
    private void generateTask2()
    {

    }
    private void generateTask3()
    {

    }
}
