using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatModel : MonoBehaviour
{

    private CatBehaviour cat;

    public void SetCat(CatBehaviour cat)
    {
        this.cat = cat;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            PropBehaviour prop = other.transform.parent.parent.GetComponent<PropBehaviour>();

            Vector3 direction = (prop.self.position - cat.self.position).normalized;
            other.transform.parent.parent.GetComponent<PropBehaviour>().Push(direction*cat.pushStrength);
        }
    }*/


    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Prop"))
        {
            PropBehaviour prop = collision.collider.GetComponent<PropBehaviour>();
            if (prop.weight != 0)
            {
                cat.currentSpeed = cat.maxMoveSpeed / collision.collider.GetComponent<PropBehaviour>().weight;
            }
            
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Prop"))
        {
            cat.currentSpeed = cat.maxMoveSpeed;
        }
    }

}
