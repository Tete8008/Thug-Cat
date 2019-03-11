using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    public Transform self;
    public Animator animator;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public CatModel catModel;

    public float pushStrength;

    public void Init()
    {
        catModel.SetCat(this);
    }

    public void PushProp()
    {
        animator.SetTrigger("Push");
    }


    

}
