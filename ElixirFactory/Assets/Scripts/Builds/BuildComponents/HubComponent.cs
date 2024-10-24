using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubComponent : BuildProperties
{
    private void OnMouseUpAsButton()
    {
        UIReferencer.Instance.ActiveMenu(UIReferencer.Instance.HubMenu);
        UIReferencer.Instance.crusherUI.GetComponent<HubUI>().hubComponent = this;
        // UIReferencer.Instance.crusherUI.GetComponent<HubUI>().currentCrusherComponent = this;
        // UIReferencer.Instance.crusherUI.GetComponent<HubUI>().sliderConstructTime.value = 0;
    }
}
