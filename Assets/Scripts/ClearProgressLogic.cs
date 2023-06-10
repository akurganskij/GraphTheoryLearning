using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearProgressLogic : MonoBehaviour
{
    public void HandleYesButtonClickEvent()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Results.clear();
        Destroy(gameObject);
    }

    public void HandleNoButtonClickEvent()
    {
        Destroy(gameObject);
    }
}
