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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Instantiate(catManagerPrefab,self);
        Instantiate(propManagerPrefab, self);
        Instantiate(inputManagerPrefab, self);
        Instantiate(humanManagerPrefab, self);
        Instantiate(uiManagerPrefab, self);


        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(sceneToLoad);
    }


    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        CatManager.instance.Init();
        PropManager.instance.Init();
        UIManager.instance.Init();
        HumanManager.instance.Init();
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
