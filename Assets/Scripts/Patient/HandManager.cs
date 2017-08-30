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

    GameObject leftLocal;
    GameObject rightLocal;

    public bool active = false;
    public bool success = false;

    public Material red;
    public Material yellow;

    public GameObject placementUI;
    public int selection = 1;
    public Text selectionTxt;

    //LayoutElement leftLayout;
    //LayoutElement rightLayout;

    public string currentlyTesting = "";

    void Start()
    {
        placementUI.SetActive(false);
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
            if(leftLocal)
            {
                leftLocal.GetComponentInChildren<Renderer>().material = yellow;
                leftLocal = null;
            }
            if(rightLocal)
            {
                rightLocal.GetComponentInChildren<Renderer>().material = yellow;
                rightLocal = null;
            }
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
                    leftLocal = hit.collider.gameObject;
                    hit.collider.GetComponentInChildren<Renderer>().material = red;
                    StartPlacement();
                    break;
                case "right":
                    rightLocation = hit.collider.transform.tag;
                    rightLocal = hit.collider.gameObject;
                    hit.collider.GetComponentInChildren<Renderer>().material = red;
                    StartPlacement();
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
        }
    }

    public void CheckLocations()
    {
        if (currentlyTesting == "RightShoulder" && leftLocation == "RightElbow" && rightLocation == "RightHand")
        {
            Success();
        }
        else if (currentlyTesting == "LeftShoulder" && leftLocation == "LeftHand" && rightLocation == "LeftElbow")
        {
            Success();
        }
        else if (currentlyTesting == "LeftElbow" && leftLocation == "LeftHand" && rightLocation == "LeftElbow")
        {
            Success();
        }
        else
        {
            Success();
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

    public void StartPlacement()
    {
        placementUI.SetActive(true);
        placementUI.transform.position = Input.mousePosition + new Vector3(0, 0, 0);        //mouse position with offset (0)
        selectionTxt.text = selection.ToString();
    }

    public void DisablePlacement()
    {
        if (leftLocation != null && rightLocation != null)
        {
            CheckLocations();
            leftHandObj.SetActive(true);
            rightHandObj.SetActive(true);
        }
        placementUI.SetActive(false);
        selection = 1;
    }

    public void NextPlacement()
    {
        selection++;
        selectionTxt.text = selection.ToString();
    }

    /*public void ToggleLayout()
    {
        leftLayout.ignoreLayout = true;
        leftHandObj.transform.position = originalPosLeft;
        rightLayout.ignoreLayout = true;
        rightHandObj.transform.position = originalPosRight;
    }*/
}
