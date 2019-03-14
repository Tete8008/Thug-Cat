using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode { Game,SkinSelection}

public class InputManager : Singleton<InputManager>
{

    private Vector3 initialPosition;

    private bool isTouching = false;

    public float maxInputVelocity;

    private bool active = false;

    private InputMode inputMode=InputMode.Game;

    private Vector3 skinSelectionInitialPosition;

    public void Enable(bool on)
    {
        active = on;
        isTouching = false;
    }

    public void ToggleMode(InputMode inputMode)
    {
        this.inputMode = inputMode;
    }

    private void Update()
    {
        if (!active) { return; }
        switch (inputMode)
        {
            case InputMode.Game:
                if (Input.GetMouseButtonDown(0))
                {
                    initialPosition = Input.mousePosition;
                    isTouching = true;
                    CatBehaviour.instance.animator.SetBool("IsMoving", true);
                    UIManager.instance.tuto.SetActive(false);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isTouching = false;
                    CatBehaviour.instance.animator.SetBool("IsMoving", false);
                }


                if (isTouching)
                {
                    Vector3 direction = Input.mousePosition - initialPosition;
                    Vector3 velocity = new Vector3(direction.x, 0, direction.y);
                    CatBehaviour.instance.Move(Vector3.ClampMagnitude(velocity, maxInputVelocity) * Time.deltaTime);
                }
                break;
            case InputMode.SkinSelection:
                if (Input.GetMouseButtonDown(0))
                {
                    initialPosition = Input.mousePosition;
                    isTouching = true;
                    skinSelectionInitialPosition = SkinSelection.instance.transform.position;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isTouching = false;
                }

                if (isTouching)
                {
                    float offset = (Input.mousePosition.x - initialPosition.x)/100;
                    SkinSelection.instance.transform.position = new Vector3(skinSelectionInitialPosition.x + offset, SkinSelection.instance.transform.position.y, SkinSelection.instance.transform.position.z);
                    if (SkinSelection.instance.transform.localPosition.x> 0.15f){
                        SkinSelection.instance.transform.localPosition = new Vector3(0.15f, SkinSelection.instance.transform.localPosition.y, SkinSelection.instance.transform.localPosition.z);
                    }else if(SkinSelection.instance.transform.localPosition.x< -2.41f)
                    {
                        SkinSelection.instance.transform.localPosition = new Vector3(-2.41f, SkinSelection.instance.transform.localPosition.y, SkinSelection.instance.transform.localPosition.z);
                    }
                }

                break;
        }
        

    }


    public bool HasMoved()
    {
        return (initialPosition != Input.mousePosition);
    }


}
