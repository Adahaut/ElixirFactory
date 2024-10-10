using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); 
    public int maxInventorySize = 20; 
    public void AddItem(Item newItem)
    {
        if (items.Count < maxInventorySize)
        {
            items.Add(newItem);
            Debug.Log(newItem.itemName + " a été ajouté.");
        }
        else
        {
            Debug.Log("Inventaire plein !");
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " a été retiré.");
        }
    }
}