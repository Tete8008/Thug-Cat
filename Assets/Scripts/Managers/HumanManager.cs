using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : Singleton<HumanManager>
{
    public GameObject humanPrefab;

    public List<Vector3> humanSpawnPoints;

    [System.NonSerialized] public int humansPresentCount;

    public int humanMaxCountAtSameTime;

    public List<HumanData> humanDatas;


    private List<HumanBehaviour> activeHumans;


    public void Init()
    {
        humansPresentCount = 0;
        activeHumans = new List<HumanBehaviour>();
        SpawnRandomHuman();
    }

    public void SpawnHuman(HumanData humanData)
    {
        HumanBehaviour human = Instantiate(humanPrefab).GetComponent<HumanBehaviour>();
        human.self.position = humanSpawnPoints[Random.Range(0, humanSpawnPoints.Count)];
        human.Init(humanData);
        activeHumans.Add(human);
    }

    public void SpawnRandomHuman()
    {
        if (humanDatas.Count > 0)
        {
            int index = Random.Range(0, humanDatas.Count);
            SpawnHuman(humanDatas[index]);
        }
        else
        {
            Debug.LogWarning("no human datas linked");
            return;
        }
    }



}
