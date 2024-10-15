using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesManager : MonoBehaviour
{
    public GameObject[] categories;

    public void DesactivateCategories()
    {
        for (int i = 0; i < categories.Length; i++)
        {
            categories[i].SetActive(false);
        }
    }

}
