using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : Singleton<PropManager>
{

    [System.NonSerialized] public int propsPushed =0;

    public PropSpawnPointsData propSpawnPointsData;

    //private bool pushing;

    public List<GameObject> propPrefabs;

    [System.NonSerialized] public List<PropBehaviour> activeProps;

    //private int 


    public void Init()
    {
        activeProps=new List<PropBehaviour>();
        for (int i = 0; i < propSpawnPointsData.propSpawnPositions.Count; i++)
        {
            SpawnRandomProp(propSpawnPointsData.propSpawnPositions[i],propSpawnPointsData.propSpawnRotations[i]);
        }
    }


    public void SpawnRandomProp(Vector3 position,Quaternion rotation)
    {
        if (propPrefabs.Count > 0)
        {
            int index = Random.Range(0, propPrefabs.Count);
            SpawnProp(propPrefabs[index],position,rotation);
        }
        else
        {
            Debug.LogWarning("no prop prefab linked");
            return;
        }
    }

    public void SpawnProp(GameObject go,Vector3 position,Quaternion rotation)
    {
        activeProps.Add(Instantiate(go, position, Quaternion.identity).GetComponent<PropBehaviour>());
    }


    public void PushActiveObject()
    {
        
        CatManager.instance.cat.PushProp();
        
    }


    public void PropFallen(PropBehaviour prop)
    {
        propsPushed++;
        activeProps.Remove(prop);
        UIManager.instance.RefreshPropsPushedCount();
        
        GameObject fragments=Instantiate(prop.brokenProp,prop.self.position,prop.self.rotation);
        fragments.transform.localScale = prop.self.localScale;

        Destroy(prop.gameObject);
        //SpawnRandomProp();
        
    }
}
