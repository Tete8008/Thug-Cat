﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;

    [System.NonSerialized] public int currentLevel;

    public void LoadCurrentLevel()
    {

        Level level = GetCurrentLevel();
        if (SceneManager.GetActiveScene().name != level.sceneName)
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            InputManager.instance.Enable(false);
            SceneManager.LoadScene(level.sceneName);
        }
        else
        {
            PropManager.instance.Init(level.propSpawnPointsData);
            HumanManager.instance.Init(level.hoomanLayers);
            CatManager.instance.Init();
            InputManager.instance.Enable(true);
            GameManager.instance.gameFinished = false;
        }


        UIManager.instance.RefreshLevelText();
        

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        LoadCurrentLevel();
        UIManager.instance.Init();
        UIManager.instance.menu.mainMenuPanel.SetActive(false);
        UIManager.instance.DisplayPanel(UIPanel.IngameOverlay);
    }

    public Level GetCurrentLevel()
    {
        return levels[currentLevel];
    }

    public void NextLevel()
    {
        if (currentLevel < levels.Count - 1)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 0;
        }
        UIManager.instance.RefreshLevelText();
    }
}
