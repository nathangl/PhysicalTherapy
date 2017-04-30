using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;

public class QSearch : MonoBehaviour
{
    int hypothesisCount = 1;                    //Count answers to instructorQ
    string input;//, prevInput = null;
    public static List<int> Asked = new List<int>();   //stores which questions have been asked already
    string[] result;
    public Text textArea;
    public Text totalAskedText;
    public ScrollRect scrollRect;
    public GameObject AROM, PROM, Strength;
    public PatientController pController;       //Need to get the currentScreen
    public Animator pAnimator;
    public bool testing = false;
    public static bool SubjectiveBegun = false;
    [HideInInspector]
    public static bool instructorQ = false;            //So you know if the first question has been answered
    //string instructorQanswer;                   //Stores the users answer to the instructorQ
    AnimatorStateInfo pAnimInfo;
    //Keywords keywords;
    //public string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
    [HideInInspector]
    public string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise", "7", "8", "9", "10" };
    [HideInInspector]
    public string[] instructorQanswers = { "left sided hemiparesis", "decreased left side strength", "decreased left side sensation", "left visual neglect", "low tone", "low level of alertness" };
    //public GameObject model1;
    //public GameObject model2;
    // Use this for initialization
    void Awake()
    {
        textArea.text = "Before beginning the patient examination please answer the following question:\nWhat are at least 3 impairments you hypothesize are present in this patient before you begin your screen?\n\n";
        //scrollRect = GetComponent<>
        //keywords = gameObject.GetComponent<Keywords>();
    }

    void Update()
    {
        //Activate AROM/PROM/Strength button after 6 subjective questions are asked
        if (Asked.Count == 6 || testing == true)
        {
            ActivateModel2();
            AROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            PROM.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
            Strength.GetComponent<Manager>().OnOff(AROM.GetComponent<Manager>().menuOn = true);
        }
    }
    //Called when user clicks button to submit dialogue
    public void OnClick()
    {
        string tableName; //Name of table to query
        input = gameObject.GetComponent<InputField>().text;
        Debug.Log("input:" + input);
        scrollRect.verticalNormalizedPosition = 0.0f; //moves scroll rect to bottom
        if (input != "IAMTESTING") //demo purposes
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
            ActivateModel2();
            instructorQ = true;
            testing = true;
        }
        Tracker.LogData(input);
    }
    //Creates a query and queries the databse to get patient responses to questions
    void DBQuery(string tableName)
    {
        string temp;
        bool approved;
        //Remove excess question marks
        input.Replace("?", "");
        //Split the input into an array
        result = input.Split(' ');
        temp = Keywords.FindKeywords(result, instructorQ);   //Finds keywords and returns a string to complete SQL query

        string conn = "URI=file:" + Application.dataPath + "/UserDB.s3db";  //Connecting to database
        IDbConnection dbconn = new SqliteConnection(conn);
        dbconn.Open();
        try
        {
            string sqlQuery = "SELECT * FROM " + tableName + " WHERE " + temp;

            IDbCommand dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            reader.Read();
            int num = reader.GetInt32(0);
            //string question = reader.GetString(1);

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
                //instructorQanswer = input;
                //textArea.text += "Thanks you, you may now move on to the subjective exam.\n\n";
                //instructorQ = true;
                textArea.text += "Response " + hypothesisCount + ": " + input + "\n\n";
                hypothesisCount++;
            }
            else
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
            if (hypothesisCount > 5 && instructorQ == false)
            {
                instructorQ = true;
                textArea.text += "Thanks you, you may now move on to the subjective exam.\n\n";
                textArea.text += "Please use this box to ask the patient at least six subjective questions before moving on.\n\n";
            }
            reader.Close();
            dbcmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
        }
        catch (Exception e)
        {
            textArea.text += instructorQ ? "ERROR: Insufficient Question: '" + input + "' Please try again.\n\n" : "Response " + hypothesisCount + ": " + input + "\n\n";
            //instructorQ = true;
            hypothesisCount++;
            if (hypothesisCount >= 4 && instructorQ == false)
            {
                instructorQ = true;
                textArea.text += "Thanks you, you may now move on to the subjective exam.\n\n";
                textArea.text += "Please use this box to ask the patient at least six subjective questions before moving on.\n\n";

            }
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
                    approved = Keywords.NumKeywords < 2 ? false : true;
                    break;
                case 7:
                    approved = Keywords.NumKeywords < 3 ? false : true;
                    break;
                case 8:
                    approved = Keywords.NumKeywords < 6 ? false : true;
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
                    approved = Keywords.NumKeywords < 3 ? false : true;
                    break;
                default:
                    approved = Keywords.NumKeywords < 2 ? false : true;
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
        else if(num == 8)
        {
            ActivateModel2();
        }
        else if (num == 9)
        {
            pAnimator.Play("LookLeft", 1);
        }
    }
    //Sets the Asked list to keep track of what questions have been asked
    void SetList(int num)
    {
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

    void ActivateModel2()
    {
        //model1.SetActive(false);
        //model2.SetActive(true);
        //pController = model2.GetComponent<PatientController>();
        //pAnimator = model2.GetComponent<Animator>();
        pAnimator.SetTrigger("Idle2");
    }
}
