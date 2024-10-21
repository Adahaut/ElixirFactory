using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class BuildProperties : MonoBehaviour, BuildInterface
{
    public Vector2 coordinates;
    public Vector2 size;
    private GameObject UI;
    private bool isRecipeSet = false;
    private Recipe recipe;
    private List<Item> toBuildItem;
    private Item result;

    private void Start()
    {
        toBuildItem = new ();
        result = new Item();
    }
    
    public void SetCoordinates(Vector2 newCoordinates)
    {
        coordinates = newCoordinates;
    }

    public Vector2 GetCoordinates()
    {
        return coordinates;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public void SetCoordinatesOfBuildInGrid(GameObject[,] grid)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                grid[(int)coordinates.x + x, (int)coordinates.y + y].GetComponent<Case>().isOccupied = true;
            }
        }
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
    
    protected void SetItem(Item OldItem, Item itemToSet)
    {
        itemToSet.itemIcon = OldItem.itemIcon;
        itemToSet.itemName = OldItem.itemName;
        itemToSet.currentStack = 0;
        itemToSet.maxstack = OldItem.maxstack;
    }

    protected void SetListOfItems(List<Item> OldItemList, List<Item> ItemToSet)
    {
        for (int i = 0; i < ItemToSet.Count; i++)
        {
            ItemToSet[i].itemIcon = OldItemList[i].itemIcon;
            ItemToSet[i].itemName = OldItemList[i].itemName;
            ItemToSet[i].currentStack = 0;
            ItemToSet[i].maxstack = OldItemList[i].maxstack;
        }
    }
    
    public void ConstructItem()
    {
        if (toBuildItem[0].currentStack >= recipe.toBuildItems[0].currentStack)
        {
            toBuildItem[0].currentStack -= recipe.toBuildItems[0].currentStack;
            StartCoroutine(ConstructItemCoroutine());
        }
    }
    
    private IEnumerator ConstructItemCoroutine()
    {
        float timer = 0f;
        while (timer / recipe.buildTime < 1)
        {
            timer += Time.deltaTime;
            UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().sliderConstructTime.value = timer / recipe.buildTime;
            yield return new WaitForEndOfFrame();
        }
        result.currentStack += recipe.result.currentStack;
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().sliderConstructTime.value = 0;
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().outSlot.GetComponent<DragAndDropSlot>()
            .itemInSolt = result;
        ConstructItem();
        yield return null;
    }
}