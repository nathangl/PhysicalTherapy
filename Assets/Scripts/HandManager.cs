using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandManager : MonoBehaviour
{

    private RaycastHit hit;
    GameObject leftHandObj;
    GameObject rightHandObj;

    string leftLocation = null;     //Left hand current location
    string rightLocation = null;        //Right hand current location

    public bool active = false;
    public bool success = false;

    public string currentlyTesting = "";

    void Start()
    {
        leftHandObj = GameObject.Find("LeftHand");
        rightHandObj = GameObject.Find("RightHand");
        this.gameObject.SetActive(false);
    }

    public void ToggleHands()
    {
        if (active)
        {
            this.gameObject.SetActive(false);
            active = false;
            leftLocation = null;
            rightLocation = null;
        }
        else
        {
            this.gameObject.SetActive(true);
            active = true;
            leftHandObj.SetActive(true);
            rightHandObj.SetActive(true);
        }
    }

    //
    public void CheckDrag(Vector3 dragTransform, string whichHand)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            switch (whichHand)
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

            if (leftLocation != null)
            {
                leftHandObj.SetActive(false);
            }
            if (rightLocation != null)
            {
                rightHandObj.SetActive(false);
            }

            if (leftLocation != null && rightLocation != null)
            {
                CheckLocations();
                leftHandObj.SetActive(true);
                rightHandObj.SetActive(true);
            }
        }
    }

    public void CheckLocations()
    {
        if (currentlyTesting == "rightprom" && leftLocation == "RightElbow" && rightLocation == "RightHand")
        {
            Success();
        }
        else if (currentlyTesting == "leftprom" && leftLocation == "LeftHand" && rightLocation == "LeftElbow")
        {
            Success();
        }
        else
        {
            Debug.Log("HANDS IN INCORRECT SPOT");
        }
        ResetLocations();
    }

    public void ResetLocations()
    {
        leftLocation = null;
        rightLocation = null;
    }

    public void Success()
    {
        Debug.Log("HANDS IN CORRECT SPOT");
        success = true;
        ToggleHands();
        active = false;
    }
}
