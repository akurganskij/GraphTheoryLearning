using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level1Stage1Logic : MonoBehaviour
{
    private void Start()
    {
        List<string> strings = CSVReader.ReadCSV("Level1Stage1Text.csv", 22);
        GameObject.Find("TMP").GetComponent<TMP_Text>().text = String.Join("\n", strings);
    }
}
