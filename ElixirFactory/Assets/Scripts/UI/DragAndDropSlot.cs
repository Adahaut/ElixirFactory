using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public Item itemInSolt;
    private CanvasGroup canvasGroup;
    public Canvas SlotCanvas;
    private Transform previousParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        SlotCanvas = GameObject.FindWithTag("SlotCanvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Sauvegarder la position originale du bouton
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        previousParent = transform.parent;
        transform.SetParent(SlotCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Déplacer le bouton en fonction du mouvement de la souris
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(previousParent);
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}