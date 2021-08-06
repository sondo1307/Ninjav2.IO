using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnItemClick : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    , IScrollHandler, IDragHandler, IEndDragHandler
{
    public GameObject outline;


    public void OnSelect(BaseEventData eventData)
    {
            outline.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
            outline.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //outline.SetActive(false);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //outline.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //outline.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //outline.SetActive(true);

    }

    public void OnScroll(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
