using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName ="ThugCat/Level",order =1)]
public class Level : ScriptableObject
{
    public List<HoomanLayer> hoomanLayers;
    public PropSpawnPointsData propSpawnPointsData;
    [Range(0, 100)]
    public float destructionPercentageRequired;
    public string sceneName;
}
