using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour {

    Vector3 originalPosition;
    HandManager manager;
    GameObject container;

    public string whichHand;
    int offset = 0;

    void Start()
    {
        container = GameObject.FindGameObjectWithTag("Hand Container");
        manager = container.GetComponent<HandManager>();
        if (whichHand == "left")
        {
            offset = -50;
        }
        else
            offset = 0;
    }

    public void BeginDrag()
    {
        originalPosition = transform.position;
    }

    public void OnDrag()
    {
        transform.position = Input.mousePosition + new Vector3(offset, 0, 0);
    }

    public void EndDrag()
    {
        manager.CheckDrag(transform.position, whichHand);
    }
}
