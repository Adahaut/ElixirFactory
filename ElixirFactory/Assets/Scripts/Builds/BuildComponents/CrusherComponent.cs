using System;
using UnityEngine;

public class CrusherComponent : BuildProperties
{
    private void OnMouseUpAsButton()
    {
        UIReferencer.Instance.crusherUI.SetActive(true);
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().currentCrusher = gameObject;
    }
    
}
