using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : Singleton<HumanManager>
{
    public List<GameObject> humanPrefabs;
    private List<HumanBehaviour> activeHumans;

    public int humanCount;


    public void Init()
    {
        activeHumans = new List<HumanBehaviour>();
        for (int i = 0; i < humanCount; i++)
        {
            SpawnRandomHuman();
        }
        
    }

    public void SpawnHuman(GameObject go)
    {
        HumanBehaviour human = Instantiate(go).GetComponent<HumanBehaviour>();
        activeHumans.Add(human);
    }

    public void SpawnRandomHuman()
    {
        if (humanPrefabs.Count > 0)
        {
            int index = Random.Range(0, humanPrefabs.Count);
            SpawnHuman(humanPrefabs[index]);
        }
        else
        {
            Debug.LogWarning("no human datas linked");
            return;
        }
    }



}
