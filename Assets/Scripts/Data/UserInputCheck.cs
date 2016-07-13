using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//need these for the saving
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using StudentsLevelClass;

public class UserInputCheck : MonoBehaviour
{

    //Correspond to the fields in the Scene
    public InputField User_ID;
    public static int User_ID_All_Level;

    //  public GameObject instructions; 
    public void InputCheck()
    {

        if ( User_ID.text == "")
        {

            Debug.Log("Check you fields! ");
        }

        else
        {
            SaveUserInfo(); 
       
        }
    }

    private void SaveUserInfo()
    {
        bool RedundantId = false;
        Student data = new Student();
        int tempIdChecking = Int32.Parse(User_ID.text);
        string path = Application.persistentDataPath
          + "/playerInfo.dat";
        //File.Delete(path);


        if (!File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
             + "/playerInfo.dat");
            Debug.Log(Application.persistentDataPath);

            data.StudentId = Int32.Parse(User_ID.text);
            User_ID_All_Level = Int32.Parse(User_ID.text);
           // data.FName = firstName.text;
           // data.LName = "Johny";
           //write data to file
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileOpen = File.Open(Application.persistentDataPath
             + "/playerInfo.dat", FileMode.Open);
            while (fileOpen.Position != fileOpen.Length)
            {
                Student sData = (Student)bf.Deserialize(fileOpen);
                if (sData.StudentId == tempIdChecking)
                {
                    RedundantId = true;
                    User_ID_All_Level = Int32.Parse(User_ID.text);
                    //load the data from user
                    SceneManager.LoadScene("WellcomeBack");
                    Debug.Log("This ID already exist!");
                    break;
                }
            }
            if (RedundantId == false)
            {
                User_ID_All_Level = Int32.Parse(User_ID.text);
                SceneManager.LoadScene("ID_Not_Exist"); 
            }

        }
    }



    
}

