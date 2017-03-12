using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour {

    private RaycastHit hit;
    GameObject leftHandObj;
    GameObject rightHandObj;

    string currentHand;     //Hand being tested in CheckDrag
    string leftLocation = null;     //Left hand current location
    string rightLocation = null;        //Right hand current location

    void Start()
    {
        leftHandObj = GameObject.Find("LeftHand");
        rightHandObj = GameObject.Find("RightHand");
    }

    //
    public void CheckDrag(Vector3 dragTransform, string whichHand)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            switch(whichHand)
            {
                case "left":
                    leftLocation = hit.collider.transform.tag;
                    break;
                case "right":
                    rightLocation = hit.collider.transform.tag;
                    break;
                default:
                    Debug.Log("Error at HandManager");
                    break;
            }

            if(leftLocation!=null && rightLocation!=null)
            {
                Debug.Log("left hand at " + leftLocation + "  and right hand at " + rightLocation);
            }
        }
    }
}
