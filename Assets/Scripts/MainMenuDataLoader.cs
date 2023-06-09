using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDataLoader : MonoBehaviour
{
    List<Text> levelNames = new List<Text>();
    List<Text> levelDescriptions = new List<Text>();


    // Start is called before the first frame update
    void Start()
    {
        List<string> list = CSVProcessor.ReadCSV("LevelNames.csv", 9);
        List<string> amountOfStages = CSVProcessor.ReadCSV("LevelStagesInfo.csv", 9);
        GetLevelsHeadings();
        for (int i = 0; i < levelNames.Count; i++)
        {
            Text level = levelNames[i];
            Text description = levelDescriptions[i];
            if (level != null)
            {
               level.text = list[i];
            }
            if(description != null)
            {
                description.text = amountOfStages[i];
            }
        }
        
    }

    void GetLevelsHeadings()
    {
        for (int i = 0; i < 9; ++i)
        {
            foreach (Text text in GameObject.Find("LevelButton" + i).GetComponentsInChildren<Text>())
            {
                if (text.tag == "LevelHeading") levelNames.Add(text);
                if (text.tag == "LevelInfo") levelDescriptions.Add(text);
            }
        }
    }

}
