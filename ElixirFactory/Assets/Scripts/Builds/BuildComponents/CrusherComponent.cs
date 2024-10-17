using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherComponent : MonoBehaviour, BuildInterface
{
    private GameObject UI;
    private bool isRecipeSet = false;
    private Recipe recipe;
    private List<Item> toBuildItem;
    private Item result;

    private void Start()
    {
        toBuildItem = new (1);
        toBuildItem.Add(new Item());
        result = new Item();
    }

    private void OnMouseUpAsButton()
    {
        UIReferencer.Instance.crusherUI.SetActive(true);
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().currentCrusher = gameObject;
    }

    public bool GetRecipeSet()
    {
        return isRecipeSet;
    }

    public Item GetResult()
    {
        return result;
    }

    public List<Item> GetToBuildItem()
    {
        return toBuildItem;
    }
    
    public void SetRecipe(Recipe newRecipe)
    {
        recipe = newRecipe;
        SetItem(newRecipe.result, result);
        SetItem(newRecipe.toBuildItems[0], toBuildItem[0]);
        isRecipeSet = true;
    }
    
    public void ConstructItem()
    {
        if (toBuildItem[0].currentStack >= recipe.toBuildItems[0].currentStack)
        {
            toBuildItem[0].currentStack -= recipe.toBuildItems[0].currentStack;
            result.currentStack += recipe.result.currentStack;
        }
    }

    private void SetItem(Item newItem, Item itemToSet)
    {
        itemToSet.itemIcon = newItem.itemIcon;
        itemToSet.itemName = newItem.itemName;
        itemToSet.currentStack = 0;
        itemToSet.maxstack = newItem.maxstack;
    }
    
}
