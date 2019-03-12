using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : Singleton<PropManager>
{

    [System.NonSerialized] public int propsPushed =0;

    [System.NonSerialized] public PropBehaviour activeProp;

    public List<Vector3> propSpawnPoints;

    //private bool pushing;

    public List<GameObject> propPrefabs;




    public void Init()
    {
        SpawnRandomProp();
    }


    public void SpawnRandomProp()
    {
        if (propPrefabs.Count > 0)
        {
            int index = Random.Range(0, propPrefabs.Count);
            SpawnProp(propPrefabs[index]);
        }
        else
        {
            Debug.LogWarning("no prop prefab linked");
            return;
        }

        
        
    }

    public void SpawnProp(GameObject go)
    {
        //activeProp=Instantiate(go, propSpawnPoint, Quaternion.identity).GetComponent<PropBehaviour>();
    }


    public void PushActiveObject()
    {
        
        CatManager.instance.cat.PushProp();
        
    }


    public void PropFallen(PropBehaviour prop)
    {
        propsPushed++;
        UIManager.instance.RefreshPropsPushedCount();
        
        GameObject fragments=Instantiate(prop.brokenProp,prop.self.position,prop.self.rotation);
        fragments.transform.localScale = prop.self.localScale;

        Destroy(prop.gameObject);
        //SpawnRandomProp();
        
    }
}
