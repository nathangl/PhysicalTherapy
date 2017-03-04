using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour {
    //This script moves the camera when the subjective exam opens
    Vector3 beginV3;
    Vector3 endV3;
    Vector3 addV3;
    Quaternion beginQ;
    Quaternion endQ;
    Quaternion addQ;
    public float timeLength = 10f; //The time it takes to move the camera when opening the dialogue box
    public float speed = 2.5f; //lerp speed
    bool prevState = false; //Stores the previous state of active
    [HideInInspector]
    public bool finished = true; //So we know if we are finished lerping

    //GameObject patient;
    [HideInInspector]
    public bool active = false;
	void Start () {
        beginV3 = Camera.main.transform.position;
        beginQ = Camera.main.transform.rotation;

        addV3 = new Vector3(0.024f, 0f, 0f);
        addQ = Quaternion.Euler(0f, 15.12f, 0f);
        
	}
	
    public void OnClick()
    {
        active = active ? false : true;
    }

	// Update is called once per frame
	void Update () {
        if(prevState != active && finished == true)
        {
            StartCoroutine(LerpCamera(active));
            prevState = !prevState;
        }
        Debug.Log(finished);
	}
    IEnumerator LerpCamera(bool active)
    {
        float startTime = Time.time;
        if (active)
        {
            endV3 = addV3 + Camera.main.transform.position;
            endQ = Quaternion.Euler(addQ.eulerAngles + Camera.main.transform.rotation.eulerAngles);
        }
        if(!active)
        {
            beginV3 = Camera.main.transform.position - addV3;
            beginQ = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles - addQ.eulerAngles);
        }

        while (Time.time < startTime + timeLength - 0.5)
        {
            finished = false;
            float amountLerp = (Time.time - startTime) / timeLength; //The amount to lerp by
            if (active)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, endV3, amountLerp);
                Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, endQ, amountLerp);
            }
            else
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, beginV3, amountLerp);
                Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, beginQ, amountLerp);
            }
            yield return null;
        }
        finished = true;
    }
}
