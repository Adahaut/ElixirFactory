using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CrusherComponent : BuildProperties
{
    private void OnMouseUpAsButton()
    {
        UIReferencer.Instance.crusherUI.SetActive(true);
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().currentCrusher = gameObject;
    }

    protected override IEnumerator ConstructItemCoroutine()
    {
        float timer = 0f;
        while (timer / recipe.buildTime < 1)
        {
            timer += Time.deltaTime;
            UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().sliderConstructTime.value = timer / recipe.buildTime;
            yield return new WaitForEndOfFrame();
        }
        result.currentStack += recipe.result.currentStack;
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().sliderConstructTime.value = 0;
        UIReferencer.Instance.crusherUI.GetComponent<CrusherUI>().outSlot.GetComponent<DragAndDropSlot>()
            .itemInSolt = result;
        ConstructItem();
        yield return null;
    }
    
}
