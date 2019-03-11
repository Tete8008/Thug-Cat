using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public Transform self;
    public Rigidbody rigidBody;

    [System.NonSerialized] public PropData propData;

    private GameObject propModel;

    public void Init(PropData propData)
    {
        this.propData = propData;
    }

    public void Move(float catPower)
    {
        rigidBody.AddForce(new Vector3(catPower / propData.weight, 0, 0), ForceMode.Impulse);
        //self.Translate(new Vector3(catPower/propData.weight, 0, 0));
    }

}
