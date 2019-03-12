using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public Transform self;
    public Rigidbody rigidBody;

    public GameObject brokenProp;

    public void Push(Vector3 forceVector)
    {
        rigidBody.AddForce(forceVector, ForceMode.Impulse);
    }
}
