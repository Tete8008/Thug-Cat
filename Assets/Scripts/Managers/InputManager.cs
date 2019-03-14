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
                    skinSelectionInitialPosition = UIManager.instance.menu.skinSelection.transform.position;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isTouching = false;
                }

                if (isTouching)
                {
                    float offset = (Input.mousePosition.x - initialPosition.x)/100;
                    UIManager.instance.menu.skinSelection.transform.position = new Vector3(skinSelectionInitialPosition.x + offset, UIManager.instance.menu.skinSelection.transform.position.y, UIManager.instance.menu.skinSelection.transform.position.z);
                    if (UIManager.instance.menu.skinSelection.transform.localPosition.x> -534.5f){
                        UIManager.instance.menu.skinSelection.transform.localPosition = new Vector3(-534.5f, UIManager.instance.menu.skinSelection.transform.localPosition.y, UIManager.instance.menu.skinSelection.transform.localPosition.z);
                    }else if(UIManager.instance.menu.skinSelection.transform.localPosition.x< -545.1f)
                    {
                        UIManager.instance.menu.skinSelection.transform.localPosition = new Vector3(-545.1f, UIManager.instance.menu.skinSelection.transform.localPosition.y, UIManager.instance.menu.skinSelection.transform.localPosition.z);
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
