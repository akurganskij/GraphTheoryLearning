using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        EventManager.Initialize();
    }

    public void HandleLevelOpenButtonClickEvent(int level)
    {
        MenuNames menuNames = MenuNames.Level1Menu;
        switch (level) 
        {
            case 1: menuNames = MenuNames.Level1Menu; break;
            case 2: menuNames = MenuNames.Level2Menu; break;
            case 3: menuNames = MenuNames.Level3Menu; break;
            case 4: menuNames = MenuNames.Level4Menu; break;
            case 5: menuNames = MenuNames.Level5Menu; break;
            case 6: menuNames = MenuNames.Level6Menu; break;
            case 7: menuNames = MenuNames.Level7Menu; break;
            case 8: menuNames = MenuNames.Level8Menu; break;
            case 9: menuNames = MenuNames.Level9Menu; break;
        }
        MenuManager.GoToMenu(menuNames);
    }

}
