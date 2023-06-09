using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Results
{
    private static List<int> stagesAmount = new List<int>();
    private static Dictionary<KeyValuePair<int, int>, List<int>> results = new Dictionary<KeyValuePair<int, int>, List<int>>();
    private static Dictionary<KeyValuePair<int, int>, List<int>> maximumNeeded = new Dictionary<KeyValuePair<int, int>, List<int>>();
    private static bool isLoaded = false, dataLoaded = false, levelInfoLoaded = false;
    
    public static void loadResults(int level, int stage)
    {
        if (!levelInfoLoaded)
        {
            var t = CSVProcessor.ReadCSV("AmountToComplete.csv", 9);
            foreach(string s in t)
            {
                stagesAmount.Add(int.Parse(s));
            }
            levelInfoLoaded = true;
        }

        string key = "Level" + level + "Stage" + stage + "Results";
        if (PlayerPrefs.HasKey(key)) {
            List<int> list = new List<int>();
            string p = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(p))
            {
                foreach (string s in p.Split(','))
                {
                    list.Add(int.Parse(s));
                }
                results.Add(new KeyValuePair<int, int>(level, stage), list);
            }
        }
        

        if (!dataLoaded)
        {
            int k = 0;
            var t = CSVProcessor.ReadCSV("AmountToCompleteInStage.csv", 50);
            for (int i = 0; i < stagesAmount.Count; i++)
            {
                for (int j = 0; j < stagesAmount[i]; j++)
                {
                    List<int> list = new List<int>();
                    if (t[k] != null)
                    {
                        foreach (string s in t[k].Split(','))
                        {
                            if (string.IsNullOrEmpty(s)) list.Add(0);
                            else list.Add(int.Parse(s));
                        }
                        maximumNeeded.Add(new KeyValuePair<int, int>(i + 1, j + 1), list);
                    }
                    else
                    {
                        maximumNeeded.Add(new KeyValuePair<int, int>(i + 1, j + 1), new List<int>() {0});
                    }
                    k++;
                }
            }
            dataLoaded = true;
        }
        isLoaded = true;
    }

    public static void saveResults(int level, int stage)
    {
        string key = "Level" + level + "Stage" + stage + "Results";
        PlayerPrefs.SetString(key, string.Join(",", results[new KeyValuePair<int, int>(level, stage)]));
        PlayerPrefs.Save();
    }
    public static void appendResults(int level, int stage, int taskNum)
    {
        if(!dataLoaded)loadResults(level, stage);
        var t1 = results;
        var t2 = maximumNeeded;
        if (results.ContainsKey(new KeyValuePair<int, int>(level, stage)))
        {
            int qurrent = results[new KeyValuePair<int, int>(level, stage)][taskNum - 1];
            int max = maximumNeeded[new KeyValuePair<int, int>(level, stage)][taskNum - 1];
            if (qurrent < max)
            {
                results[new KeyValuePair<int, int>(level, stage)][taskNum - 1]++;
            }
        }
        else
        {
            var temp = maximumNeeded[new KeyValuePair<int, int>(level, stage)];
            for (int i = 0; i < temp.Count; i++) temp[i] = 0;
            temp[taskNum - 1]++;
            results[new KeyValuePair<int, int>(level, stage)] = temp;
        }
        saveResults(level, stage);

    }
    public static List<int> amountToComplete(int level, int stage)
    {
        if(!dataLoaded) loadResults(level, stage);
        List<int> amount = new List<int>();
        maximumNeeded.TryGetValue(new KeyValuePair<int, int>(level, stage), out amount);
        return amount;
    }
}
