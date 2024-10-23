using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllMenuController : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject inventory;
    public GameObject crusherUI;

    private void Awake()
    {
        buildMenu = UIReferencer.Instance.buildMenu;
        inventory = UIReferencer.Instance.inventory;
        crusherUI = UIReferencer.Instance.crusherUI;
    }


}
