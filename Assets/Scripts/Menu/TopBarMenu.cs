using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarMenu : MonoBehaviour
{
    public void HandleSideMenuOpenButtonClickEvent()
    {
        MenuManager.GoToMenu(MenuNames.SideMenu);
    }
}
