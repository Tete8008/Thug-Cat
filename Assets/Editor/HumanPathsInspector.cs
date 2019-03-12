using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HumanPaths))]
public class HumanPathsInspector : Editor
{

    private HumanPaths humanPaths;
    private int selectedHumanPathIndex;
    private string newPathName;

    private bool debugOpen;

    private List<Vector2> lastPoints;


    private void OnEnable()
    {
        humanPaths = target as HumanPaths;

        string[] guids1 = AssetDatabase.FindAssets("t:HumanPath", new[] { "Assets/Balancing/HumanPaths" });
        humanPaths.humanPaths = new List<HumanPath>();
        foreach (string guid1 in guids1)
        {
            humanPaths.humanPaths.Add(AssetDatabase.LoadAssetAtPath<HumanPath>(AssetDatabase.GUIDToAssetPath(guid1)));
        }
        
    }

    private void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        Color bgColor = GUI.backgroundColor;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        debugOpen = EditorGUILayout.Foldout(debugOpen, "Debug");
        if (debugOpen)
        {

            base.OnInspectorGUI();

        }
        EditorGUILayout.EndVertical();


        GUI.backgroundColor = new Color((float)109 / 255, (float)164 / 255, (float)250 / 255);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.backgroundColor = bgColor;
        EditorGUILayout.LabelField("→ Tool", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();


        

        string[] strings = new string[humanPaths.humanPaths.Count];
        for (int i = 0; i < humanPaths.humanPaths.Count; i++)
        {
            strings[i] = humanPaths.humanPaths[i].pathName;
        }

        selectedHumanPathIndex = EditorGUILayout.Popup("Selected path",selectedHumanPathIndex, strings);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        newPathName = EditorGUILayout.TextField("New path : ", newPathName);

        if (GUILayout.Button("add"))
        {
            if (newPathName == null)
            {
                Debug.LogWarning("path name cannot be empty");
                return;
            }

            HumanPath humanPath = ScriptableObject.CreateInstance<HumanPath>();

            string uniquePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Balancing/HumanPaths/" + newPathName + ".asset");
            AssetDatabase.CreateAsset(humanPath,uniquePath );
            humanPath.points = new List<Vector2>();
            humanPath.bezierHandleBeforePoints = new List<Vector2>();
            humanPath.bezierHandleAfterPoints = new List<Vector2>();
            string[] splits = uniquePath.Split('/');
            humanPath.pathName = splits[splits.Length - 1];
            humanPath.bezierDivisions = 2;
            humanPaths.humanPaths.Add(humanPath);

            AssetDatabase.SaveAssets();


            
        }

        

        
        EditorGUILayout.EndHorizontal();
        if (humanPaths.humanPaths.Count > selectedHumanPathIndex)
        {
            HumanPath humanPath = humanPaths.humanPaths[selectedHumanPathIndex];
            humanPath.bezierDivisions = EditorGUILayout.IntSlider("Bezier divisions", humanPath.bezierDivisions, 2, 20);
        }

        EditorGUILayout.EndVertical();

        EditorUtility.SetDirty(humanPaths);
    }

    private void OnSceneGUI()
    {
        if (humanPaths.humanPaths.Count > selectedHumanPathIndex)
        {
            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect(0, 0, 200, 40),EditorStyles.helpBox);

        

            HumanPath humanPath = humanPaths.humanPaths[selectedHumanPathIndex];

            if (GUILayout.Button("add point", EditorStyles.miniButton))
            {
                
                if (humanPath.points.Count > 0)
                {
                    humanPath.points.Add(new Vector2(humanPath.points[humanPath.points.Count - 1].x, humanPath.points[humanPath.points.Count - 1].y));
                    humanPath.bezierHandleBeforePoints.Add(new Vector2(humanPath.points[humanPath.points.Count - 1].x, humanPath.points[humanPath.points.Count - 1].y));
                    humanPath.bezierHandleAfterPoints.Add(new Vector2(humanPath.points[humanPath.points.Count - 1].x, humanPath.points[humanPath.points.Count - 1].y));
                }
                else
                {
                    humanPath.points.Add(new Vector2());
                    humanPath.bezierHandleBeforePoints.Add(new Vector2());
                    humanPath.bezierHandleAfterPoints.Add(new Vector2());
                }
                Undo.RecordObject(humanPaths,"Added path point");
                
            }
            if (GUILayout.Button("remove last point", EditorStyles.miniButton))
            {
                humanPath.points.RemoveAt(humanPath.points.Count - 1);
                humanPath.bezierHandleBeforePoints.RemoveAt(humanPath.bezierHandleBeforePoints.Count - 1);
                humanPath.bezierHandleAfterPoints.RemoveAt(humanPath.bezierHandleAfterPoints.Count - 1);
                Undo.RecordObject(humanPaths, "Removed last path point");
            }

            GUILayout.EndArea();

            Handles.EndGUI();
            for (int i = 0; i < humanPath.points.Count; i++)
            {

                Vector3 pathPoint=Handles.PositionHandle(new Vector3(humanPath.points[i].x,0, humanPath.points[i].y), Quaternion.identity);
                Handles.Label(pathPoint, (i + 1).ToString(),EditorStyles.helpBox);
                humanPath.points[i] = new Vector2(pathPoint.x, pathPoint.z);
                Vector3 handleBeforePoint = Handles.FreeMoveHandle(new Vector3(humanPath.bezierHandleBeforePoints[i].x, 0, humanPath.bezierHandleBeforePoints[i].y), Quaternion.identity, HandleUtility.GetHandleSize(new Vector3(humanPath.bezierHandleBeforePoints[i].x, 0, humanPath.bezierHandleBeforePoints[i].y)) *0.2f, Vector3.one * 0.5f, Handles.SphereHandleCap);
                humanPath.bezierHandleBeforePoints[i] = new Vector2(handleBeforePoint.x, handleBeforePoint.z);

                Vector3 handleAfterPoint=Handles.FreeMoveHandle(new Vector3(humanPath.bezierHandleAfterPoints[i].x, 0, humanPath.bezierHandleAfterPoints[i].y), Quaternion.identity, HandleUtility.GetHandleSize(new Vector3(humanPath.bezierHandleAfterPoints[i].x, 0, humanPath.bezierHandleAfterPoints[i].y))*0.2f, Vector3.one * 0.5f, Handles.SphereHandleCap);
                humanPath.bezierHandleAfterPoints[i] = new Vector2(handleAfterPoint.x, handleAfterPoint.z);


                if (humanPath.bezierDivisions < 2)
                {
                    humanPath.bezierDivisions = 2;
                }

                Color color = Handles.color;

                if (i < humanPath.points.Count - 1)
                {
                    //Handles.DrawLine(humanPath.points[i], humanPath.points[i + 1]);

                    Vector3[] bezierPoints = Handles.MakeBezierPoints(pathPoint, new Vector3(humanPath.points[i + 1].x,0, humanPath.points[i + 1].y), handleBeforePoint, handleAfterPoint,humanPath.bezierDivisions);

                    Handles.DrawPolyLine(bezierPoints);

                    Handles.color = Color.blue;
                    Handles.DrawDottedLine(pathPoint, handleBeforePoint, HandleUtility.GetHandleSize(handleBeforePoint) * 0.05f);
                    Handles.DrawDottedLine(new Vector3(humanPath.points[i + 1].x, 0, humanPath.points[i + 1].y), handleAfterPoint, HandleUtility.GetHandleSize(handleAfterPoint) * 0.05f);
                    Handles.color = color;

                }
                else
                {
                    Vector3[] bezierPoints = Handles.MakeBezierPoints(pathPoint, new Vector3(humanPath.points[0 ].x, 0, humanPath.points[0].y), handleBeforePoint, handleAfterPoint, humanPath.bezierDivisions);

                    Handles.DrawPolyLine(bezierPoints);

                    Handles.color = Color.blue;
                    Handles.DrawDottedLine(pathPoint, handleBeforePoint, HandleUtility.GetHandleSize(handleBeforePoint) * 0.05f);
                    Handles.DrawDottedLine(new Vector3(humanPath.points[0].x, 0, humanPath.points[0].y), handleAfterPoint, HandleUtility.GetHandleSize(handleAfterPoint) * 0.05f);
                    Handles.color = color;
                }
                
                

            }
        }
    }
}
