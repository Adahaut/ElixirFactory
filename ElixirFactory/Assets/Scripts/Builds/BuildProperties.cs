using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;


public class BuildProperties : MonoBehaviour, BuildInterface, IItemReceiver
{
    public Vector2 coordinates;
    public Vector2 size;
    private GameObject UI;
    private bool isRecipeSet = false;
    protected Recipe recipe;
    protected List<Item> toBuildItem;
    protected Item result;
    public Sprite buildImage;
    private void Start()
    {
        toBuildItem = new ();
        result = new Item();
    }
    
    public void OnBuildingPlaced()
    {
        NotifyNearbyBelts();
    }

    private void NotifyNearbyBelts()
    {
        // Vérifiez les convoyeurs dans un rayon autour du bâtiment et forcez une mise à jour
        Vector2 buildingPosition = transform.position;
        int radius = 1; // Rayon de recherche des convoyeurs adjacents

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector2 beltPosition = new Vector2(buildingPosition.x + x, buildingPosition.y + y);
                if (GridModel.instance.grid[(int)beltPosition.x, (int)beltPosition.y].TryGetComponent<Belt>(out Belt belt) && isRecipeSet)
                {
                    belt.TryTransferItem();
                }
            }
        }
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
                grid[(int)coordinates.x + x, (int)coordinates.y + y].GetComponent<Case>().SetObjectInCase(this);
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
        SetListOfItems(newRecipe.toBuildItems, toBuildItem);
        NotifyNearbyBelts();
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
        ItemToSet.Clear();
        for (int i = 0; i < OldItemList.Count; i++)
        {
            ItemToSet.Add(new Item());
            ItemToSet[i].itemIcon = OldItemList[i].itemIcon;
            ItemToSet[i].itemName = OldItemList[i].itemName;
            ItemToSet[i].currentStack = 0;
            ItemToSet[i].maxstack = OldItemList[i].maxstack;
        }
    }
    
    public void ConstructItem() // à modifier pour résoudre le bug (retire aux items même si tout les items ne sont pas dispo)
    {
        for (int i = 0; i < toBuildItem.Count; i++)
        {
            if (toBuildItem[i].currentStack >= recipe.toBuildItems[i].currentStack)
            {
                toBuildItem[i].currentStack -= recipe.toBuildItems[i].currentStack;
            }
            else
            {
                return;
            }
        }

        StartCoroutine(ConstructItemCoroutine());
    }

    virtual protected IEnumerator ConstructItemCoroutine()
    {
        return null;
    }

    public void ReceiveItem(BeltItem item)
    {
        Debug.Log("Item reçu par la Factory: " + item.itemData.itemName);
        for (int i = 0; i < toBuildItem.Count; i++)
        {
            if (item.itemData.itemName == toBuildItem[i].itemName)
            {
                toBuildItem[i].currentStack += item.itemData.currentStack;
                Destroy(item.gameObject);
            }
        }

    }

    public bool CanReceiveItem()
    {
        return true;
    }
}