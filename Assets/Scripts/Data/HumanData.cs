using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HumanData",menuName ="ThugCat/HumanData",order =1)]
public class HumanData : ScriptableObject
{
    public Vector2 spawnDelay;
    public Vector2 presenceDuration;
    public float timeToShowUpAfterWarning;
}
