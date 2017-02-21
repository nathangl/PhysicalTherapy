using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour {
    //This script moves the camera when the subjective exam opens
    Vector3 beginV3;
    Vector3 endV3;
    Quaternion beginQ;
    Quaternion endQ;
    public float speed = 2.5f; //lerp speed
    
    //GameObject patient;
    bool active = false;
	void Start () {
        beginV3 = Camera.main.transform.position;
        beginQ = Camera.main.transform.rotation;

        endV3 = new Vector3(0.024f, 0.61f, -1.25f);
        endQ = Quaternion.Euler(0f, 15.12f, 0f);
        
	}
	
    public void OnClick()
    {
        if (!active)
            active = true;
        else
            active = false;
    }

	// Update is called once per frame
	void Update () {
        if (active)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, endV3,  speed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, endQ, speed * Time.deltaTime);
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, beginV3, speed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, beginQ, speed * Time.deltaTime);
        }
	}
}
