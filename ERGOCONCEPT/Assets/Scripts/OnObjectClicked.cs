using UnityEngine;
using UnityEngine.EventSystems;

public class OnObjectClicked : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }
}