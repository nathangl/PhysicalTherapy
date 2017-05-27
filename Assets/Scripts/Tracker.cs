using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
//This script is used to track user interaction and store the data in Data.txt
public class Tracker : MonoBehaviour {
    static string path;
    static float timer;
    static string minutes, seconds;
    static Dictionary<string, int> idealOrder = new Dictionary<string, int> {
        {"Hypothesis1", 0 }, {"Hypothesis2", 0 }, {"Hypothesis3", 0 }, {"AROM", 2 }, {"AROMLeftShoulder", 3 }, {"AROMRightShoulder", 3 },
        {"PROMLeftShoulder", 5 }, {"AROMLF", 3 }, {"AROMRF", 3 }, {"PROMLF", 5 }, {"PROM", 4 },
        {"PROMLHP", 5 }
    };
    [HideInInspector]
    public static HashSet<string> clicked = new HashSet<string>(); //Stores what options a user has already clicked
    [HideInInspector]
    public static List<string> currentOrder = new List<string>(); //Stores the order the user clicks options

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
        for (int i = 0; i < QSearch.answers.Length; i++)
        {
            idealOrder.Add("Q" + i, 1);
        }

        
       // writer = new StreamWriter(path);
    }

    public int CalculateScore()
    {
        int total = 0;
        int curMax = 0; //Max value thats been seen
        foreach(var item in clicked)
        {
            if(curMax >= idealOrder[item])
            {
                total++;
            }
            else if (idealOrder[item] == (curMax + 1))
            {
                total++;
                curMax++;
            }
            else
            {
                curMax = idealOrder[item];
            }
        }
        return total;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = Mathf.Floor(timer % 60).ToString("00");
        //Debug.Log(CalculateScore());
    }

    public static void LogData(string input)
    {
        //StreamWriter writer = new StreamWriter(path);
        using (StreamWriter write = File.AppendText(path)) { write.WriteLine("[" + minutes + ":" + seconds + "]" + input); }
        
    }
}
