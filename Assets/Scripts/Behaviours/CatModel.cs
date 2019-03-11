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
            other.transform.parent.parent.GetComponent<PropBehaviour>().Move(cat.catData.pushPower);
        }
    }

}
