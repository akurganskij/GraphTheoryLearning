using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1Stage1Logic : IntEventInvoker
{
    [SerializeField] GameObject correct;
    [SerializeField] GameObject incorrect;

    Graph graph;
    Timer timer;

    private List<Dropdown> task1 = new List<Dropdown>();
    private List<Text> task1Texts = new List<Text>();
    private List<bool> task1Answers = new List<bool>();
    
    private List<InputField> task2 = new List<InputField>();

    private int task3Res;

    GameObject rgo;

    private void Start()
    {
        EventManager.AddListener(EventName.GraphChangedEvent, RefreshTasks);
        unityEvents.Add(EventName.ReloadGraph, new ReloadGraphEvent());
        EventManager.AddInvoker(EventName.ReloadGraph, this);
        List<string> strings = CSVProcessor.ReadCSV("Level1Stage1Text.csv", 23);
        GameObject.Find("TMP").GetComponent<TMP_Text>().text = String.Join("\n", strings);
        graph = GameObject.Find("GraphPanel").GetComponent<GraphPanelLogic>().CurrentGraph;

        List<string> tasksText = CSVProcessor.ReadCSV("Level1Stage1Tasks.csv", 3);
        for(int i = 1; i <= 3; i++)
        {
            GameObject.Find("TextTask" + i).GetComponent<TMP_Text>().text = tasksText[i - 1];
        }
        generateTask3();
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
    }

    private void generateTask1()
    {
        GameObject go = GameObject.Find("Task1Panel");
        int k = 6;
        int u, v;
        u = UnityEngine.Random.Range(1, graph.V + 1);
        v = UnityEngine.Random.Range(1, graph.V + 1);
        go.GetComponentInChildren<Text>().text = "„и Ї ребро м≥ж вершинами " + u + " та " + v + "?";
        go.GetComponentInChildren<Dropdown>().value = 1;
        go.GetComponentInChildren<Dropdown>().value = 0;
        task1Answers.Add(graph.containsEdge(u, v));
        task1.Add(go.GetComponentInChildren<Dropdown>());
        for(int i = 1; i < k; i++)
        {
            Text txt = Instantiate(go.GetComponentInChildren<Text>(), go.GetComponent<Transform>());
            Dropdown inp = Instantiate(go.GetComponentInChildren<Dropdown>(),  go.GetComponent<Transform>());
            inp.transform.localPosition = new Vector3(
                inp.transform.localPosition.x,
                inp.transform.localPosition.y - 60.0f * i,
                inp.transform.localPosition.z);
            txt.transform.localPosition = new Vector3(
                txt.transform.localPosition.x,
                txt.transform.localPosition.y - 60.0f * i,
                txt.transform.localPosition.z);
            inp.tag = "Destroy";
            txt.tag = "Destroy";
            u = UnityEngine.Random.Range(1, graph.V + 1);
            v = UnityEngine.Random.Range(1, graph.V + 1);
            txt.text = "„и Ї ребро м≥ж вершинами " + u + " та " + v + "?";
            task1Answers.Add(graph.containsEdge(u, v));
            task1.Add(inp);
            task1Texts.Add(txt);
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
        GameObject go = GameObject.Find("Task3Panel");
        TMP_Text text= go.GetComponentInChildren<TMP_Text>();
        int k = UnityEngine.Random.Range(1, 10);
        text.text = String.Empty;
        Graph gr = GraphGenerator.generateRandom(k, false);
        task3Res = 0;

        for (int i = 0; i < k; i++)
        {
            int d = gr.deg(i + 1);

            task3Res += d;
            if (i == k - 1 && task3Res % 2 == 1)
            {
                task3Res--;
                d--;
            }

            text.text = text.text + "deg(" + (i + 1) + ") = " + d + "\n";                        
        }

    }

    public void HandleTask1CompleteButtonClickEvent()
    {
        bool done = true;
        for (int i = 0; i < task1.Count; i++)
        {
            Dropdown field = task1[i];
            int k = 2;
            if (task1Answers[i]) k = 1;
            if (k != field.value)
            {
                done = false;
            }
        }
        if (done)
        {
            Results.appendResults(1, 1, 1);
            correctResponce();
        }
        else
        {
            incorrectResponce();
        }
        for (int i = 0; i < task1.Count; i++)
        {
            Dropdown field = task1[i];
            field.value = 0;
        }
        unityEvents[EventName.ReloadGraph].Invoke(0);
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
            correctResponce();
        }
        else
        {
            incorrectResponce();
        }
        for (int i = 0; i < task2.Count; i++)
        {
            InputField field = task2[i];
            field.text = "";
        }
        unityEvents[EventName.ReloadGraph].Invoke(0);
    }

    public void HandleTask3CompleteButtonClickEvent()
    {
        GameObject go = GameObject.Find("Task3Panel");
        InputField inp = go.GetComponentInChildren<InputField>();
        int res = -1;
        int.TryParse(inp.text, out res);
        if (res == task3Res / 2)
        {
            Results.appendResults(1, 1, 3);
            correctResponce();
        }
        else
        {
            incorrectResponce();
        }

        inp.text = String.Empty;
        generateTask3();
    }


    private void Update()
    {
        if (timer != null && timer.Finished)
        {
            if(rgo != null)
                Destroy(rgo);
        }
    }

    private void correctResponce()
    {
        rgo = GameObject.Instantiate(correct, GameObject.Find("Canvas").GetComponent<Transform>());
        rgo.transform.localPosition = new Vector3(
            GameObject.Find("Canvas").GetComponent<RectTransform>().rect.center.x,
            GameObject.Find("Canvas").GetComponent<RectTransform>().rect.center.y,
            0
            );
        timer = rgo.AddComponent<Timer>();
        timer.Duration = 3;
        timer.Run();
    }

    private void incorrectResponce()
    {
        rgo = GameObject.Instantiate(incorrect, GameObject.Find("Canvas").GetComponent<Transform>());
        rgo.transform.localPosition = new Vector3(
            GameObject.Find("Canvas").GetComponent<RectTransform>().rect.center.x,
            GameObject.Find("Canvas").GetComponent<RectTransform>().rect.center.y,
            0
            );
        timer = rgo.AddComponent<Timer>();
        timer.Duration = 3;
        timer.Run();
    }

}
