using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//This script is used to track user interaction and store the data in Data.txt
public class Tracker : MonoBehaviour {
    static string path;
    //Creates the data file if it doesnt exists or wipes it if it does exist
    void Start()
    {
        path = Application.dataPath + @"/Data.txt";

        if (!File.Exists(path))
        {
            File.CreateText(path);
            File.CreateText(path).Dispose();
        }
        else
        {
            File.WriteAllText(path, "");
            //File.WriteAllText(path, "").Dispose();
        }

        
       // writer = new StreamWriter(path);
    }
	
	public static void LogData(string input)
    {
        //StreamWriter writer = new StreamWriter(path);
        using (StreamWriter write = File.AppendText(path)) { write.WriteLine(input); }
    }
}
