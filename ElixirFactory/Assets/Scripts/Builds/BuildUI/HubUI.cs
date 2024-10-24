using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HubUI : MonoBehaviour, IDropHandler
{
    public List<Image> thingToUnlockImgs;
    public List<Image> milestoneImgs;
    public List<Image> priceSlotImgs;
    public HubComponent hubComponent;

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDropSlot button = eventData.pointerDrag.GetComponent<DragAndDropSlot>();
        if (button != null)
        {
            Inventory.instance.TransferItemFromInvToOther(hubComponent.GetToBuildItem(), button.itemInSolt);
        }
    }

    public void OnClickOnButton(Button buttonToActive)
    {
        buttonToActive.GetComponent<MilestoneButtonHub>().UpdateUiThingsToUnlock(thingToUnlockImgs);
        for (int i = 0; i < priceSlotImgs.Count; i++)
        {
            if (i > buttonToActive.GetComponent<MilestoneButtonHub>().PriceItems.Count)
            {
                priceSlotImgs[i].gameObject.SetActive(false);
            }
            else
            {
                priceSlotImgs[i].gameObject.SetActive(true);
                priceSlotImgs[i].sprite =
                    buttonToActive.GetComponent<MilestoneButtonHub>().PriceItems[i].itemIcon;
                priceSlotImgs[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    buttonToActive.GetComponent<MilestoneButtonHub>().PriceItems[i].currentStack.ToString();
            }
        }
    }


}
