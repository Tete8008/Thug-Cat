﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public Transform self;
    public Rigidbody rigidBody;
    public MeshRenderer meshRenderer;
    

    public GameObject brokenProp;

    public float weight;

    [System.NonSerialized] public Vector3 initialPosition;
    [System.NonSerialized] public Quaternion initialRotation;
    [System.NonSerialized] public bool attached = false;

    private void Start()
    {
        initialPosition = self.position;
        initialRotation = self.rotation;
        attached = false;
    }

    public void Push(Vector3 forceVector)
    {
        rigidBody.AddForce(forceVector, ForceMode.Impulse);
    }

}
