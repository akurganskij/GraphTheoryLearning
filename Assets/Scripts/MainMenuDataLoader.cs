using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDataLoader : MonoBehaviour
{
    List<Text> levelNames = new List<Text>();
    List<Text> levelDescriptions = new List<Text>();

    List<Text> levelPercentage = new List<Text>();


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
            if(PlayerPrefs.HasKey("Level" + (i+1) + "Percentage"))
            {
                float percentage = PlayerPrefs.GetFloat("Level" + (i + 1) + "Percentage");
                GameObject.Find("LevelButton" + i).GetComponentInChildren<Slider>().value = percentage;
                levelPercentage[i].text = "" + (int)(percentage * 100) + "%";
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
                else if (text.tag == "LevelInfo") levelDescriptions.Add(text);
                else levelPercentage.Add(text);

            }
        }
    }

}
