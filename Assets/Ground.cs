using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            PropManager.instance.PropFallen(other.transform.parent.parent.GetComponent<PropBehaviour>());
        }
    }
}
