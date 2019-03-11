using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PropManager.instance.PushActiveObject();
        }
    }


}
