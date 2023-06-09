using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public static class CSVProcessor
{
   public static List<string> ReadCSV(string name, int amount)
    {
        List<string> list = new List<string>();
        StreamReader input = null;
        String filePath = Path.Combine(Application.streamingAssetsPath + "/", name);

#if UNITY_EDITOR || UNITY_IOS
        input = new StreamReader(filePath);
        for (int i = 0; i < amount; i++)
        {
            list.Add(input.ReadLine());
        }

#elif UNITY_ANDROID
        WWW reader = new WWW(filePath);
        while (!reader.isDone)
        {
        }
        foreach(string s in reader.text.Split('\n'))
        {
            list.Add((string)s);
        }
#endif
        if (list.Count < amount)
        {
            for (int i = list.Count; i < amount; i++)
            {
                list.Add("Level " + i);
            } 
        }
        return list;
    }

    public static void WriteCSV(string name, List<string> lines)
    {
        StreamWriter output = null;
        String filePath = Path.Combine(Application.streamingAssetsPath + "/", name);

#if UNITY_EDITOR || UNITY_IOS
        output = new StreamWriter(filePath);
        foreach(string line in lines)
        {
            output.WriteLine(line);
        }

#elif UNITY_ANDROID        
        File.WriteAllText(filePath, String.Join("\n", lines));
#endif
    }

    public static List<int> convertCSV(string csvText)
    {
        List<int> list = new List<int>();
        if (!String.IsNullOrEmpty(csvText))
        {
            foreach (string s in csvText.Split(','))
            {
                list.Add(int.Parse(s));
            }
        }
        return list;
    }
}
