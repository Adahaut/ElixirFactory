using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; 
    public GameObject inventorySlotPrefab; 
    public Transform inventoryGrid; 

    void Start()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject); 
        }

        foreach (Item item in inventory.items)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryGrid);
            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            slot.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
        }
    }
}