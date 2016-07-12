using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
//Our NameSpace
using QuestionAnswer;
using System;
using UnityEngine.SceneManagement;

public class SaveQuestionsClassData : MonoBehaviour {
    // fields in the Scene
    public InputField questionId, question, answer,
        Possible_Answer1, Possible_Answer2, Possible_Answer3;
    bool isRedundant = false;
    public void SaveQuestionsData()
    {
        // checking whether the feilds are filled
        bool FilledChecked = CheckAnswerNotNull();
        //if all feilds are filled
        if(FilledChecked)
        {
            string path = Application.persistentDataPath
             + "/QuestionsAnswersData.dat";
            // File.Delete(path);
            if (!File.Exists(path))
            {
                CreateNewQuestionAnswerFile(); 
            }
            else
            {
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath
             + "/QuestionsAnswersData.dat", FileMode.Open);
                
                Debug.Log(Application.persistentDataPath);

                //check if the Question ID redundunt

                //append new questions

                file.Seek(0, SeekOrigin.Begin);
                while (file.Position != file.Length)
                {
                    QuestionsClass data = (QuestionsClass)bf.Deserialize(file);
                    if (data.QuestionId == int.Parse(questionId.text))
                    {
                        Debug.Log("The questionId: " + data.QuestionId + "" +
                           data.Question + "with answer: " + data.Answer +
                           "is Already exist");
                         
                        isRedundant = true;
                        file.Close();
                        break;
                    }
                }

                if (isRedundant == false)
                {
                    file.Close();
                    AppendNewDataToQuestionFile(); 
                }
            }
        } //if there is empty feild
        else
        {
            Debug.Log("The three feilds needed to be filled!");
        }
       
            

    }

    private void CreateNewQuestionAnswerFile()
    {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
         + "/QuestionsAnswersData.dat");
            Debug.Log(Application.persistentDataPath);
            QuestionsClass data = new QuestionsClass();
            data.QuestionId = int.Parse(questionId.text);
            data.Question = question.text;
            data.Answer = answer.text;
            data.PossibleAnswers.Add(Possible_Answer1.text);
            data.PossibleAnswers.Add(Possible_Answer2.text);
            data.PossibleAnswers.Add(Possible_Answer3.text);

            //write data to file
            bf.Serialize(file, data);
            file.Close();
            Debug.Log("File is created and data saved");
            SceneManager.LoadScene("SaveQuestionAnsweDataSucess");
    }

    private void AppendNewDataToQuestionFile()
    {
            BinaryFormatter bfAppend = new BinaryFormatter();
            FileStream bAppendfile = File.Open(Application.persistentDataPath
     + "/QuestionsAnswersData.dat", FileMode.Append);
            //FileStream appendFile = File.Open(Application.persistentDataPath
            // + "/QuestionsAnswersData.dat", FileMode.Append);
            //write data to file
            QuestionsClass data = new QuestionsClass();
            data.QuestionId = int.Parse(questionId.text);
            data.Question = question.text;
            data.Answer = answer.text;
            data.PossibleAnswers.Add(Possible_Answer1.text);
            data.PossibleAnswers.Add(Possible_Answer2.text);
            data.PossibleAnswers.Add(Possible_Answer3.text);
            bfAppend.Serialize(bAppendfile, data);
            bAppendfile.Close();

            Debug.Log("Data appended");
            SceneManager.LoadScene("SaveQuestionAnsweDataSucess");
    }

    private bool CheckAnswerNotNull()
    {
       if (Possible_Answer1.text == "")  
        {
            return false;
        }else if( Possible_Answer2.text == "")
        {
            return false;
        }
        else if (Possible_Answer3.text == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
