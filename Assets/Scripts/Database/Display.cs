using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {

    public Text text;
    public int userID, logID;

    void Start()
    {
        text.text = DatabaseManager.getLogSample(userID, logID);
    }
}
