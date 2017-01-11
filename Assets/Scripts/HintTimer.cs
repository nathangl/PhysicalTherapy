using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class HintTimer : MonoBehaviour {
	QSearch qSearch;
	public Text timetest;
	bool startTimer=false;
	public int inactiveTime = 15; 	//Time before someone is counted as inactive
	float TotalTime=0f;				//Total time of the subjective exam
	float TimeSinceClick=0f;		//Time between Clicking to enter a question 
	int oldAsked=0;					//Stores how many quesations were in the Asked list before OnClick
	int totalHints=0;

	HintTreeNode root = new HintTreeNode { Value = "Hints" };

	// Use this for initialization
	void Start () {
		CreateHintTree ();
		qSearch = GetComponent<QSearch> ();
	}

	void Update() {
		if (startTimer == true && qSearch.Asked.Count != 6) {
			TimeSinceClick += Time.deltaTime;
			timetest.text = TimeSinceClick.ToString();

			if (TimeSinceClick >= inactiveTime) {
				GiveHint ();
				TimeSinceClick = 0f;
			}

		}
	}

	public void OnClick () {
		if (qSearch.Asked.Count > oldAsked) {
			TimeSinceClick = 0f;
			oldAsked = qSearch.Asked.Count;
		}
		startTimer = true;
	}

	void GiveHint(){
		Debug.Log ("Hint Placeholder");
		totalHints++;
		/*
		Action<HintTreeNode> traverse = null;
		traverse = (n) => {
			if (n.Value == "Question 1") {
				foreach (HintTreeNode nv in n.Nodes) {
					Debug.Log (nv.Value + " Before: " + nv.Given + "\n");
					nv.Given += 1;
					Debug.Log (nv.Value + " After: " + nv.Given + "\n");
				}
			}
			else {
				n.Nodes.ForEach (traverse);
			}
		};
		traverse (root);
		*/
	}

	void CreateHintTree() {
		root.Nodes.Add (new HintTreeNode { Value = "Question 1" });
		root.Nodes [0].Nodes.Add (new HintTreeNode { Value = "Question 1 hint placeholder 1" });
		root.Nodes [0].Nodes.Add (new HintTreeNode { Value = "Question 1 hint placeholder 2" });
		root.Nodes.Add (new HintTreeNode { Value = "Question 2" });
		root.Nodes [1].Nodes.Add (new HintTreeNode { Value = "Question 2 hint placeholder" });
	}
}

class HintTreeNode
{
	public string Value { get; set; }
	public int Given { get; set; }				//Tracks how many times a hint is given
	public List<HintTreeNode> Nodes { get; set; }
	public string Criteria { get; set; }

	public HintTreeNode()
	{
		Nodes = new List<HintTreeNode>();
		Given = 0;
	}
}