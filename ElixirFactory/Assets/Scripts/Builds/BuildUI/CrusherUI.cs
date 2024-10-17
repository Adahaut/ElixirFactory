using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrusherUI : MonoBehaviour, IDropHandler
{
    public GameObject currentCrusher;
    public CrusherComponent currentCrusherComponent;
    public GameObject RecipePanel;
    public GameObject buildPanel;
    public GameObject inSlot;
    public GameObject outSlot;
    private Image inSlotRenderer;
    private Image outSlotRenderer;
    private TextMeshProUGUI inSlotText;
    private TextMeshProUGUI outSlotText;
    private void Start()
    {
        inSlotRenderer = inSlot.GetComponent<Image>();
        outSlotRenderer = outSlot.GetComponent<Image>();
        inSlotText = inSlot.GetComponentInChildren<TextMeshProUGUI>();
        outSlotText = outSlot.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UIReferencer.Instance.inventory.SetActive(true);
    }

    public void SetRecipe(Recipe recipe)
    {
        currentCrusherComponent.SetRecipe(recipe);
    }

    private void UpdateUI()
    {
        inSlotRenderer.sprite = currentCrusherComponent.GetToBuildItem()[0].itemIcon;
        inSlotText.text = currentCrusherComponent.GetToBuildItem()[0].currentStack.ToString();
        outSlotRenderer.sprite = currentCrusherComponent.GetResult().itemIcon;
        outSlotText.text = currentCrusherComponent.GetResult().currentStack.ToString();
    }

    private void Update()
    {
        if (currentCrusherComponent == null)
        {
            currentCrusherComponent = currentCrusher.GetComponent<CrusherComponent>();
        }
        else
        {
            CheckForRecipe();
            UpdateUI();
        }
    }

    private void CheckForRecipe()
    {
        if (currentCrusherComponent.GetRecipeSet())
        {
            RecipePanel.SetActive(false);
            buildPanel.SetActive(true);
        }
        else
        {
            RecipePanel.SetActive(true);
            buildPanel.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDropSlot button = eventData.pointerDrag.GetComponent<DragAndDropSlot>();
        if (button != null)
        {
            Debug.Log("Item DROPED");
            Inventory.instance.TransferItemFromInvToOther(currentCrusherComponent.GetToBuildItem(), button.itemInSolt);
        }
    }
}
