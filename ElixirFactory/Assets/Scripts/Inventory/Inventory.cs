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
        for (int i = 0; i < rowSize; i++)
        {
            items.Add(new Item());
        }
        GetComponent<InventoryUI>().InitInventoryUI();
    }
    public void AddItem(Item newItem)
    { 
        AddStackOfItem(newItem.currentStack, newItem);
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
    
    private int CheckForFirstSlotOfTypeNotFull(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName && items[i].currentStack < items[i].maxstack)
            {
                return i;
            }
        } 
        return CheckForFirstEmptySlot();
    }

    private void AddStackOfItem(int amountToAdd, Item newItem)
    {
        while (amountToAdd > 0)
        {
            int newStackSlot = CheckForFirstSlotOfTypeNotFull(newItem.itemName);
            items[newStackSlot].maxstack = newItem.maxstack;

            int valueToAdd;
            if (amountToAdd > items[newStackSlot].maxstack - items[newStackSlot].currentStack)
            {
                valueToAdd = items[newStackSlot].maxstack - items[newStackSlot].currentStack;
                items[newStackSlot].currentStack += valueToAdd;
                amountToAdd -= valueToAdd;
            }
            else
            {
                items[newStackSlot].currentStack += amountToAdd;
                amountToAdd = 0;
            }
            items[newStackSlot].itemIcon = newItem.itemIcon;
            items[newStackSlot].itemName = newItem.itemName;
            CheckLastInventoryItem();
            GetComponent<InventoryUI>().UpdateInventoryUI();
        }
    }


}