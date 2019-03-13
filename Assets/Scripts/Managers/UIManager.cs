using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIPanel { MainMenu,IngameOverlay,PauseMenu,Win,Lose}

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;
    private GameObject activePanel;

    [System.NonSerialized] public MenuBehaviour menu;

    public void Init()
    {
        menu = Instantiate(canvasPrefab).GetComponent<MenuBehaviour>();
    }

    public void RefreshPropsPushedCount()
    {
        menu.propsPushedText.text = PropManager.instance.propsPushed.ToString();
    }



    public void DisplayPanel(UIPanel uIPanel)
    {
        switch (uIPanel)
        {
            case UIPanel.MainMenu:
                activePanel = menu.mainMenuPanel;
                break;
            case UIPanel.IngameOverlay:
                activePanel = menu.ingameOverlay;
                break;
            case UIPanel.PauseMenu:
                activePanel = menu.pausePanel;
                break;
            case UIPanel.Win:
                activePanel = menu.winPanel;
                break;
            case UIPanel.Lose:
                activePanel = menu.losePanel;
                break;
        }

        activePanel.SetActive(true);
    }


    public void HideActivePanel()
    {
        activePanel.SetActive(false);
    }

}
