using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Vector2 mousePos;
    public bool mouseRightClick;
    public bool mouseLeftClick;

    public void CheckMouseLeftClick(InputAction.CallbackContext context)
    {
        mouseLeftClick = context.ReadValueAsButton();
    }
    
    public void CheckMouseRightClick(InputAction.CallbackContext context)
    {
        mouseRightClick = context.ReadValueAsButton();
    }

    public void GetMousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;
    }
}
