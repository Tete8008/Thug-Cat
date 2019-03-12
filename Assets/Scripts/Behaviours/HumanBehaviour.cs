using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public Transform self;
    public List<MeshRenderer> meshRenderers;

    public float moveSpeed;
    private float startAngle;
    private float time;

    private float actualRadius;

    private void Start()
    {
        startAngle = Random.Range(0, Mathf.PI * 2);
        
        actualRadius = TableBehaviour.instance.meshFilter.sharedMesh.bounds.size.x / 2 * TableBehaviour.instance.meshFilter.transform.localScale.x + TableBehaviour.instance.humansDistanceFromTable;
        print("human" + actualRadius) ;
        self.position = TableBehaviour.instance.self.position + new Vector3(Mathf.Cos(startAngle), 0, Mathf.Sin(startAngle))*actualRadius;
    }


    private void Update()
    {
        time += Time.deltaTime;

        if (time * moveSpeed >= Mathf.PI * 2)
        {
            time -= Mathf.PI * 2 / moveSpeed;
        }
        self.position = TableBehaviour.instance.self.position + new Vector3(Mathf.Cos(startAngle+time*moveSpeed), 0, Mathf.Sin(startAngle+time * moveSpeed)) * actualRadius;
    }
}
