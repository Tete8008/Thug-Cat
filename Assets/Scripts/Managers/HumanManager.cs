using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : Singleton<HumanManager>
{
    public List<GameObject> humanPrefabs;
    private List<HumanBehaviour> activeHumans;

    public int humanCount;


    public void Init(List<HoomanLayer> hoomanLayers)
    {
        activeHumans = new List<HumanBehaviour>();

        for (int i = 0; i < hoomanLayers.Count; i++)
        {
            for (int j = 0; j < hoomanLayers[i].humanCount; j++)
            {
                SpawnRandomHuman(hoomanLayers[i].distanceFromTable);
            }
        }
    }

    public void SpawnHuman(GameObject go,float distance)
    {
        HumanBehaviour human = Instantiate(go).GetComponent<HumanBehaviour>();
        human.Init(distance);
        activeHumans.Add(human);
    }

    public void SpawnRandomHuman(float distance)
    {
        if (humanPrefabs.Count > 0)
        {
            int index = Random.Range(0, humanPrefabs.Count);
            SpawnHuman(humanPrefabs[index],distance);
        }
        else
        {
            Debug.LogWarning("no human datas linked");
            return;
        }
    }



}
