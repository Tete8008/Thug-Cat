using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    public Transform self;
    public Animator animator;

    [System.NonSerialized] public CatData catData;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public CatModel catModel;

    public void Init(CatData catData)
    {
        this.catData = catData;
        catModel.SetCat(this);
    }

    public void PushProp()
    {
        animator.SetFloat("PushSpeedMultiplier", 1/catData.pushDuration);
        animator.SetTrigger("Push");
    }


    

}
