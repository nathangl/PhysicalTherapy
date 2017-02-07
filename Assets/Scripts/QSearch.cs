﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour {
	string input;
	public List<int> Asked = new List<int>(); //stores which questions have been asked already
	string[] result;
	public Text textArea;
	public Text totalAskedText;
	public ScrollRect scrollRect;
    public GameObject AROM, PROM, Strength;
    public bool testing=false;
	//SubjectiveExam sExam;
	Keywords keywords;
	//public string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
	public string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise" };

	// Use this for initialization
	void Start () {
		textArea.text = "";
		keywords = gameObject.GetComponent<Keywords> ();
		//sExam = gameObject.GetComponent<SubjectiveExam> ();
	}

    void Update()
    {
        if (Asked.Count == 6 || testing==true)
        {
            AROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            PROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            Strength.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
        }
    }

	public void OnClick() {
		input = gameObject.GetComponent<InputField> ().text;
		string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";//Connecting to database
		IDbConnection dbconn = new SqliteConnection(conn);
		dbconn.Open ();
		scrollRect.verticalNormalizedPosition = 0.0f; //moves scroll rect to bottom

		if (input != null) 
		{
			//string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE ";
			string temp;
			while (input.EndsWith("?")) {
				input = input.Remove(input.Length - 1);
			}
			result = input.Split (' ');
			temp = keywords.FindKeywords (result);
			if (temp != "") {
				string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE " + temp;
				Debug.Log (sqlQuery);

				IDbCommand dbcmd = dbconn.CreateCommand ();
				dbcmd.CommandText = sqlQuery;
				IDataReader reader = dbcmd.ExecuteReader ();

				reader.Read ();	

				int num = reader.GetInt32 (0);
				string question = reader.GetString (1);
				SetList (num);
				if ((num == 5 || num == 6) && (keywords.NumKeywords < 2)) {
					textArea.text += "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n";
				}
				else {
					textArea.text += "User: " + reader ["Question"].ToString () + "\n";
					textArea.text += "Patient: " + '"' + answers [num] + '"' + "\n\n";
				}

				reader.Close ();
				dbcmd.Dispose ();
				dbconn.Close ();
				dbconn.Dispose ();
			}
			else
				textArea.text += "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n";
		}
	}

	void SetList(int num) { //Sets the Asked list to keep track of what questions have been asked
		if (Asked.Contains (num) == false) {
			Asked.Add (num);
			totalAskedText.text = "Questions Asked " + Asked.Count + "/6";
		} 
	}
}
