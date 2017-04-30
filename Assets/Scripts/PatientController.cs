using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class PatientController : MonoBehaviour
{

    public Dropdown userDropdown;
    public SphereCollider leftShoulder, rightShoulder;
    private RaycastHit hit;
    TreeNode root = new TreeNode { Value = "Patient" };
    Animator patientAnim;
    string dropdownIndex;
    bool dropdownActive = false;
    string currentTag = "";
    public string currentScreen = "";
    GameObject chart;
    Manager manager;
    public GameObject PROMAnims;
    string prevMode = ""; //Previous mode (AROM/PROM)
    public HandManager handManager;
    bool dropdownEnabled = true;
    bool PROMActive = false;
    DotControlller dotController;

    void Awake()
    {
        chart = GameObject.FindGameObjectWithTag("Chart");
        manager = chart.GetComponent<Manager>();
        //handManager = GameObject.Find("Hands").GetComponent<HandManager>();
        patientAnim = GetComponent<Animator>();
        dotController = GetComponent<DotControlller>();
    }

    void Start()
    {
        CreatePatientTree();
    }

    void Update()
    {
        if (currentScreen != prevMode)
        {
            patientAnim.speed = 1;
            patientAnim.Play("Idle2");
            prevMode = currentScreen;
        }
        if (currentScreen != "PROM")
        {
            PROMAnims.SetActive(false);
            PROMActive = false;
        }
        else if (handManager.success == true)
        {
            dotController.DisableDots();

            PROMAnims.SetActive(true);
            PROMActive = true;
        }
        if (PROMActive = true && handManager.success == true)
            handManager.success = false;
        //TODO: fix weird spacing
        if (Input.GetMouseButtonDown(0) && dropdownEnabled) // if left mouse clicked
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // make a raycast to clicked position

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // if the raycast hit something
            {
                if (currentScreen != "")
                {
                    if (currentTag != hit.collider.transform.tag && dropdownActive == true) //if raycast hits a new collider
                    {
                        currentTag = hit.collider.transform.tag;
                        SetDropdown(hit.collider.transform.tag);
                    }
                    else if (dropdownActive == false)   //if raycast hits a collider
                    {
                        currentTag = hit.collider.transform.tag;
                        SetDropdown(hit.collider.transform.tag);   // if (hit.collider.transform.tag == "Patient")
                    }
                }
            }

            if (dropdownActive && EventSystem.current.currentSelectedGameObject == null && currentScreen == "")
            {
                /*dropChecker = EventSystem.current.currentSelectedGameObject.layer;    //get gameobject's layer
                if (dropChecker != 8)
                {
                    DisableDropdown();
                }*/
                DisableDropdown();
            }
            else if (EventSystem.current.currentSelectedGameObject)
            {
                if (EventSystem.current.currentSelectedGameObject.layer != 8)
                {
                    DisableDropdown();
                }
            }
        }
    }

    public void SetDropdown(string clicked)
    {
        if(clicked == "Untagged")
        {
            return;
        }
        Debug.Log(clicked);
        List<string> dropdownList = new List<string>();
        Action<TreeNode> traverse = null;
        traverse = (n) => {
            if (n.Value == clicked)
            {
                Debug.Log(n.Value + " CLICKED!");
                dropdownIndex = n.Value;
                dropdownList.Add(n.Value);
                foreach (TreeNode nv in n.Nodes)
                {
                    //Debug.Log(nv.Value);
                    dropdownList.Add(nv.Value);
                }
            }
            else
            {
                n.Nodes.ForEach(traverse);
            }

        };

        traverse(root);

        userDropdown.gameObject.SetActive(true);
        dropdownActive = true;
        userDropdown.transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, 0);
        Debug.Log(new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, Input.mousePosition.z));
        userDropdown.ClearOptions();
        userDropdown.AddOptions(dropdownList);
    }

    public void DetermineDropdown()
    {
        if (userDropdown.value == 0)
        {
            return;
        }

        else if (userDropdown.value == 1 && currentScreen == "AROM")        //AROM
        {
            dotController.DisableDots();

            Debug.Log("AROM Flexion Animation Accessed");
            if (dropdownIndex == "LeftShoulder")
            {
                patientAnim.SetTrigger("AROMLeftArm");
            }
            if (dropdownIndex == "RightShoulder")
            {
                patientAnim.SetTrigger("AROMRightArm");
            }
            currentScreen = "";
        }
        else if (userDropdown.value == 1 && currentScreen == "PROM")
        {
            dotController.EnableHandDots();

            Debug.Log("PROM Animation Accessed");
            handManager.ToggleHands();
            if (currentTag == "LeftShoulder")
                handManager.currentlyTesting = "leftprom";
            else if (currentTag == "RightShoulder")
                handManager.currentlyTesting = "rightprom";
            dropdownEnabled = false;
        }/*
        else if (userDropdown.value == 1)
        {
            currentScreen = "AROM";
            Debug.Log("AROM MODE");
        }*/

        /*else if (userDropdown.value == 2 && currentScreen == "PROM")       //PROM
        {
            Debug.Log("PROM Animation Accessed");
            if (dropdownIndex == "LeftShoulder")
            {
                patientAnim.SetTrigger("PROMLeftArm");
            }
            if (dropdownIndex == "RightShoulder")
            {
                patientAnim.SetTrigger("PROMRightArm");
            }
            currentScreen = "";
        }*/
        else if (userDropdown.value == 2 && currentScreen == "")
        {
            currentScreen = "PROM";
            Debug.Log("PROM MODE");
        }
        else
            Debug.Log("Not Yet Implemented");

        DisableDropdown();
    }

    public void DisableDropdown()
    {
        userDropdown.value = 0;
        userDropdown.transform.position = new Vector3(10000, 0, 0);
        dropdownActive = false;
        //userDropdown.gameObject.SetActive(false);
    }

    public void ButtonInput(string mode)
    {
        currentScreen = "";
        PROMActive = false;
        PROMAnims.SetActive(false);
        dotController.DisableDots();
        if (mode != "chart")
        {
            currentScreen = mode;
            dropdownEnabled = true;
            dotController.EnableDropdownDots();
        }
        if(handManager.active)
        {
            handManager.ToggleHands();
        }
        Debug.Log(mode + " MODE");
    }

    public void CreatePatientTree()
    {
        root.Nodes.Add(new TreeNode { Value = "UpperExtremity" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Shoulder" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "LeftShoulder" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "RightShoulder" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Choose" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "AROM" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "PROM" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "Strength" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "Tone" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "Sensation" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "Visual-spatial" });
        root.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "Findings" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Elbow" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Hand" });
        root.Nodes.Add(new TreeNode { Value = "LowerExtremity" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "RIGHT" });
        root.Nodes.Add(new TreeNode { Value = "Side" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "RIGHT" });
    }
}

class TreeNode
{
    public string Value { get; set; }
    public List<TreeNode> Nodes { get; set; }


    public TreeNode()
    {
        Nodes = new List<TreeNode>();
    }
}