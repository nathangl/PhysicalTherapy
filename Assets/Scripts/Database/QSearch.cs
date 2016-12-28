﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour {
	string input;
	string[] result;
	public Text textArea;
	//SubjectiveExam sExam;
	Keywords keywords;
	public string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
	public string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise" };

	// Use this for initialization
	void Start () {
		textArea.text = "";
		keywords = gameObject.GetComponent<Keywords> ();
		//sExam = gameObject.GetComponent<SubjectiveExam> ();
	}

	public void OnClick() {

		/* Old working code
		string criteria="";
		input = gameObject.GetComponent<InputField> ().text;
		//Debug.Log (input);

		string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";//Connecting to database
		IDbConnection dbconn = new SqliteConnection(conn);
		dbconn.Open ();

		if (input != null) 
		{
			result = input.Split (' ');
			foreach (string str in result) {
				if (!string.IsNullOrEmpty (criteria))
					criteria += "  AND ";
				criteria += "Question LIKE '%" + str + "%' ";
			}
			string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE " + criteria;
			Debug.Log (criteria);
			IDbCommand dbcmd = dbconn.CreateCommand ();
			dbcmd.CommandText = sqlQuery;
			IDataReader reader = dbcmd.ExecuteReader ();

			while (reader.Read ()) {
				int num = reader.GetInt32 (0);
				string question = reader.GetString (1);
				Debug.Log (num + ", " + question + " Response: " + sExam.answers [num]);
			}
			reader.Close ();
			dbcmd.Dispose ();
			dbconn.Close ();
			dbconn.Dispose ();
		}
		*/
		input = gameObject.GetComponent<InputField> ().text;
		//Debug.Log (input);

		string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";//Connecting to database
		IDbConnection dbconn = new SqliteConnection(conn);
		dbconn.Open ();

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

				if ((num == 5 || num == 6) && (keywords.NumKeywords < 2)) {
					textArea.text += "Insufficient Question: '" + input + "' Please try again.\n\n";
				}
				else {
					textArea.text += reader ["Question"].ToString () + "\n";
					textArea.text += answers [num] + "\n\n";
				}
			
				/*
				while (reader.Read ()) {
					int num = reader.GetInt32 (0);
					string question = reader.GetString (1);
					if ((num == 5 || num == 6) && (keywords.NumKeywords < 2))
						Debug.Log ("Wrong");
					else
						Debug.Log (num + ", " + question + " Response: " + answers [num]);
				}
				*/
				reader.Close ();
				dbcmd.Dispose ();
				dbconn.Close ();
				dbconn.Dispose ();
			}
			else
				textArea.text += "Insufficient Question: '" + input + "' Please try again.\n";
		}
	}
}
