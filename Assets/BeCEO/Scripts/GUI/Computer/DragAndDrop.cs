using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 170);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rect.anchoredPosition += eventData.delta;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255);
    }
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    
    
}
