using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherComponent : MonoBehaviour, BuildInterface
{
    private GameObject UI;
    private bool isRecipeSet = false;
    private Recipe recipe;
    private Item toBuildItem;
    private Item result;

    private void Start()
    {
        toBuildItem = new Item();
        result = new Item();
    }

    private void OnMouseUpAsButton()
    {
        UIReferencer.Instance.CrusherUI.SetActive(true);
        UIReferencer.Instance.CrusherUI.GetComponent<CrusherUI>().currentCrusher = gameObject;
    }

    public bool GetRecipeSet()
    {
        return isRecipeSet;
    }

    public Item GetResult()
    {
        return result;
    }

    public Item GetToBuildItem()
    {
        return toBuildItem;
    }
    
    public void SetRecipe(Recipe newRecipe)
    {
        recipe = newRecipe;
        result = newRecipe.result;
        toBuildItem = newRecipe.toBuildItems[0];
        result.currentStack = 0;
        isRecipeSet = true;
    }
    
    public void ConstructItem()
    {
        if (toBuildItem.currentStack >= recipe.toBuildItems[0].currentStack)
        {
            toBuildItem.currentStack -= recipe.toBuildItems[0].currentStack;
            result.currentStack += recipe.result.currentStack;
        }
    }
    
}
