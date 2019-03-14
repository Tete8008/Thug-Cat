using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;

    [System.NonSerialized] public int currentLevel;

    public void LoadCurrentLevel()
    {
        Level level = GetCurrentLevel();
        PropManager.instance.Init(level.propSpawnPointsData);
        HumanManager.instance.Init(level.hoomanLayers);
        CatManager.instance.Init();
        InputManager.instance.Enable(true);
        GameManager.instance.gameFinished = false;

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
        
    }
}
