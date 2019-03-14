using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : Singleton<CatBehaviour>
{
    public Transform self;
    public Animator animator;

    /*public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;*/
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public CatModel catModel;

    public float maxMoveSpeed;

    private float tableRadius;
    private float catHeight;

    [System.NonSerialized] public float currentSpeed;

    public void Init()
    {
        catModel.SetCat(this);
        tableRadius = TableBehaviour.instance.meshFilter.sharedMesh.bounds.size.x / 2 * TableBehaviour.instance.meshFilter.transform.localScale.x;
        catHeight = self.position.y-TableBehaviour.instance.self.position.y;
        currentSpeed = maxMoveSpeed;
        skinnedMeshRenderer.material = CatManager.instance.selectedCatMaterial;
    }

    public void PushProp()
    {
        animator.SetTrigger("Push");
    }


    public void Move(Vector3 velocity)
    {
        velocity *= currentSpeed;
        self.Translate(velocity);
        Vector2 direction = new Vector2(TableBehaviour.instance.self.position.x, TableBehaviour.instance.self.position.z) - new Vector2(self.position.x, self.position.z);
        float angle = Vector2.SignedAngle(new Vector2(velocity.x,velocity.z), Vector2.up)+180;
        animator.transform.eulerAngles = new Vector3(animator.transform.eulerAngles.x, angle, animator.transform.eulerAngles.z);
        if (direction.sqrMagnitude > tableRadius * tableRadius)
        {
            Vector2 clampedDirection = -Vector2.ClampMagnitude(direction, tableRadius);
            self.position = TableBehaviour.instance.self.position + new Vector3(clampedDirection.x, catHeight, clampedDirection.y);
        }
    }





}
