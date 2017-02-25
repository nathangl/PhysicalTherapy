using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour {
	string input, prevInput=null;
	public List<int> Asked = new List<int>(); //stores which questions have been asked already
	string[] result;
	public Text textArea;
	public Text totalAskedText;
	public ScrollRect scrollRect;
    public GameObject AROM, PROM, Strength;
    public bool testing=false;

    public bool instructorQ = false;   //So you know if the first question has been answered
    string instructorQanswer; //Stores the users answer to the instructorQ
	//SubjectiveExam sExam;
	Keywords keywords;
	//public string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
	public string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise" };
    public string[] instructorQanswers = { "left sided hemiparesis", "decreased left side strength", "decreased left side sensation", "left visual neglect", "low tone", "low level of alertness"};

	// Use this for initialization
	void Start () {
		textArea.text = "Before beginning the patient examination please answer the following question:\nWhat impairments might you hypothesize are present in this patient before you begin your screen?\n\n";
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
        Debug.Log("input:" + input);
		string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";//Connecting to database
		IDbConnection dbconn = new SqliteConnection(conn);
		dbconn.Open ();
		scrollRect.verticalNormalizedPosition = 0.0f; //moves scroll rect to bottom
        if (input != prevInput)
        {
            if (instructorQ == true)
            {
                if (input != "")
                {
                    //string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE ";
                    string temp;
                    while (input.EndsWith("?"))
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    result = input.Split(' ');
                    temp = keywords.FindKeywords(result);
                    if (temp != "")
                    {
                        string sqlQuery = "SELECT * " + "FROM SubjectiveQuestions " + "WHERE " + temp;
                        Debug.Log(sqlQuery);

                        IDbCommand dbcmd = dbconn.CreateCommand();
                        dbcmd.CommandText = sqlQuery;
                        IDataReader reader = dbcmd.ExecuteReader();

                        reader.Read();

                        int num = reader.GetInt32(0);
                        string question = reader.GetString(1);
                        SetList(num);
                        if (((num == 5) || (num == 9) || (num == 10)) && (keywords.NumKeywords < 2) || (num == 7) && (keywords.NumKeywords < 3)
                            || (num == 8) && (keywords.NumKeywords < 6))
                        {
                            textArea.text += "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n";
                        }
                        else
                        {
                            textArea.text += "User: " + reader["Question"].ToString() + "\n";
                            textArea.text += "Patient: " + '"' + answers[num] + '"' + "\n\n";
                        }

                        reader.Close();
                        dbcmd.Dispose();
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                    else
                        textArea.text += "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n";
                }
            }
            else //For instructorQ
            {
                if (input != "")
                {
                    string temp;
                    while (input.EndsWith("?"))
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    result = input.Split(' ');
                    temp = keywords.FindKeywords(result);
                    if (temp != "")
                    {
                        try
                        {
                            Debug.Log(result[0]);
                            //Need to add scoring here later
                            string sqlQuery = "SELECT * " + "FROM AnswerData " + "WHERE " + temp;
                            Debug.Log(sqlQuery);

                            IDbCommand dbcmd = dbconn.CreateCommand();
                            dbcmd.CommandText = sqlQuery;
                            IDataReader reader = dbcmd.ExecuteReader();

                            reader.Read();
                            int num = reader.GetInt32(0);
                            string question = reader.GetString(1);

                            if (((num == 2 || num == 3) && (keywords.NumKeywords < 3)) || keywords.NumKeywords < 2)
                            {
                                //This means answer wasnt correct or efficient enough so minus points
                                Debug.Log("Answer not good enough");
                            }
                            else
                            {
                                //Answer was correct
                                Debug.Log("Correct");
                            }

                            reader.Close();
                            dbcmd.Dispose();
                            dbconn.Close();
                            dbconn.Dispose();
                        }
                        catch(Exception e){
                            Debug.Log(e.ToString());
                        }
                    }
                    instructorQanswer = input;
                    textArea.text += "Thanks you, you may now move on to the subjective exam.\n\n";
                    instructorQ = true;
                }
                else
                {
                    textArea.text += "ERROR: Please enter a response.\n\n";
                }
            }
            prevInput = input;
        }
	}

	void SetList(int num) { //Sets the Asked list to keep track of what questions have been asked
		if (Asked.Contains (num) == false) {
			Asked.Add (num);
			totalAskedText.text = "Questions Asked " + Asked.Count + "/10";
		} 
	}
}
