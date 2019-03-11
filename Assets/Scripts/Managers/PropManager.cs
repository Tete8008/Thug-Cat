using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : Singleton<PropManager>
{

    [System.NonSerialized] public int propsPushed =0;

    [System.NonSerialized] public PropBehaviour activeProp;

    public GameObject propPrefab;

    public Vector3 propSpawnPoint;

    //private bool pushing;

    public List<PropData> propDatas;





    public void Init()
    {
        SpawnRandomProp();
    }


    public void SpawnRandomProp()
    {
        if (propDatas.Count > 0)
        {
            int index = Random.Range(0, propDatas.Count);
            SpawnProp(propDatas[index]);
        }
        else
        {
            Debug.LogWarning("no prop datas linked");
            return;
        }

        
        
    }

    public void SpawnProp(PropData propData)
    {
        activeProp=Instantiate(propPrefab, propSpawnPoint, Quaternion.identity).GetComponent<PropBehaviour>();
        activeProp.Init(propData);
        
    }


    public void PushActiveObject()
    {
        
        CatManager.instance.cat.PushProp();
        if (HumanManager.instance.humansPresentCount > 0)
        {
            UIManager.instance.DisplayLosePanel();
            Debug.Log("You lose");
        }
        
    }


    public void PropFallen(PropBehaviour prop)
    {
        propsPushed++;
        UIManager.instance.RefreshPropsPushedCount();
        Destroy(prop.gameObject);
        SpawnRandomProp();
        
    }
}
