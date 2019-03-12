using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : Singleton<HumanManager>
{
    public List<GameObject> humanPrefabs;
    private List<HumanBehaviour> activeHumans;

    public int humanCount;

    public List<HoomanLayer> hoomanLayers;


    public void Init()
    {
        activeHumans = new List<HumanBehaviour>();

        for (int i = 0; i < hoomanLayers.Count; i++)
        {
            for (int j = 0; j < hoomanLayers[i].humanCount; j++)
            {
                SpawnRandomHuman(hoomanLayers[i].humanPath);
            }
        }
    }

    public void SpawnHuman(GameObject go,HumanPath humanPath)
    {
        HumanBehaviour human = Instantiate(go).GetComponent<HumanBehaviour>();
        human.Init(humanPath);
        activeHumans.Add(human);
    }

    public void SpawnRandomHuman(HumanPath humanPath)
    {
        if (humanPrefabs.Count > 0)
        {
            int index = Random.Range(0, humanPrefabs.Count);
            SpawnHuman(humanPrefabs[index],humanPath);
        }
        else
        {
            Debug.LogWarning("no human datas linked");
            return;
        }
    }



}
