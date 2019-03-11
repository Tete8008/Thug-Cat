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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            PropBehaviour prop = other.transform.parent.parent.GetComponent<PropBehaviour>();

            Vector3 direction = (prop.self.position - cat.self.position).normalized;
            other.transform.parent.parent.GetComponent<PropBehaviour>().Push(direction*cat.pushStrength);
        }
    }

}
