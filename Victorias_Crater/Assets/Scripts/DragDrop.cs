using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

    [SerializeField] private Canvas canvas;
    public static bool isAnchored;//check on true item place
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector2 originalPos;
    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    //public void OnPointerDown(PointerEventData eventData) {
    //    Debug.Log("Item get clicked");

    //}

    public void OnBeginDrag(PointerEventData eventData) {

        Debug.Log("Start dragging");

        isAnchored = false;
        originalPos = rectTransform.transform.position;
        canvasGroup.blocksRaycasts = false; //allow collision
    }

    public void OnDrag(PointerEventData eventData) {
       
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor; //add dist whitch mouse move depends on canvas scale

    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("dragIsOff");
        canvasGroup.blocksRaycasts = true;
        if (isAnchored) {
            originalPos = rectTransform.transform.position;
        }
        else {
            rectTransform.transform.position = originalPos;//rewrite pos in arr of obj
        }
    }
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("yes");
            isAnchored = true;
        }
        else {
            isAnchored = false;
            Debug.Log("no");
        }
    }

}

