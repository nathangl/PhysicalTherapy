using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
// Student and Question namespaces
using StudentsLevelClass;
using QuestionAnswer;


public class LoadingUserPreviousActivities : MonoBehaviour
{

    public static LoadingUserPreviousActivities control;

    public int score;
    public int User_ID;
    public int user_question_ID_answered, tempQuestionId;
    public Text question_to_be_asked;
    public GameObject notificationUI;
    public Text continueButton;
    //correct answer to question
    private string tempCorrectAnswer;   //no clue

    //Green&Blue_Toggle buttons
    public Toggle First_Answer_Toggle, Second_Answer_Toggle,
        Third_Answer_Toggle, Fourths_Answer_Toggle;
    public Text First_Answer, Second_Answer,
         Third_Answer, Fourths_Answer;


    //singeltone
    void Start()        //was Awake() changed to Start() nadd
    {
        /*
        //if there is NotificationServices game control 
        // make this as game control 
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            //if control already exist
            // and this is not it then destroy
        }
        else if (control != this)
        {
            Destroy(gameObject);
        
    */
        //Load User Previous performance
        Load();
    }

    void OnGUI()
    {
        //if(GUI)
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + score);
        GUI.Label(new Rect(10, 40, 150, 30), "User_ID: " + UserInputCheck.User_ID_All_Level);
        //removed buttons and random debugs
    }

   
    public void Load()
    {
        string path = Application.persistentDataPath +
            "/playerLevelInfo.dat";
      //File.Delete(path);
      //if student file already exist
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(
                Application.persistentDataPath +
            "/playerLevelInfo.dat", FileMode.Open);
            while (file.Position != file.Length)
            {
                Level lData = (Level)bf.Deserialize(file);
                if (lData.StudentId == UserInputCheck.User_ID_All_Level)
                {
                    // assign data from file to temporary feilds
                    score = lData.LevelScore;
                    user_question_ID_answered = lData.QuestionId;
                    //question, answer, 
                    Debug.Log(score + "last student question: " +
                        user_question_ID_answered);
                }
            }
            file.Close();
            // Open File contain questions and answers
            string Questionpath = Application.persistentDataPath
         + "/QuestionsAnswersData.dat";
            if (File.Exists(Questionpath))
               {
                /*  BinaryFormatter qbf = new BinaryFormatter();
                  FileStream qfile = File.Open(
                      Questionpath, FileMode.Open);
                  // find the last question user answered
                  //Move it to the begining of the file

                  qfile.Seek(0, SeekOrigin.Begin);
                  while (qfile.Position != qfile.Length)
                    {
                      QuestionsClass qdata = (QuestionsClass)bf.Deserialize(qfile);

                      if (qdata.QuestionId == user_question_ID_answered)
                        {
                          tempQuestionId =qdata.QuestionId + 1; 
                          question_to_be_asked.text = qdata.Question;
                          List<string> shuffle = qdata.PossibleAnswers.ToList();
                          shuffle.Add(qdata.Answer);
                          //Shuffle the answers
                          var shuffledAnswers = shuffle.OrderBy(a => Guid.NewGuid());

                          List<string> items = shuffledAnswers.OrderBy(item => item).ToList();

                          First_Answer.text = items[0].ToString();
                          Second_Answer.text = items[1].ToString();
                          Third_Answer.text = items[2].ToString();
                          Fourths_Answer.text = items[3].ToString();
                          Debug.Log(health + "last student question: " +
                                      user_question_ID_answered);
                          qfile.Close(); */
                // Find the next question to be assigned to user


                LoadNextQuestion(user_question_ID_answered);

                }
                
            

        }
        else
        {
            string Questionpath = Application.persistentDataPath
            + "/QuestionsAnswersData.dat";
            BinaryFormatter qbf = new BinaryFormatter();
            FileStream qfile = File.Open(
                Questionpath, FileMode.Open);
            QuestionsClass qdata = (QuestionsClass)qbf.Deserialize(qfile);
            user_question_ID_answered = 1;
            question_to_be_asked.text = qdata.Question;
            List<string> shuffle = qdata.PossibleAnswers.ToList();
            shuffle.Add(qdata.Answer);
            //Shuffle the answers
            var shuffledAnswers = shuffle.OrderBy(a => Guid.NewGuid());

            List<string> items = shuffledAnswers.OrderBy(item => item).ToList();

            First_Answer.text = items[0].ToString();
            Second_Answer.text = items[1].ToString();
            Third_Answer.text = items[2].ToString();
            Fourths_Answer.text = items[3].ToString();
            qfile.Close();
            User_ID= UserInputCheck.User_ID_All_Level;

        }
        

    }

    public void LoadNextQuestion(int previousQAns)
    {
        //add one to the previous question ID
        previousQAns += 1;
        string Questionpath = Application.persistentDataPath
         + "/QuestionsAnswersData.dat";
        BinaryFormatter qbf = new BinaryFormatter();
        FileStream qfile = File.Open(
            Questionpath, FileMode.Open);
        // find the last question user answered
        //Move it to the begining of the file

        qfile.Seek(0, SeekOrigin.Begin);
        while (qfile.Position != qfile.Length)
        {
            QuestionsClass qdata = (QuestionsClass)qbf.Deserialize(qfile);

            if (qdata.QuestionId == previousQAns)
            {
                user_question_ID_answered = previousQAns;
                question_to_be_asked.text = qdata.Question; 
                List<string> shuffle = qdata.PossibleAnswers.ToList();
                shuffle.Add(qdata.Answer);
                //Shuffle the answers
                var shuffledAnswers = shuffle.OrderBy(a => Guid.NewGuid());
                
               List<string> items = shuffledAnswers.OrderBy(item => item).ToList();
                
                First_Answer.text =  items[0].ToString();
                Second_Answer.text = items[1].ToString();
                Third_Answer.text =  items[2].ToString();
                Fourths_Answer.text = items[3].ToString();

                Debug.Log(score + "last student question: " +
                    user_question_ID_answered);
                qfile.Close();
                //nadd
                return;
            }

        }
    }

    public void SaveUserNewActivities()
    { 

        string pathL = Application.persistentDataPath +
            "/playerLevelInfo.dat";
        //File.Delete(pathL);
        if (!File.Exists(pathL))
        {
            BinaryFormatter bfL = new BinaryFormatter();
            FileStream fileL = File.Create(Application.persistentDataPath +
            "/playerLevelInfo.dat");
            Debug.Log(Application.persistentDataPath);
            Level sLevels = new Level();
            sLevels.StudentId = UserInputCheck.User_ID_All_Level;
            sLevels.QuestionId = 1;
           string lfq= LoadFirstQuestion();
                if(First_Answer_Toggle.isOn && First_Answer.text == lfq)
                {
                    ShowNotification(true);
                    score += 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 1;
                }
                else if (Second_Answer_Toggle.isOn && Second_Answer.text == lfq)
                {
                    ShowNotification(true);
                    score += 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 1;
                }
                else if(Third_Answer_Toggle.isOn && Third_Answer.text == lfq)
                {
                    ShowNotification(true);
                    score += 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 1;


                }
                else if (Fourths_Answer_Toggle.isOn && Fourths_Answer.text == lfq)
                {
                    ShowNotification(true);
                    score += 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 1;

                }
                else
                {
                    ShowNotification(false);
                    score -= 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 0;
                }
            
            bfL.Serialize(fileL, sLevels);
            fileL.Close();
         }
        //find students record
        else
        {
            BinaryFormatter bfL = new BinaryFormatter();
            FileStream fileL = File.Open(Application.persistentDataPath +
            "/playerLevelInfo.dat", FileMode.Append);

            Debug.Log(Application.persistentDataPath);
            Level sLevels = new Level();
            sLevels.StudentId = UserInputCheck.User_ID_All_Level;
            //sLevels.QuestionId =  int.Parse( tempQuestionId.ToString());  //where all problems lie  0, 0
            sLevels.QuestionId = int.Parse(user_question_ID_answered.ToString());

            tempCorrectAnswer = LoadAnswer(sLevels);

            if (First_Answer_Toggle.isOn && First_Answer.text == tempCorrectAnswer)
            {
                ShowNotification(true);
                score += 10;
                sLevels.LevelScore = score;
                sLevels.Success = 1;
            }
            else if (Second_Answer_Toggle.isOn && Second_Answer.text == tempCorrectAnswer)
            {
                ShowNotification(true);
                score += 10;
                sLevels.LevelScore = score;
                sLevels.Success = 1;
            }
            else if (Third_Answer_Toggle.isOn && Third_Answer.text == tempCorrectAnswer)
            {
                ShowNotification(true);
                score += 10;
                sLevels.LevelScore = score;
                sLevels.Success = 1;


            }
            else if (Fourths_Answer_Toggle.isOn && Fourths_Answer.text == tempCorrectAnswer)
            {
                ShowNotification(true);
                score += 10;
                sLevels.LevelScore = score;
                sLevels.Success = 1;

            }
            else
            {
                // check if health greater that 10
                if(score >= 10)
                {
                    ShowNotification(false);
                    score -= 10;
                    sLevels.LevelScore = score;
                    sLevels.Success = 0;
                }else
                {
                    ShowNotification(false);
                    score = 0;
                }
                
            } 
            bfL.Serialize(fileL, sLevels);
            fileL.Close();
            //To BE DONE, rest current forms data
            //resetQuestionForm();
        }


    }

     
    private string LoadFirstQuestion()
    {
        string Questionpath = Application.persistentDataPath
          + "/QuestionsAnswersData.dat";
        BinaryFormatter qbf = new BinaryFormatter();
        FileStream qfile = File.Open(
            Questionpath, FileMode.Open); 
        qfile.Seek(0, SeekOrigin.Begin);
        QuestionsClass qdata = (QuestionsClass)qbf.Deserialize(qfile);
        qfile.Close();
        return qdata.Answer;
    }


    //trying to rewrite
    private string LoadAnswer(Level sLevels)
    {
        string Questionpath = Application.persistentDataPath
            + "/QuestionsAnswersData.dat";
        BinaryFormatter qbf = new BinaryFormatter();
        FileStream qFile = File.Open(
            Questionpath, FileMode.Open);
        qFile.Seek(0, SeekOrigin.Begin + sLevels.QuestionId);
        for (int i = 0; i < qFile.Length; i++)
        {
            QuestionsClass qdata = (QuestionsClass)qbf.Deserialize(qFile);
            qFile.Close();
            if (qdata.QuestionId == user_question_ID_answered)
            {

                return qdata.Answer;
            }
        }
        Debug.Log("Error in getting answer");
        return null;
        
    }

    private void ShowNotification(bool correct)
    {
        notificationUI.SetActive(true);
        if (correct)
        {
            continueButton.text = ("Correct! +10 points");
        }
        else if (!correct)
        {
            continueButton.text = ("Incorrect! -10 points");
        }
        else
            Debug.Log("Notification bool error.");
    }
                
}



