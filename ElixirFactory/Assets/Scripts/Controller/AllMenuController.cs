using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllMenuController : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject inventory;
    public List<GameObject> allActivatedMenus;
    public void ActiveMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            allActivatedMenus.Add(menu);
        }
        else
        {
            if (allActivatedMenus.Contains(menu))
            {
                allActivatedMenus.Remove(menu);
            }
        }
    }

    public void DesactiveAllActivatedMenus()
    {
        if (allActivatedMenus.Count > 0)
        {
            foreach (GameObject menu in allActivatedMenus)
            {
                menu.SetActive(false);
                allActivatedMenus.Remove(menu);
            }
        }
    }

}
