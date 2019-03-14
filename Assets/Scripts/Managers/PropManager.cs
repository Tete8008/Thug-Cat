using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : Singleton<PropManager>
{

    [System.NonSerialized] public int propsPushed =0;

    //private bool pushing;

    public List<GameObject> propPrefabs;

    [System.NonSerialized] public List<PropBehaviour> activeProps;
    [System.NonSerialized] public List<GameObject> activeFragments;
    [System.NonSerialized] public int propsCatched = 0;



    public void Init(PropSpawnPointsData propSpawnPointsData)
    {
        if (activeProps != null)
        {
            for (int i = 0; i < activeProps.Count; i++)
            {
                Destroy(activeProps[i].gameObject);
            }
        }

        if (activeFragments != null)
        {
            for (int i = 0; i < activeFragments.Count; i++)
            {
                Destroy(activeFragments[i]);
            }
        }

        activeProps=new List<PropBehaviour>();
        activeFragments = new List<GameObject>();

        for (int i = 0; i < propSpawnPointsData.propSpawnPositions.Count; i++)
        {
            SpawnRandomProp(propSpawnPointsData.propSpawnPositions[i],propSpawnPointsData.propSpawnRotations[i]);
        }

        propsPushed = 0;
        propsCatched = 0;
        UIManager.instance.RefreshPropsPushedCount();
        UIManager.instance.RefreshPropsCatchedCount();

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
        activeProps.Add(Instantiate(go, position, go.transform.rotation).GetComponent<PropBehaviour>());
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
        activeFragments.Add(fragments);

        Destroy(prop.gameObject);
        //SpawnRandomProp();
        GameManager.instance.CheckGameOver();
    }
}
