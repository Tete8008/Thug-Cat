using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    private Vector3 initialPosition;

    private bool isTouching = false;

    public float maxInputVelocity;

    private bool active = false;

    public void Enable(bool on)
    {
        active = on;
    }

    private void Update()
    {
        //print("active" + active);
        if (!active) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = Input.mousePosition;
            isTouching = true;
            CatBehaviour.instance.animator.SetBool("IsMoving", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
            CatBehaviour.instance.animator.SetBool("IsMoving", false);
        }


        if (isTouching)
        {
            Vector3 direction = Input.mousePosition - initialPosition;
            Vector3 velocity = new Vector3(direction.x, 0, direction.y) ;
            CatBehaviour.instance.Move(Vector3.ClampMagnitude(velocity,maxInputVelocity) * Time.deltaTime);
        }

    }


}
