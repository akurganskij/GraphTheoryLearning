using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void HandleClearProgressButtonClickEvent()
    {
        Destroy(gameObject);
        Object.Instantiate(Resources.Load("ClearConfirmDialog"), GameObject.Find("Canvas").GetComponent<Transform>());
    }

    public void HandleDrawGraphButtonClickEvent()
    {
        SceneManager.LoadScene("GraphFromAdjList");
    }
    public void HandleQuitButtonClickEvent()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Destroy(gameObject);
            }
        }
    }
}
