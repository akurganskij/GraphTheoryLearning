using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public static class CSVReader
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
}
