using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPathsManager : MonoBehaviour {
    public string prevAnim="";
    public GameObject prevObj;
    public bool first = true;
    public float prevPos = 0; //prev animation position
    public PatientController pController;
    string prev="";
    public Animator anim;
	// Use this for initialization
	void Start () {
        prev = pController.currentScreen;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
