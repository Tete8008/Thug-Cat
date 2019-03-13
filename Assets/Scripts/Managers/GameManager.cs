using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string sceneToLoad = "Game";

    public Transform self;


    public GameObject catManagerPrefab;
    public GameObject propManagerPrefab;
    public GameObject inputManagerPrefab;
    public GameObject humanManagerPrefab;
    public GameObject uiManagerPrefab;
    public GameObject levelManagerPrefab;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Instantiate(catManagerPrefab,self);
        Instantiate(propManagerPrefab, self);
        Instantiate(inputManagerPrefab, self);
        Instantiate(humanManagerPrefab, self);
        Instantiate(uiManagerPrefab, self);
        Instantiate(levelManagerPrefab, self);

        LevelManager.instance.currentLevel = PlayerPrefs.GetInt("level");



        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(sceneToLoad);
    }


    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
        
        UIManager.instance.Init();

        UIManager.instance.DisplayPanel(UIPanel.MainMenu);

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }


    private void StartGame()
    {
        LevelManager.instance.LoadCurrentLevel();
    }



    public void GameOver(bool victory)
    {
        if (victory)
        {
            Debug.Log("YOU WIN");
        }
        else
        {
            Debug.Log("YOU LOSE");
        }
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level",LevelManager.instance.currentLevel);
    }
}
