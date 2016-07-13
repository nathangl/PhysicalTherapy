using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using StudentsLevelClass;
using UnityEngine.SceneManagement;
public class SaveNewUser : MonoBehaviour {

    public InputField firstName, lastName;
   public void Save()
   {
       string path = Application.persistentDataPath
         + "/playerInfo.dat";
       if (!File.Exists(path))
       {
           BinaryFormatter bf = new BinaryFormatter();
           FileStream file = File.Create(Application.persistentDataPath
            + "/playerInfo.dat");
           Debug.Log(Application.persistentDataPath);
           Student data = new Student();
           data.StudentId = UserInputCheck.User_ID_All_Level;
            //for next Scene 
            UserInputCheck.User_ID_All_Level = data.StudentId;
            data.FName = firstName.text;
           data.LName = lastName.text;
           //write data to file
           bf.Serialize(file, data);
           file.Close();
            SceneManager.LoadScene("WellcomeBack");


       }
       else
       {
           BinaryFormatter bf = new BinaryFormatter();
           FileStream file = File.Open(Application.persistentDataPath
            + "/playerInfo.dat", FileMode.Append);
           Debug.Log(Application.persistentDataPath);
           Student data = new Student();
            data.StudentId = UserInputCheck.User_ID_All_Level;
            //for next Scene 
            UserInputCheck.User_ID_All_Level = data.StudentId;
            data.FName = firstName.text;
            data.LName = lastName.text;
            //write data to file
            bf.Serialize(file, data);
           file.Close();
            SceneManager.LoadScene("WellcomeBack");
        }

       
   }
  
}
