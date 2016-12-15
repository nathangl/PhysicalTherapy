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

    void Awake()
    {
        patientAnim = GetComponent<Animator>();
    }

    void Start()
    {
        root.Nodes.Add(new TreeNode { Value = "UpperExtremity" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Shoulder" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "LeftShoulder" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "AROM" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "PROM" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "RightShoulder" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "AROM" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "PROM" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Elbow" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Hand" });
        root.Nodes.Add(new TreeNode { Value = "LowerExtremity" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "RIGHT" });
        root.Nodes.Add(new TreeNode { Value = "Side" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "RIGHT" });
    }

    void Update()
    {
        //TODO: fix weird spacing
        if (Input.GetMouseButtonDown(0)) // if left mouse clicked
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // make a raycast to clicked position

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // if the raycast hit something
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
    }

    public void SetDropdown(string clicked)       //static
    {
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
        userDropdown.transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, Input.mousePosition.z);
        userDropdown.ClearOptions();
        userDropdown.AddOptions(dropdownList);
    }

    public void PlayPatientAnim()
    {
        if (userDropdown.value == 0)
        {
            return;
        }

        if (userDropdown.value == 1)        //AROM
        {
            Debug.Log("AROM Animation Accessed");
            if (dropdownIndex == "LeftShoulder")
            {
                patientAnim.SetTrigger("AROMLeftArm");
            }
            if (dropdownIndex == "RightShoulder")
            {
                patientAnim.SetTrigger("AROMRightArm");
            }
        }

        else if (userDropdown.value == 2)       //PROM
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
        }

        else
            Debug.Log("Error at animationcontroller");

        //userDropdown.gameObject.SetActive(false);
        userDropdown.value = 0;
        userDropdown.transform.position = new Vector3(1000, 0, 0);
        dropdownActive = false;
    }

    public void DisableDropdown()
    {
        userDropdown.gameObject.SetActive(false);
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