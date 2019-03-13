using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropSpawnPoints))]
public class PropSpawnPointsInspector : Editor
{
    private PropSpawnPoints selectedPropSpawnPoints;

    private void OnEnable()
    {
        selectedPropSpawnPoints = target as PropSpawnPoints;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save as new propSpawnPointsData"))
        {
            PropSpawnPointsData propSpawnPointsData = ScriptableObject.CreateInstance<PropSpawnPointsData>();

            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();
            for (int i = 0; i < selectedPropSpawnPoints.transform.childCount; i++)
            {
                points.Add(selectedPropSpawnPoints.transform.GetChild(i).position);
                rotations.Add(selectedPropSpawnPoints.transform.GetChild(i).rotation);
            }


            propSpawnPointsData.propSpawnPositions = points;
            propSpawnPointsData.propSpawnRotations = rotations;
            AssetDatabase.CreateAsset(propSpawnPointsData,  AssetDatabase.GenerateUniqueAssetPath("Assets/Balancing/PropSpawnPoints/PropSpawnPointsData.asset"));
            AssetDatabase.SaveAssets();
        }


    }
}
