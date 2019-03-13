using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PropsPointsTool : EditorWindow
{
    private PropManager propManagerPrefab;

    private Texture propIcon;
    private Texture catBackground;

    private GUIStyle centeredTextStyle;

    private List<Vector3> propPoints;


    //[MenuItem("ThugCat/PropsPointsTool")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PropsPointsTool window = (PropsPointsTool)EditorWindow.GetWindow(typeof(PropsPointsTool));
        window.Show();
    }


    private void OnEnable()
    {
        propManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/PropManager.prefab").GetComponent<PropManager>();
        catBackground = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/CatBackground.jpg");



        if (propManagerPrefab == null) { Debug.LogWarning("PropManager prefab not found. Make sure it is located under Assets/Prefabs/Managers/"); SceneView.onSceneGUIDelegate -= OnSceneGUIDelegate; }


        SceneView.onSceneGUIDelegate += OnSceneGUIDelegate;
        propIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/PropIcon.png");


        centeredTextStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter };
        //propPoints = propManagerPrefab.propSpawnPoints;
        if (propPoints == null)
        {
            propPoints = new List<Vector3>();
        }
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUIDelegate;
    }



    private void OnSceneGUIDelegate(SceneView sceneView)
    {
        Handles.BeginGUI();

        bool saveNeeded = false;

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        if (GUILayout.Button("+"))
        {
            propPoints.Add(new Vector3());
            saveNeeded = true;
        }

        if (GUILayout.Button("-"))
        {
            /*if (propManagerPrefab.propSpawnPoints.Count > 0)
            {
                propPoints.RemoveAt(propPoints.Count - 1);
                saveNeeded = true;
            }*/
        }

        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < propPoints.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            propPoints[i] = Handles.DoPositionHandle(propPoints[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                saveNeeded = true;
            }
            //Handles.Label(propManagerPrefab.propSpawnPoints[i], propIcon);
        }
        if (saveNeeded)
        {
            EditorUtility.SetDirty(propManagerPrefab);
        }
        
        Handles.EndGUI();
        Repaint();
    }

    public void OnGUI()
    {
        EditorGUILayout.LabelField("Prop spawn points : ");
        EditorGUI.indentLevel++;
        for (int i = 0; i < propPoints.Count; i++)
        {
            propPoints[i]=EditorGUILayout.Vector3Field("_",propPoints[i]);
        }

        
    }


    



}
