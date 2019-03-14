using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : Singleton<MenuBehaviour>
{
    public GameObject losePanel;

    public GameObject winPanel;
    public GameObject mainMenuPanel;
    public GameObject optionsFrame;
    public GameObject pausePanel;
    public GameObject ingameOverlay;
    public GameObject skinSelectionPanel;
    public Slider progressionSlider;

    public TextMeshProUGUI levelText;


    public Button vibrationsButton;


    private bool optionsOpen;
    [System.NonSerialized] public  bool paused;
    private bool vibrationsEnabled;




    public void Play()
    {
        UIManager.instance.HideActivePanel();
        UIManager.instance.DisplayPanel(UIPanel.IngameOverlay);
        LevelManager.instance.LoadCurrentLevel();
        UIManager.instance.tuto.SetActive(true);
    }


    public void ToggleOptions()
    {
        optionsOpen = !optionsOpen;
        UIManager.instance.ToggleOptions(optionsOpen);
    }

    public void Quit()
    {
        GameManager.instance.Quit();
    }


    public void Pause()
    {
        paused = !paused;
        GameManager.instance.Pause(paused);
    }

    public void Retry()
    {
        GameManager.instance.Retry();
    }

    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }

    public void GoBackToMenu()
    {
        GameManager.instance.GoBackToMenu();
    }

    public void ToggleVibrations()
    {
        vibrationsEnabled = !vibrationsEnabled;

        UIManager.instance.ToggleVibrations(vibrationsEnabled);
    }

    public void GoToSkinSelection()
    {
        UIManager.instance.HideActivePanel();
        UIManager.instance.DisplayPanel(UIPanel.SkinSelection);

    }

    public void GoBackFromSkinSelection()
    {
        UIManager.instance.HideActivePanel();
        SkinSelection.instance.gameObject.SetActive(false);
        UIManager.instance.DisplayPanel(UIPanel.MainMenu);
        InputManager.instance.Enable(false);
        InputManager.instance.ToggleMode(InputMode.Game);
        SkinSelection.instance.HideSkins();

    }


    

}
