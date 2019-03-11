using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawnPointsTool : EditorWindow
{
    private Vector3 catPosition;
    private Quaternion catRotation;

    private Vector3 propPosition;

    private List<Vector3> humanSpawnPoints;

    private CatManager catManagerPrefab;
    private HumanManager humanManagerPrefab;
    private PropManager propManagerPrefab;

    private Texture catTexture;
    private Texture propTexture;
    private Texture humanTexture;
    private Texture catBackground;

    private GUIStyle centeredTextStyle;

    //[MenuItem("ThugCat/SpawnPointsTool")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SpawnPointsTool window = (SpawnPointsTool)EditorWindow.GetWindow(typeof(SpawnPointsTool));
        window.Show();
        


    }

    private void OnEnable()
    {

        if (EditorSceneManager.GetActiveScene().name != "Game")
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
        }


        SceneView.onSceneGUIDelegate += onSceneGUI;
        humanManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/HumanManager.prefab").GetComponent<HumanManager>();
        catManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/CatManager.prefab").GetComponent<CatManager>();
        propManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/PropManager.prefab").GetComponent<PropManager>();
        catTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/CatPreview.png");
        propTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/PropPreview.png");
        humanTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/HumanPreview.png");
        catBackground = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/CatBackground.jpg");

        if (humanManagerPrefab == null) { Debug.LogWarning("HumanManager prefab not found. Make sure it is located under Assets/Prefabs/Managers/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }
        if (catManagerPrefab == null) { Debug.LogWarning("CatManager prefab not found. Make sure it is located under Assets/Prefabs/Managers/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }
        if (propManagerPrefab == null) { Debug.LogWarning("PropManager prefab not found. Make sure it is located under Assets/Prefabs/Managers/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }
        if (catTexture == null) { Debug.LogWarning("CatPreview.png not found. Make sure it is located under Assets/Sprites/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }
        if (propTexture == null) { Debug.LogWarning("PropPreview.png not found. Make sure it is located under Assets/Sprites/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }
        if (humanTexture == null) { Debug.LogWarning("HumanPreview.png not found. Make sure it is located under Assets/Sprites/"); SceneView.onSceneGUIDelegate -= onSceneGUI;  }

        centeredTextStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter };

        
        //humanSpawnPoints = humanManagerPrefab.humanSpawnPoints;
        catPosition = catManagerPrefab.catSpawnPosition;
        catRotation = Quaternion.Euler(catManagerPrefab.catSpawnRotation);
        //propPosition = propManagerPrefab.propSpawnPoint;



    }

    private void onSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();

        if (humanSpawnPoints == null)
        {
            humanSpawnPoints = new List<Vector3>();
        }

        EditorGUI.BeginChangeCheck();
        catPosition = Handles.PositionHandle(catPosition,catRotation);
        if (EditorGUI.EndChangeCheck())
        {
            catManagerPrefab.catSpawnPosition = catPosition;
            EditorUtility.SetDirty(catManagerPrefab);
        }
        if (catTexture != null)
        {
            Handles.Label(catPosition, catTexture);
        }
        else
        {
            Handles.Label(catPosition, "Cat",centeredTextStyle);
        }
        
        /*EditorGUI.BeginChangeCheck();
        catRotation = Handles.RotationHandle(catRotation, catPosition);
        if (EditorGUI.EndChangeCheck())
        {
            catManagerPrefab.catSpawnRotation = catRotation.eulerAngles;
            EditorUtility.SetDirty(catManagerPrefab);
        }*/

        EditorGUI.BeginChangeCheck();
        propPosition = Handles.PositionHandle(propPosition, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            //propManagerPrefab.propSpawnPoint = propPosition;
            EditorUtility.SetDirty(propManagerPrefab);
        }

        if (propTexture != null)
        {
            Handles.Label(propPosition, propTexture);
        }
        else
        {
            Handles.Label(propPosition, "Prop",centeredTextStyle);
        }

        bool saveNeeded = false;

        for (int i = 0; i < humanSpawnPoints.Count; i++)
        {
            if (!saveNeeded)
            {
                EditorGUI.BeginChangeCheck();
            }
            humanSpawnPoints[i] = Handles.PositionHandle(humanSpawnPoints[i], Quaternion.identity);
            if (saveNeeded==false && EditorGUI.EndChangeCheck())
            {
                saveNeeded = true;
            }

            Handles.Label(humanSpawnPoints[i], humanTexture);

        }

        if (saveNeeded)
        {
            //humanManagerPrefab.humanSpawnPoints = humanSpawnPoints;
            for (int i = 0; i < humanSpawnPoints.Count; i++)
            {
                Debug.Log(humanSpawnPoints[i]);
            }
            EditorUtility.SetDirty(humanManagerPrefab);
        }
        

        Handles.EndGUI();
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= onSceneGUI;
    }

    private void OnGUI()
    {
        Color color = GUI.backgroundColor;
        GUI.backgroundColor = Color.yellow;
        EditorGUILayout.LabelField("Set the spawn points by dragging the handles in the scene.");
        GUI.backgroundColor = color;

        GUILayout.Box((new GUIContent()
        {
            image = catBackground
        }),new GUIStyle() {
            stretchWidth=true,
            stretchHeight=true
        });
    }


}
