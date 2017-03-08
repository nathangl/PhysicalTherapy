using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script rotates the camera 180 degrees around the model
public class cameraRotate : MonoBehaviour {
    public GameObject center; //The gameobject that we will rotate
    CameraLerp camLerp;
    float speed = 180f;
    public Button left, right;
    bool leftB, rightB;
    bool reset = false;
    Vector3 resetV3;
    Quaternion resetQ;
    [HideInInspector]
    public float currentAngle; //The current angle around the patient
	// Use this for initialization
	void Start () {
        camLerp = gameObject.GetComponent<CameraLerp>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Camera.main.transform.position != new Vector3(0.024f, 0.61f, -1.25f) || Camera.main.transform.position != new Vector3(0f, 0.61f, -1.25f))
        {
            reset = false;
        }
        if (camLerp.finished == true)
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || left.GetComponent<buttonDown>().mouseDown) && (transform.rotation.y < 0.5))
                transform.RotateAround(center.transform.position, Vector3.up, speed * Time.deltaTime);

            if ((Input.GetKey(KeyCode.RightArrow) || right.GetComponent<buttonDown>().mouseDown) && (transform.rotation.y > -0.5))
                transform.RotateAround(center.transform.position, -Vector3.up, speed * Time.deltaTime);
        }
        if(camLerp.active)
        {
            resetV3 = new Vector3(0.024f, 0.61f, -1.25f);
            resetQ = Quaternion.Euler(0f, 15.12f, 0f);
        }
        else
        {
            resetV3 = new Vector3(0f, 0.61f, -1.25f);
            resetQ = Quaternion.Euler(0f, 0f, 0f);
        }
        currentAngle = Camera.main.transform.rotation.y;
        //Debug.Log(DetermineZone());

    }

    void changeState(bool current)
    {
        current = current ? false : true;
    }
    public void Reset()
    {
        if(camLerp.finished == true) 
            StartCoroutine(ResetPos());
    }
    //Returns the camera to the default (0,0,0) state
    IEnumerator ResetPos()
    {
        if(camLerp.finished == true && reset == false)
        {
            camLerp.finished = false;
            float startTime = Time.time;
            while (Time.time < startTime + 0.5)
            {
                reset = false;
                float amountLerp = (Time.time - startTime) / 2; //The amount to lerp by
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, resetV3, amountLerp);
                Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, resetQ, amountLerp);
                yield return null;
            }
            reset = true;
            camLerp.finished = true;
        }
    }
    //Determine if the PT is left/right/in front of the patient
    public string DetermineZone()
    {
        string zone="";
        if (currentAngle > 0.30)
            zone = "right";         //The patients right side
        else if (currentAngle < -0.30)
            zone = "left";          //The patients left side
        else
            zone = "front";
            
        return zone;
    }
}
