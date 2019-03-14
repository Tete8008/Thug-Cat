using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSkin : MonoBehaviour
{
    public Material material;
    public Animator animator;





    private void OnMouseUp()
    {
        if (!InputManager.instance.HasMoved())
        {
            CatManager.instance.selectedCatMaterial = material;

            for (int i = 0; i < MenuBehaviour.instance.catSkins.Count; i++)
            {
                print(MenuBehaviour.instance.catSkins[i].material != material);
                if (MenuBehaviour.instance.catSkins[i].material != material)
                {
                    MenuBehaviour.instance.catSkins[i].animator.SetBool("Selected", false);
                }
                else
                {
                    animator.SetBool("Selected", true);
                }
            }
        }
    }
}