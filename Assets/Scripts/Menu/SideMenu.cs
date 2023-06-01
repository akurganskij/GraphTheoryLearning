using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public void HandleBackButtonClickEvent()
    {
        Destroy(gameObject);
    }

    public void HandleToMainMenuButtonClickEvent()
    {
        MenuManager.GoToMenu(MenuNames.MainMenu);
    }

    public void HandleStandingsMenuButtonClickEvent()
    {

    }

    public void HandleDrawGraphButtonClickEvent()
    {

    }
    public void HandleQuitButtonClickEvent()
    {
        Application.Quit();
    }
}
