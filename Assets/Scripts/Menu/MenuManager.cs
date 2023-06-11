using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuNames name) 
    {
        switch (name)
        {
            case MenuNames.MainMenu:
                {
                    SceneManager.LoadScene("MainMenu");
                    break;
                }
            case MenuNames.SideMenu:
                {
                    Object.Instantiate(Resources.Load("SideMenu"));
                    break;
                }
            case MenuNames.Level1Menu:
                {
                    SceneManager.LoadScene("Level1Menu");
                    break;
                }
            case MenuNames.Level1Stage1:
                {
                    SceneManager.LoadScene("Level1Stage1");
                    break;
                }
            case MenuNames.Level2Menu:
                {
                    SceneManager.LoadScene("Level2Menu");
                    break;
                }
            case MenuNames.Level3Menu:
                {
                    SceneManager.LoadScene("Level3Menu");
                    break;
                }
            case MenuNames.Level4Menu:
                {
                    SceneManager.LoadScene("Level4Menu");
                    break;
                }
            case MenuNames.Level5Menu:
                {
                    SceneManager.LoadScene("Level5Menu");
                    break;
                }
            case MenuNames.Level6Menu:
                {
                    SceneManager.LoadScene("Level6Menu");
                    break;
                }
            case MenuNames.Level6Stage3:
                {
                    SceneManager.LoadScene("Level6Stage3");
                    break;
                }
            case MenuNames.Level7Menu:
                {
                    SceneManager.LoadScene("Level7Menu");
                    break;
                }
            case MenuNames.Level8Menu:
                {
                    SceneManager.LoadScene("Level8Menu");
                    break;
                }
            case MenuNames.Level9Menu:
                {
                    SceneManager.LoadScene("Level9Menu");
                    break;
                }
        }
    }
}
