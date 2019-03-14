using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : Singleton<CatManager>
{
    public GameObject catPrefab;


    public Vector3 catSpawnPosition;
    public Vector3 catSpawnRotation;

    [System.NonSerialized] public CatBehaviour cat;


    public void Init()
    {
        SpawnCat();
    }

    public void SpawnCat()
    {
        if (cat != null)
        {
            Destroy(cat.gameObject);
        }
        cat = Instantiate(catPrefab,catSpawnPosition,Quaternion.Euler(catSpawnRotation)).GetComponent<CatBehaviour>();
        cat.Init();
    }
}
