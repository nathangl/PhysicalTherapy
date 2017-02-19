using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class HintTimer : MonoBehaviour
{
    QSearch qSearch;
    public Text timetest;
    bool startTimer = false;
    public int inactiveTime = 15;   //Time before someone is counted as inactive
                                    //float TotalTime=0f;				//Total time of the subjective exam
    float TimeSinceClick = 0f;      //Time between Clicking to enter a question 
    int oldAsked = 0;                   //Stores how many quesations were in the Asked list before OnClick
    int totalHints = 0;

    HintTreeNode root = new HintTreeNode { Value = "Hints" };

    // Use this for initialization
    void Start()
    {
        CreateHintTree();
        qSearch = GetComponent<QSearch>();
    }

    void Update()
    {
        if (startTimer == true && qSearch.Asked.Count != 6)
        {
            TimeSinceClick += Time.deltaTime;
            timetest.text = TimeSinceClick.ToString();

            if (TimeSinceClick >= inactiveTime)
            {
                GiveHint();
                TimeSinceClick = 0f;
            }

        }
    }

    public void OnClick()
    {
        if (qSearch.instructorQ == true)
        {
            if (qSearch.Asked.Count > oldAsked)
            {
                TimeSinceClick = 0f;
                oldAsked = qSearch.Asked.Count;
            }
            startTimer = true;
        }
    }

    void GiveHint()
    {
        bool Done = false;
        totalHints++;
        Action<HintTreeNode, string> SubjectiveTraverse = null;

        SubjectiveTraverse = (n, c) => {
            int result;
            int.TryParse(n.Value, out result);
            if (qSearch.Asked.Contains(result) || n.Value == "Hints")
            {
                foreach (HintTreeNode node in n.Nodes)
                    SubjectiveTraverse(node, c);
            }
            else
            {
                foreach (HintTreeNode nv in n.Nodes)
                {
                    if (nv.Given == false && nv.Criteria == c && Done == false)
                    {
                        qSearch.scrollRect.verticalNormalizedPosition = 0.0f;
                        qSearch.textArea.text += nv.Value + "\n\n";
                        nv.Given = true;
                        Done = true;
                        break;
                    }
                }
            }
            //round++;
            //Debug.Log(round, "Subjective");
        };

        SubjectiveTraverse(root, "Subjective");

    }

    void CreateHintTree()
    {
        root.Nodes.Add(new HintTreeNode { Value = "1", Criteria = "Subjective" });
        root.Nodes[0].Nodes.Add(new HintTreeNode { Value = "Question 1 hint placeholder1", Criteria = "Subjective" });
        root.Nodes[0].Nodes.Add(new HintTreeNode { Value = "Question 1 hint placeholder2", Criteria = "Subjective" });
        root.Nodes.Add(new HintTreeNode { Value = "2", Criteria = "Subjective" });
        root.Nodes[1].Nodes.Add(new HintTreeNode { Value = "Question 2 hint placeholder1", Criteria = "Subjective" });
        root.Nodes.Add(new HintTreeNode { Value = "3", Criteria = "Subjective" });
        root.Nodes[2].Nodes.Add(new HintTreeNode { Value = "Question 3 hint placeholder1", Criteria = "Subjective" });
        root.Nodes[2].Nodes.Add(new HintTreeNode { Value = "Question 3 hint placeholder2", Criteria = "Subjective" });
    }
}

class HintTreeNode
{
    public string Value { get; set; }
    public bool Given { get; set; }             //Tracks how many times a hint is given
    public List<HintTreeNode> Nodes { get; set; }
    public string Criteria { get; set; }

    public HintTreeNode()
    {
        Nodes = new List<HintTreeNode>();
        Given = false;
    }
}