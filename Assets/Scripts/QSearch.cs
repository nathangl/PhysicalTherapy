using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour
{
    string input, prevInput = null;
    public List<int> Asked = new List<int>(); //stores which questions have been asked already
    string[] result;
    public Text textArea;
    public Text totalAskedText;
    public ScrollRect scrollRect;
    public GameObject AROM, PROM, Strength;
    public PatientController pController; //Need to get the currentScreen
    public Animator pAnimator;
    public bool testing = false;
    public bool SubjectiveBegun = false;
    [HideInInspector]
    public bool instructorQ = false;   //So you know if the first question has been answered
    string instructorQanswer; //Stores the users answer to the instructorQ
    AnimatorStateInfo pAnimInfo;
    Keywords keywords;
    //public string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
    [HideInInspector]
    public string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise", "7", "8", "9", "10" };
    [HideInInspector]
    public string[] instructorQanswers = { "left sided hemiparesis", "decreased left side strength", "decreased left side sensation", "left visual neglect", "low tone", "low level of alertness" };

    // Use this for initialization
    void Start()
    {
        textArea.text = "Before beginning the patient examination please answer the following question:\nWhat impairments might you hypothesize are present in this patient before you begin your screen?\n\n";
        keywords = gameObject.GetComponent<Keywords>();
        //sExam = gameObject.GetComponent<SubjectiveExam> ();
    }

    void Update()
    {
        if (Asked.Count == 6 || testing == true)
        {
            AROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            PROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            Strength.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
        }
    }

    public void OnClick()
    {
        string tableName;
        input = gameObject.GetComponent<InputField>().text;
        Debug.Log("input:" + input);
        scrollRect.verticalNormalizedPosition = 0.0f;                       //moves scroll rect to bottom
        if (input != "IAMTESTING")                                          //demo purposes
        {
            if (input != "")
            {
                tableName = instructorQ ? "SubjectiveQuestions" : "AnswerData";
                DBQuery(tableName);
                //prevInput = input;
            }
            else
                textArea.text += "ERROR: Nothing Entered.\n\n";
        }
        else
        {
            instructorQ = true;
            testing = true;
        }
    }
    //Creates a query and queries the databse to get question responses
    void DBQuery(string tableName)
    {
        string temp;
        bool approved;
        input.Replace("?", "");
        result = input.Split(' ');
        temp = keywords.FindKeywords(result);

        string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";  //Connecting to database
        IDbConnection dbconn = new SqliteConnection(conn);
        dbconn.Open();
        try
        {
            string sqlQuery = "SELECT * " + "FROM " + tableName + " WHERE " + temp;

            IDbCommand dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            reader.Read();
            int num = reader.GetInt32(0);
            string question = reader.GetString(1);

            approved = Approve(num);
            if (!instructorQ)
            {
                if (approved)
                {
                    //Scoring Placeholder
                    textArea.text += "correct";
                }
                else
                {
                    textArea.text += "incorrect";
                    //Scoring Placeholder
                }
                instructorQanswer = input;
                textArea.text += "Thanks you, you may now move on to the subjective exam.\n\n";
                instructorQ = true;
            }
            else if (instructorQ)
            {
                if (approved)
                {
                    PlayAnims(num);
                    OutputAnswer(num, reader);
                }
                else if (!approved)
                {
                    textArea.text += "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n";
                }
                SubjectiveBegun = true;
            }

            reader.Close();
            dbcmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
        }
        catch (Exception e)
        {
            textArea.text += instructorQ ? "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n" : "Thanks you, you may now move on to the subjective exam.\n\n";
            instructorQ = true;
            Debug.Log("Error: " + e.ToString());
        }
    }
    //Checks if there are enough keywords to retrieve the right question
    bool Approve(int num)
    {
        bool approved = false;
        if (instructorQ)
        {
            switch (num)
            {
                case 5:
                case 9:
                case 10:
                    approved = keywords.NumKeywords < 2 ? false : true;
                    break;
                case 7:
                    approved = keywords.NumKeywords < 3 ? false : true;
                    break;
                case 8:
                    approved = keywords.NumKeywords < 6 ? false : true;
                    break;
                default:
                    approved = true;
                    break;
            }
        }
        else
        {
            switch (num)
            {
                case 2:
                case 3:
                    approved = keywords.NumKeywords < 3 ? false : true;
                    break;
                default:
                    approved = keywords.NumKeywords < 2 ? false : true;
                    break;
            }
        }
        return approved;
    }
    //Plays an animation if the question asked requires it
    void PlayAnims(int num)
    {
        pAnimInfo = pAnimator.GetCurrentAnimatorStateInfo(0);
        if (num == 3)
        {
            if ((pController.currentScreen == "PROM") && (pAnimInfo.IsName("PROMLeftArm")) && (pAnimInfo.normalizedTime > 0.3f))
            {
                answers[num] = "Yes";
            }
            else
                answers[num] = "No";
        }
        else if (num == 9)
        {
            pAnimator.Play("LookLeft", 1);
        }
    }

    void SetList(int num)
    { //Sets the Asked list to keep track of what questions have been asked
        if (Asked.Contains(num) == false)
        {
            Asked.Add(num);
            totalAskedText.text = "Questions Asked " + Asked.Count + "/10";
        }
    }

    void OutputAnswer(int num, IDataReader reader)
    {
        SetList(num);
        textArea.text += "User: " + reader["Question"].ToString() + "\n";
        textArea.text += "Patient: " + '"' + answers[num] + '"' + "\n\n";
    }
}
