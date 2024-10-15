using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    private GameObject categoryToActivate;

    private void Start()
    {
        categoryToActivate = gameObject;
    }

    public void Activate()
    {
        categoryToActivate.SetActive(true);
    }
}
