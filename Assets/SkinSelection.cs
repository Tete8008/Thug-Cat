using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelection : Singleton<SkinSelection>
{
    public List<CatSkin> catSkins;

    public void InitSkins()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }


        for (int i = 0; i < catSkins.Count; i++)
        {
            if (CatManager.instance.selectedCatMaterial == catSkins[i].material)
            {
                catSkins[i].animator.SetBool("Selected", true);
            }
            else
            {
                catSkins[i].animator.SetBool("Selected", false);
            }

        }
    }

    public void HideSkins()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
