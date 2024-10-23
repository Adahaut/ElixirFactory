using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int rowSize = 6;
    private int currentInventoryCount;
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        UIReferencer.Instance.inventory.GetComponent<InventoryUI>().InitInventoryUI();
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
            UIReferencer.Instance.inventory.GetComponent<InventoryUI>().UpdateInventoryUI();
        }
    }

    public void TransferItemFromInvToOther(List<Item> others, Item itemToTransfer)
    {
        if (!itemToTransfer)
        {
            return;
        }
        for (int i = 0; i < others.Count; i++)
        {
            if (others[i].itemName == itemToTransfer.itemName)
            {
                int diff = others[i].maxstack - others[i].currentStack;
                if (itemToTransfer.currentStack > diff)
                {
                    others[i].currentStack += diff;
                    itemToTransfer.currentStack -= diff;
                }
                else
                {
                    others[i].currentStack += itemToTransfer.currentStack;
                    resetItem(itemToTransfer);
                }
            }
        }
        UIReferencer.Instance.inventory.GetComponent<InventoryUI>().UpdateInventoryUI();
    }
    
    public void TransferItemFromOtherToInventory(Item other)
    {
        if (!other)
        {
            return;
        }
        AddStackOfItem(other.currentStack, other);
        other.currentStack = 0;
        UIReferencer.Instance.inventory.GetComponent<InventoryUI>().UpdateInventoryUI();
    }

    private void resetItem(Item itemToReset)
    {
        itemToReset.itemIcon = null;
        itemToReset.currentStack = 0;
        itemToReset.maxstack = 0;
        itemToReset.itemName = "";
    }

}