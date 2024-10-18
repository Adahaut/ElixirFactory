using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IDropHandler
{
    public Inventory inventory; 
    public GameObject inventorySlotPrefab; 
    public Transform inventoryGrid; 
    private List<GameObject> inventorySlots = new();

    void Awake()
    {
        inventory = Inventory.instance;
    }

    public void InitInventoryUI()
    {
        for (int i = 0; i < inventory.rowSize; i++)
        {
            GameObject item = Instantiate(inventorySlotPrefab, inventoryGrid);
            item.GetComponentInChildren<TextMeshProUGUI>().text = "";
            inventorySlots.Add(item);
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {

                inventorySlots[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.items[i].currentStack.ToString();
                inventorySlots[i].GetComponent<Image>().sprite =
                    inventory.items[i].itemIcon;
                inventorySlots[i].GetComponent<DragAndDropSlot>().itemInSolt = inventory.items[i];
            
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDropSlot button = eventData.pointerDrag.GetComponent<DragAndDropSlot>();
        Debug.Log("Drop in inventory");
        if (button != null && !inventorySlots.Contains(button.gameObject))
        {
            inventory.TransferItemFromOtherToInventory(button.itemInSolt);
        }
    }
}