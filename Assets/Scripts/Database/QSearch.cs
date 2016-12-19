using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour {
	string input, criteria;
	string[] result;
	SubjectiveExam sExam = new SubjectiveExam();

	// Use this for initialization
	void Start () {
	}

	public void OnClick() {
		input = gameObject.GetComponent<InputField> ().text;
		//Debug.Log (input);

		string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";//Connecting to database
		IDbConnection dbconn = new SqliteConnection(conn);
		dbconn.Open ();

		if (input != null) 
		{
			result = input.Split (' ');
			/*
			IDbCommand dbcmd = dbconn.CreateCommand ();

			string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE Question LIKE '" + input + "%'"; 
			dbcmd.CommandText = sqlQuery;
			IDataReader reader = dbcmd.ExecuteReader ();

			while (reader.Read ()) {
				int num = reader.GetInt32 (0);
				string question = reader.GetString (1);
				Debug.Log (num + ", " + question + " Response: " + sExam.answers [num]);
			}
			reader.Close ();
			reader = null;
			dbcmd.Dispose ();
			dbcmd = null;
			dbconn.Close ();
			dbconn = null;
			*/
			foreach (string str in result) {
				if (!string.IsNullOrEmpty (criteria))
					criteria += "  OR ";
				criteria += "Question LIKE '%" + input + "%' ";
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
			//reader = null;
			dbcmd.Dispose ();
			//dbcmd = null;
			dbconn.Close ();
			dbconn.Dispose ();
			//dbconn = null;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
