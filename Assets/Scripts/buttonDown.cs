using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//Checks if a UI button is pressed
public class buttonDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public bool mouseDown = false;
	public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
        Debug.Log(mouseDown);
    }
}
