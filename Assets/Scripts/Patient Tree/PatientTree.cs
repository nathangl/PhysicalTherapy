using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PatientTree : MonoBehaviour
{

    static TreeNode root = new TreeNode { Value = "Patient" };

    public Dropdown userDropdown;

    void Start()
    {
        root.Nodes.Add(new TreeNode { Value = "UpperExtremity" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Shoulder" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "LeftShoulder" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "RightShoulder" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Elbow" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Hand" });
        root.Nodes.Add(new TreeNode { Value = "LowerExtremity" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "RIGHT" });
        root.Nodes.Add(new TreeNode { Value = "Side" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "LEFT" });
        root.Nodes[2].Nodes.Add(new TreeNode { Value = "RIGHT" });

    }

    static public void DetermineClick(string clicked)
    {
        Action<TreeNode> traverse = null;

        traverse = (n) => {
            if (n.Value == clicked)
            {
                Debug.Log(n.Value + " CLICKED!");
                foreach (TreeNode nv in n.Nodes)
                {
                    Debug.Log(nv.Value);
                }

            }
            else
            {
                n.Nodes.ForEach(traverse);
            }

        };

        traverse(root);
    }

    public void DropdownUI()
    {
        userDropdown.gameObject.SetActive(true);
        userDropdown.transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, Input.mousePosition.z);
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
