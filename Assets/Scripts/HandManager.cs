using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    //LayoutElement leftLayout;
    //LayoutElement rightLayout;

    public string currentlyTesting = "";

    void Start()
    {
        leftHandObj = GameObject.Find("LeftHand");
        //leftLayout = leftHandObj.gameObject.GetComponent<LayoutElement>();
        rightHandObj = GameObject.Find("RightHand");
        //rightLayout = leftHandObj.gameObject.GetComponent<LayoutElement>();
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
            //ToggleLayout();
        }
        else
        {
            this.gameObject.SetActive(true);
            active = true;
            leftHandObj.SetActive(true);
            rightHandObj.SetActive(true);
            //ToggleLayout();
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
            Debug.Log("HANDS IN INCORRECT SPOT Left: " + leftLocation + " Right: " + rightLocation);
            Tracker.LogData("HANDS IN INCORRECT SPOT Left: " + leftLocation + " Right: " + rightLocation);
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
        Tracker.LogData("HANDS IN CORRECT SPOT");
        Tracker.clicked.Add("PROMLHP");
        success = true;
        ToggleHands();
    }

    /*public void ToggleLayout()
    {
        leftLayout.ignoreLayout = true;
        leftHandObj.transform.position = originalPosLeft;
        rightLayout.ignoreLayout = true;
        rightHandObj.transform.position = originalPosRight;
    }*/
}
