using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuDataLoader : MonoBehaviour
{
    List<Text> buttonTexts = new List<Text>();


    // Start is called before the first frame update
    void Start()
    {
        List<string> list = CSVProcessor.ReadCSV("SideMenuOptions.csv", 5);
        GetButtonTexts();
        for (int i = 0; i < buttonTexts.Count; i++)
        {
            Text level = buttonTexts[i];
            if (level != null)
            {
                level.text = list[i];
            }
        }

    }

    void GetButtonTexts()
    {
        for (int i = 0; i < 5; ++i)
        {
            foreach (Text text in GameObject.Find("Button" + i).GetComponentsInChildren<Text>())
            {
                buttonTexts.Add(text);
            }
        }
    }
}
