using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPath : ScriptableObject
{
    public string pathName;
    public List<Vector2> points;
    public List<Vector2> bezierHandleBeforePoints;
    public List<Vector2> bezierHandleAfterPoints;
    public int bezierDivisions;


}
