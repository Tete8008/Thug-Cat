using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnPointsData : ScriptableObject
{
    public List<Vector3> propSpawnPositions;
    public List<Quaternion> propSpawnRotations;
    [Range(0, 100)]
    public float destructionPercentageRequired;

}
