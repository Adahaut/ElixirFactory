using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private int rowSize = 8;
    private int currentInventoryCount;

    private void Start()
    {
        AddNewRowInInventory();
    }

    public void AddNewRowInInventory()
    {
        for (int i = 0; i < rowSize; i++)
        {
            AddItem(new Item());
        }
        GetComponent<InventoryUI>().UpdateInventoryUI();
    }
    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    private void CheckLastInventoryItem()
    {
        if (items[items.Count - 1].name != "")
        {
            AddNewRowInInventory();
        }
    }
}