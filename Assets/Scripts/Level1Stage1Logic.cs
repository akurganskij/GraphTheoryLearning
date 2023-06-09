using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1Stage1Logic : MonoBehaviour
{
    Graph graph;

    private List<InputField> task1 = new List<InputField>();
    private List<InputField> task2 = new List<InputField>();

    private void Start()
    {
        EventManager.AddListener(EventName.GraphChangedEvent, RefreshTasks);
        List<string> strings = CSVProcessor.ReadCSV("Level1Stage1Text.csv", 22);
        GameObject.Find("TMP").GetComponent<TMP_Text>().text = String.Join("\n", strings);
        graph = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph;
    }
    private void RefreshTasks(int a)
    {
        graph = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph;
        foreach (GameObject input in GameObject.FindGameObjectsWithTag("Destroy"))
        {
            Destroy(input);
        }
        task1.Clear();
        task2.Clear();
        generateTask1();
        generateTask2();
        generateTask3();
    }

    private void generateTask1()
    {
        GameObject go = GameObject.Find("Task1Panel");        
        int k = graph.E;
        if(graph.E > 7) k = 7;
        task1.Add(go.GetComponentInChildren<InputField>());
        for(int i = 1; i < k; i++)
        {
            InputField inp = Instantiate(go.GetComponentInChildren<InputField>(),  go.GetComponent<Transform>());
            inp.transform.localPosition = new Vector3(
                inp.transform.localPosition.x,
                inp.transform.localPosition.y - 40.0f * i,
                inp.transform.localPosition.z);
            inp.tag = "Destroy";
            task1.Add(inp);
            
        }
    }
    private void generateTask2()
    {
        GameObject go = GameObject.Find("Task2Panel");
        int k = graph.V;
        go.GetComponentInChildren<InputField>().placeholder.GetComponent<Text>().text = "¬вед≥ть степ≥нь вершини 1";
        task2.Add(go.GetComponentInChildren<InputField>());
        for (int i = 1; i < k; i++)
        {
            InputField inp = Instantiate(go.GetComponentInChildren<InputField>(), go.GetComponent<Transform>());
            inp.transform.localPosition = new Vector3(
                inp.transform.localPosition.x,
                inp.transform.localPosition.y - 40.0f * i,
                inp.transform.localPosition.z);
            inp.tag = "Destroy";
            inp.placeholder.GetComponent<Text>().text = "¬вед≥ть степ≥нь вершини " + (i + 1);
            task2.Add(inp);
            
        }
    }
    private void generateTask3()
    {

    }

    public void HandleTask1CompleteButtonClickEvent()
    {

    }

    public void HandleTask2CompleteButtonClickEvent()
    {
        bool done = true;
        for(int i = 0; i < task2.Count; i++)
        {
            InputField field = task2[i];
            int k = -1;
            int.TryParse(field.text, out k);
            if(k != graph.deg(i + 1))
            {
                done = false;
            }
        }
        if (done)
        {
            Results.appendResults(1, 1, 2);
        }
        else
        {
            RefreshTasks(0);
        }
    }

    public void HandleTask3CompleteButtonClickEvent()
    {

    }

    public void HandleNextRoundInTask3ButtonClickEvent()
    {

    }
}
