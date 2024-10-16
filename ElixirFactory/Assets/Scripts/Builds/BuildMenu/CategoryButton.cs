using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    private CategoriesManager categoriesManager;

    public void Activate()
    {
        if (categoriesManager == null)
        {
            categoriesManager = GetComponentInParent<CategoriesManager>();
        }
        categoriesManager.DesactivateCategories();
        gameObject.SetActive(true);
    }
    
}
