using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject canvasPrefab;

    [System.NonSerialized] public MenuBehaviour menu;

    public void Init()
    {
        menu = Instantiate(canvasPrefab).GetComponent<MenuBehaviour>();
    }

    public void RefreshPropsPushedCount()
    {
        menu.propsPushedText.text = "Props pushed : "+PropManager.instance.propsPushed.ToString();
    }

    public void DisplayLosePanel()
    {
        menu.losePanel.SetActive(true);
    }


}
