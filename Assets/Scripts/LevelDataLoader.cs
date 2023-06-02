using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDataLoader : MonoBehaviour
{
    [SerializeField]
    int amount;

    [SerializeField]
    string fileName;

    List<Text> levelNames = new List<Text>();


    // Start is called before the first frame update
    void Start()
    {
        List<string> list = CSVReader.ReadCSV(fileName, amount);
        GetLevelsHeadings();
        for (int i = 0; i < levelNames.Count; i++)
        {
            Text level = levelNames[i];
            if (level != null)
            {
                level.text = list[i];
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

}
