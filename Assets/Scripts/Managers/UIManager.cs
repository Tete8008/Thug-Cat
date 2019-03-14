﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIPanel { MainMenu,IngameOverlay,PauseMenu,Win,Lose}

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;

    public Color optionCheckedColor;
    private GameObject activePanel;

    [System.NonSerialized] public MenuBehaviour menu;

    public void Init()
    {
        menu = Instantiate(canvasPrefab).GetComponent<MenuBehaviour>();
    }

    public void RefreshPropsPushedCount()
    {
        float propsNumber = LevelManager.instance.GetCurrentLevel().propSpawnPointsData.propSpawnPositions.Count;
        menu.propsPushedText.text = (PropManager.instance.propsPushed/propsNumber/(LevelManager.instance.GetCurrentLevel().destructionPercentageRequired/100)*100)+" %";
    }

    public void RefreshPropsCatchedCount()
    {
        float propsNumber = LevelManager.instance.GetCurrentLevel().propSpawnPointsData.propSpawnPositions.Count;
        menu.propsCatchedText.text = PropManager.instance.propsCatched / propsNumber / (1- LevelManager.instance.GetCurrentLevel().destructionPercentageRequired/100 )*100+ " %";
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


    public void ToggleOptions(bool on)
    {
        menu.optionsFrame.SetActive(on);
    }

    public void ToggleVibrations(bool on)
    {
        GameManager.instance.vibrationsEnabled = on;
        if (on)
        {
            menu.vibrationsButton.image.color = optionCheckedColor;
        }
        else
        {
            menu.vibrationsButton.image.color = Color.white;
        }
    }


}
