using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour {
	string input;
	string[] result;
	SubjectiveExam sExam = new SubjectiveExam();
	Keywords keywords = new Keywords();
	// Use this for initialization
	void Start () {
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

				while (reader.Read ()) {
					int num = reader.GetInt32 (0);
					string question = reader.GetString (1);
					if ((num == 5 || num == 6) && (keywords.NumKeywords < 2))
						Debug.Log ("Wrong");
					else
						Debug.Log (num + ", " + question + " Response: " + sExam.answers [num]);
				}
				reader.Close ();
				dbcmd.Dispose ();
				dbconn.Close ();
				dbconn.Dispose ();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
