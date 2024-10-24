using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferencer : MonoBehaviour
{
    public static UIReferencer Instance;
    public GameObject crusherUI;
    public GameObject inventory;
    public GameObject buildMenu;
    public GameObject HubMenu;
    public List<GameObject> allActivatedMenus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    public void ActiveMenu(GameObject menu)
    {
        DesactiveAllActivatedMenus();
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
