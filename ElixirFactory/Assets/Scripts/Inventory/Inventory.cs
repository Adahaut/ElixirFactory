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
                int difference = items[i].maxstack - (items[i].currentStack + newItem.currentStack);
                if (difference > 0)
                {
                    items[i].currentStack += newItem.currentStack;
                    newItem.currentStack = 0;
                    difference = 0;
                    Debug.Log(difference);
                }
                else if (difference < 0)
                {
                    items[i].currentStack += newItem.currentStack + difference;
                    newItem.currentStack -= newItem.currentStack + difference;
                    AddStackOfItem(newItem.currentStack, newItem);
                }
                CheckLastInventoryItem();
                GetComponent<InventoryUI>().UpdateInventoryUI();
            }
            else if (items[i].itemName == "")
            {
                // CreateItemToAddInInventory(items[i], newItem);
                AddStackOfItem(newItem.currentStack, newItem);
                CheckLastInventoryItem();
                GetComponent<InventoryUI>().UpdateInventoryUI();
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

    private int CheckForFirstEmptySlot()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == "")
            {
                return i;
            }
        }
        return items.Count - 1;
    }

    private void CreateItemToAddInInventory(Item itemInInventory, Item newItem)
    {
            itemInInventory.itemName = newItem.itemName;
            itemInInventory.currentStack = newItem.currentStack;
            itemInInventory.maxstack = newItem.maxstack;
            itemInInventory.itemIcon = newItem.itemIcon;
    }

    private void AddStackOfItem(int amountToAdd, Item newItem)
    {
        while (amountToAdd > 0)
        {
            int newStackSlot = CheckForFirstEmptySlot();
            items[newStackSlot].maxstack = newItem.maxstack;
            if (items[newStackSlot].maxstack < amountToAdd)
            {
                items[newStackSlot].currentStack = items[newStackSlot].maxstack;
                amountToAdd -= items[newStackSlot].maxstack;
                newItem.currentStack -= items[newStackSlot].maxstack;
            }
            else
            {
                items[newStackSlot].currentStack = amountToAdd;
                amountToAdd = 0;
                newItem.currentStack = 0;
            }
            items[newStackSlot].itemIcon = newItem.itemIcon;
            items[newStackSlot].itemName = newItem.itemName;
            
            
        }
    }


}