﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPaths : MonoBehaviour {
    Vector3 player;
    float minDistance = float.PositiveInfinity;
    float minPercent = 0;
    Vector3 pos;
    public Animator anim;
    bool active = false;
    public animPathsManager manager;
    void Start()
    {
        iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), 0);
    }

    void OnMouseDrag()
    {
        if (gameObject.name == manager.prevAnim || manager.first == true)
        {
            //Debug.Log("hit");
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);


            float pos = DeterminePos(objPosition);
            gameObject.transform.position = objPosition;
            
            iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), pos);

            anim.speed = 0;
            if(gameObject.name=="PROMRightArm")
                anim.Play(gameObject.name, 0, (DeterminePos(objPosition) * 0.83f));
            else
                anim.Play(gameObject.name, 0, (DeterminePos(objPosition) * 0.83f) / 2);
            manager.prevAnim = gameObject.name;
            manager.prevObj = gameObject;
            manager.first = false;
        }
        else
        {
            iTween.PutOnPath(manager.prevObj, iTweenPath.GetPath(manager.prevAnim), 0);
            manager.prevAnim = gameObject.name;
        }
    }
    float DeterminePos(Vector3 input)
    {
        float minDistance = float.PositiveInfinity;
        float minPercent = 0;

        for (float t = 0; t <= 1; t += 0.005f)
        {
            //float dist = ((Vector2)input - (Vector2)iTween.PointOnPath(iTweenPath.GetPath("PROMRightArm"), t)).sqrMagnitude;
            float dist = ((Vector2)input - (Vector2)iTween.PointOnPath(iTweenPath.GetPath(gameObject.name), t)).sqrMagnitude;

            if (dist < minDistance)
            {
                minDistance = dist;
                minPercent = t;
                pos = iTween.PointOnPath(iTweenPath.GetPath(gameObject.name), t);
            }
        }
        //Debug.Log(minPercent);
        return minPercent;
    }
}