using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI propsPushedText;
    public GameObject losePanel;

    public GameObject winPanel;
    public GameObject mainMenuPanel;
    public GameObject optionsFrame;
    public GameObject pausePanel;
    public GameObject ingameOverlay;


    public void Play()
    {
        UIManager.instance.HideActivePanel();
        UIManager.instance.DisplayPanel(UIPanel.IngameOverlay);
        LevelManager.instance.LoadCurrentLevel();

    }

}
