using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class RetainUserID : MonoBehaviour {
    public InputField userID;
    // Use this for initialization
    void Awake () {
        userID.text = UserInputCheck.User_ID_All_Level.ToString();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
