using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPaths : MonoBehaviour {
    Vector3 player;
    float minDistance = float.PositiveInfinity;
    float minPercent = 0;
    Vector3 pos;
    public Animator anim;
    bool active = false;
    void Start()
    {
        iTween.PutOnPath(gameObject, iTweenPath.GetPath("PROMRightArm"), 0);
    }

    void OnMouseDrag()
    {
            //Debug.Log("hit");
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            gameObject.transform.position = objPosition;
            iTween.PutOnPath(gameObject, iTweenPath.GetPath("PROMRightArm"), DeterminePos(objPosition));
            anim.speed = 0;
            anim.Play("PROMRightArm", 0, DeterminePos(objPosition)*0.83f);
    }
    float DeterminePos(Vector3 input)
    {
        float minDistance = float.PositiveInfinity;
        float minPercent = 0;

        for (float t = 0; t <= 1; t += 0.005f)
        {
            float dist = ((Vector2)input - (Vector2)iTween.PointOnPath(iTweenPath.GetPath("PROMRightArm"), t)).sqrMagnitude;
            if (dist < minDistance)
            {
                minDistance = dist;
                minPercent = t;
                pos = iTween.PointOnPath(iTweenPath.GetPath("PROMRightArm"), t);
            }
        }
        return minPercent;
    }
}
