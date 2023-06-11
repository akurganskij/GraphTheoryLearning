using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Menu : MonoBehaviour
{
    public void HandleStage3ButtonClickEvent()
    {
        MenuManager.GoToMenu(MenuNames.Level6Stage3);
    }
}
