using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour {

    string path;
    
    void Start()
    {
        path = Application.dataPath + @"/Data.txt";
    }

    string GetText()
    {
        string text = System.IO.File.ReadAllText(path);
        return text;
    }

    public void InsertTestRecord()
    {
        DatabaseManager.insertStudentRecord(UserClass.currentUser.userID, GetText());
    }
}
