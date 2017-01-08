using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTimer : MonoBehaviour {
	QSearch qSearch;
	public Text timetest;
	bool startTimer=false;
	public int inactiveTime = 15; 		//Time before someone is counted as inactive
	float TotalTime=0f;			//Total time of the subjective exam
	float TimeSinceClick=0f;		//Time between Clicking to enter a question 

	// Use this for initialization
	void Start () {
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
		TimeSinceClick = 0f;
		startTimer = true;
	}

	void GiveHint(){
		Debug.Log ("Hint Placeholder");
	}
}
