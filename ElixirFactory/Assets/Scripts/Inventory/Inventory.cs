using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int rowSize = 8;
    private int currentInventoryCount;

    private void Start()
    {
        AddNewRowInInventory();

    }

    public void AddNewRowInInventory()
    {
        Debug.Log("start");
        for (int i = 0; i < rowSize; i++)
        {
            items.Add(new Item());
        }
        GetComponent<InventoryUI>().InitInventoryUI();
    }
    public void AddItem(Item newItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == newItem.itemName)
            {
                
                if (items[i].maxstack < items[i].currentStack + newItem.currentStack)
                {
                    //create other items
                }
                else
                {
                    items[i].currentStack += newItem.currentStack;
                }
                CheckLastInventoryItem();
                GetComponent<InventoryUI>().UpdateInventoryUI();
                return;
            }
            else if (items[i].itemName == "")
            {
                CreateItemToAddInInventory(items[i], newItem);
                CheckLastInventoryItem();
                GetComponent<InventoryUI>().UpdateInventoryUI();
                return;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    private void CheckLastInventoryItem()
    {
        if (items[items.Count - 2].itemName != "")
        {
            AddNewRowInInventory();
        }
    }

    private void CreateItemToAddInInventory(Item itemInInventory, Item newItem)
    {
        itemInInventory.itemName = newItem.itemName;
        itemInInventory.currentStack = newItem.currentStack;
        itemInInventory.maxstack = newItem.maxstack;
        itemInInventory.itemIcon = newItem.itemIcon;
    }
}