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
        for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject item = Instantiate(inventorySlotPrefab, inventoryGrid);
        }
    }
}