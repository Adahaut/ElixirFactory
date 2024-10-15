using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Vector2 mousePos;
    public Vector2 cameraAxis;
    public float scrollWheel;
    public bool mouseRightClick;
    public bool mouseLeftClick;
    public bool escapePressed;

    public void CheckMouseLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseLeftClick = true;
        }
        else
        {
            mouseLeftClick = false;
        }
    }
    
    public void CheckMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseRightClick = true;
        }
        else
        {
            mouseRightClick = false;
        }
    }

    public void GetMousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;
    }

    public void GetEscape(InputAction.CallbackContext context)
    {
        escapePressed = context.ReadValueAsButton();
    }

    public void SetCameraAxis(InputAction.CallbackContext context)
    {
        cameraAxis = context.ReadValue<Vector2>();
    }

    public void SetScrollWheelValue(InputAction.CallbackContext context)
    {
        scrollWheel = context.ReadValue<float>();
    }
}
