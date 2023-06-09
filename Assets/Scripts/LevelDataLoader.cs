using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDataLoader : MonoBehaviour
{
    [SerializeField]
    int levelNumber;

    [SerializeField]
    int amount;

    [SerializeField]
    string fileName;

    [SerializeField]
    string fileInfoName;

    List<Text> levelNames = new List<Text>();
    List<Text> levelInfo = new List<Text>();
    List<Text> levelPercentage = new List<Text>();



    // Start is called before the first frame update
    void Start()
    {
        List<string> list = CSVProcessor.ReadCSV(fileName, amount);
        GetLevelsHeadings();
        for (int i = 0; i < levelNames.Count; i++)
        {
            Text level = levelNames[i];
            if (level != null)
            {
                level.text = list[i];
            }
        }
        List<string> list2 = CSVProcessor.ReadCSV(fileInfoName, amount);
        GetLevelsInfoes();
        GetLevePercentage();
        for (int i = 0; i < levelInfo.Count; i++)
        {
            Text level = levelInfo[i];
            if (level != null)
            {
                level.text = list2[i];
            }
        }
        for (int i = 0; i < amount; i++) {
            List<int> amountToDo = Results.amountToComplete(levelNumber, i+1);
            string key = "Level" + levelNumber + "Stage" + (i+1) + "Results";
            if (PlayerPrefs.HasKey(key)) 
            { 
                List<int> res = CSVProcessor.convertCSV(PlayerPrefs.GetString(key));
                int sumAmount = 0, sumRes = 0;
                foreach (int item in res)
                {
                    sumRes += item;
                }
                foreach(int item in amountToDo)
                {
                    sumAmount += item;
                }
                float persentage = 0;
                if(sumRes == null) sumRes = 0;
                if (sumAmount != 0)
                    persentage = (float)sumRes / sumAmount;
                GameObject.Find("StageButton" + i).GetComponentInChildren<Slider>().value = persentage;
                levelPercentage[i].text = "" + (int)(persentage * 100) + "%";
            }
            else
            {
                GameObject.Find("StageButton" + i).GetComponentInChildren<Slider>().value = 0;
            }
        }
    }
    
    void GetLevelsHeadings()
    {
        for (int i = 0; i < amount; ++i)
        {
            foreach (Text text in GameObject.Find("StageButton" + i).GetComponentsInChildren<Text>())
            {
                if (text.tag == "StageHeading") levelNames.Add(text);
            }
        }
    }
    void GetLevelsInfoes()
    {
        for (int i = 0; i < amount; ++i)
        {
            foreach (Text text in GameObject.Find("StageButton" + i).GetComponentsInChildren<Text>())
            {
                if (text.tag == "LevelInfo") levelInfo.Add(text);
            }
        }
    }
    void GetLevePercentage()
    {
        for (int i = 0; i < amount; ++i)
        {
            foreach (Text text in GameObject.Find("StageButton" + i).GetComponentsInChildren<Text>())
            {
                if (text.tag != "LevelInfo" && text.tag != "StageHeading") levelPercentage.Add(text);
            }
        }
    }

}
