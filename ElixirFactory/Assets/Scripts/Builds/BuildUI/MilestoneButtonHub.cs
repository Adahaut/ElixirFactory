using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneButtonHub : MonoBehaviour
{
    public List<GameObject> thingsToUnlock;
    public List<Item> PriceItems;

    public void UpdateUiThingsToUnlock(List<Image> ToUpdateList)
    {
        for (int i = 0; i < ToUpdateList.Count; i++)
        {
            if (i > thingsToUnlock.Count)
            {  
                ToUpdateList[i].gameObject.SetActive(false);
            }
            else
            {
                ToUpdateList[i].gameObject.SetActive(true);
                ToUpdateList[i].sprite = thingsToUnlock[i].GetComponent<BuildProperties>().buildImage;
            }
        }
    }
}
