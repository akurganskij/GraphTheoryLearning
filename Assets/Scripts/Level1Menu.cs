using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Menu : MonoBehaviour
{
    public void HandleStage1ButtonClickEvent()
    {
        MenuManager.GoToMenu(MenuNames.Level1Stage1);
    }
}
