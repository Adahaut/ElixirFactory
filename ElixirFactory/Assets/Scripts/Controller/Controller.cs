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
    public bool escapePressed;
    private AllMenuController allMenuController;

    private void Start()
    {
        allMenuController = GetComponent<AllMenuController>();
    }

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
        allMenuController.DesactiveAllActivatedMenus();
    }

    public void GetBuildMenuIsPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            allMenuController.ActiveMenu(allMenuController.buildMenu);
    }
    
    public void GetInventoryIsPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            allMenuController.ActiveMenu(allMenuController.inventory);
    }
}
