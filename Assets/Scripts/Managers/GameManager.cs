using System;
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
    public GameObject skinSelectionPrefab;

    [NonSerialized] public bool gameFinished = false;

    private float travelTime = 1;
    private Vector3 cameraInitialPosition;
    private Quaternion cameraInitialRotation;
    private bool movingToCat = false;
    private float currentTime;
    
    

    [System.NonSerialized] public bool vibrationsEnabled=false;


    private void Start()
    {
        movingToCat = false;
        DontDestroyOnLoad(gameObject);

        Instantiate(catManagerPrefab,self);
        Instantiate(propManagerPrefab, self);
        Instantiate(inputManagerPrefab, self);
        Instantiate(humanManagerPrefab, self);
        Instantiate(uiManagerPrefab, self);
        Instantiate(levelManagerPrefab, self);
        Instantiate(skinSelectionPrefab, self);

        LevelManager.instance.currentLevel = PlayerPrefs.GetInt("level");
        vibrationsEnabled = PlayerPrefs.GetInt("vibrations")==0?true:false;



        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(sceneToLoad);
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level", LevelManager.instance.currentLevel);
        PlayerPrefs.SetInt("vibrations", vibrationsEnabled ? 0 : 1);
    }


    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
        
        UIManager.instance.Init();

        UIManager.instance.DisplayPanel(UIPanel.MainMenu);

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    public void NextLevel()
    {
        
        LevelManager.instance.NextLevel();
        LevelManager.instance.LoadCurrentLevel();
        UIManager.instance.menu.winPanel.SetActive(false);
        UIManager.instance.DisplayPanel(UIPanel.IngameOverlay);
    }


    private void StartGame()
    {
        LevelManager.instance.LoadCurrentLevel();
    }



    private void GameOver(bool victory)
    {
        if (gameFinished) { return; }

        if (victory)
        {
            
            UIManager.instance.menu.ingameOverlay.SetActive(false);
            InputManager.instance.Enable(false);
            CatBehaviour.instance.animator.SetTrigger("LevelWon");
            CameraGoToCat();
        }
        else
        {
            UIManager.instance.DisplayPanel(UIPanel.Lose);
            InputManager.instance.Enable(false);
            UIManager.instance.menu.ingameOverlay.SetActive(false);
        }
        HumanManager.instance.StopHumans();
        CatBehaviour.instance.animator.SetBool("IsMoving", false);
        gameFinished = true;
    }


    
    public void Retry()
    {
        LevelManager.instance.LoadCurrentLevel();
        UIManager.instance.DisplayPanel(UIPanel.IngameOverlay);
        UIManager.instance.menu.pausePanel.SetActive(false);
        UIManager.instance.menu.winPanel.SetActive(false);
        UIManager.instance.menu.losePanel.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void Pause(bool on)
    {
        InputManager.instance.Enable(!on);
        if (on)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        UIManager.instance.menu.pausePanel.SetActive(on);
        UIManager.instance.menu.ingameOverlay.SetActive(!on);

    }


    public void GoBackToMenu()
    {
        UIManager.instance.HideActivePanel();
        Time.timeScale = 1;
        UIManager.instance.menu.paused = false;
        HumanManager.instance.StopHumans();
        CatBehaviour.instance.animator.SetBool("IsMoving", false);
        UIManager.instance.menu.winPanel.SetActive(false);
        UIManager.instance.menu.losePanel.SetActive(false);
        UIManager.instance.menu.pausePanel.SetActive(false);
        UIManager.instance.DisplayPanel(UIPanel.MainMenu);
    }

    public void CheckGameOver()
    {
        float propsNumber = LevelManager.instance.GetCurrentLevel().propSpawnPointsData.propSpawnPositions.Count;
        float destructionPercentRequired = (LevelManager.instance.GetCurrentLevel().destructionPercentageRequired / 100);
        print("percent of objective : " + PropManager.instance.propsPushed / propsNumber);

        if (PropManager.instance.propsCatched / propsNumber > 1 - destructionPercentRequired)
        {
            GameOver(false);
        }
        else if (PropManager.instance.propsPushed / propsNumber >= destructionPercentRequired)
        {
            GameOver(true);
        }

        if ((PropManager.instance.activeProps.Count+ PropManager.instance.propsPushed )/ propsNumber < destructionPercentRequired )
        {
            GameOver(false);
        }
    }



    public void CameraGoToCat()
    {
        cameraInitialPosition = Camera.main.transform.position;
        currentTime = 0;
        movingToCat = true;
    }


    private void Update()
    {
        if (!movingToCat) { return; }
        currentTime += Time.deltaTime;
        float percent = currentTime / travelTime;
        if (percent < 1)
        {
            Camera.main.transform.position=Vector3.Lerp(cameraInitialPosition, CatBehaviour.instance.cameraSpot.position, percent);
            Camera.main.transform.rotation=Quaternion.Lerp(cameraInitialRotation, CatBehaviour.instance.cameraSpot.rotation, percent);
        }
        else
        {
            Camera.main.transform.position = CatBehaviour.instance.cameraSpot.position;
            Camera.main.transform.rotation = CatBehaviour.instance.cameraSpot.rotation;
            movingToCat = false;
            StartCoroutine(DisplayWinPanel());
        }
    }

    private IEnumerator DisplayWinPanel()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.DisplayPanel(UIPanel.Win);
    }

}
