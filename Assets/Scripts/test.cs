﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    bool finished = false;
    public Animator anim;
    private float pos = 0f;
    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && finished==false)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "LeftHand")
                {
                    if (pos > anim.GetCurrentAnimatorClipInfo(0).Length)
                    {
                        pos = 0f;
                        anim.speed = 1;
                        anim.Play("PROMLeftArm", 0);
                        finished = true;

                    }
                    else
                    {
                        pos += 0.003f;
                        anim.speed = 0;
                        anim.Play("PROMLeftArm", 0, pos);
                        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
                        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0).);
                        //PlayMode.StopAll;
                    }
                }
                

            }
        }
        if (finished == true)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                finished = false; timer = 0f;
            }
        }
    }
}
        

        /*
        else if ( Input.GetAxis("Mouse X") < 0 && Input.GetAxis("Mouse Y") < 0) {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "LeftHand")
                {
                    pos -= 0.003f;
                    anim.speed = 0;
                    anim.Play("PROMLeftArm", 0, pos);
                }
            }
        }
        */
        /*
    // Use this for initialization
    private float radius;
    public Image dot;
    private Vector3 center;
    private float angle;
    private Vector3 max;
    private Vector3 min;
    public Animator anim;
    private float prevAng = -70;
    float count = 0f;
    void Start()
    {
        min = new Vector3(131, -78, 0);
        max = new Vector3(61, 147, 0);
        //center = gameObject.transform.position;
        Debug.Log(center);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "LeftHand")
                {
                    center = gameObject.transform.position;
                    Vector3 dir = (Input.mousePosition - center).normalized;
                    radius = Vector3.Distance(center, dot.transform.position);
                    //angle = Vector2.Angle(center, dir);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    prevAng = angle;
                    if (angle > -70 && angle < 64)
                    {
                        if (angle - prevAng >= 0)
                        {
                            Debug.Log("hey");
                            anim.speed = 0;
                            count += 0.003f;
                            if(count < 0) count = 0;
                            anim.Play("PROMLeftArm", 0, count);
                        }
                        else
                        {
                            Debug.Log("ho");
                            anim.speed = 0;
                            count -= 0.003f;
                            if (count < 0) count = 0;
                            anim.Play("PROMLeftArm", 0, count);
                        }
                        dot.transform.position = (Vector2)center + ((Vector2)dir * radius);
                        Debug.Log(dir);
                        Debug.Log(dot.transform.position);
                        Debug.Log(angle);
                    }
                }
            }

        }
    }
    void MathStuff()
    {

    }

}
*/


