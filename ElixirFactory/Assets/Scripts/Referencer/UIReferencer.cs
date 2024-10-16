using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferencer : MonoBehaviour
{
    public static UIReferencer Instance;
    public GameObject CrusherUI;
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
}
