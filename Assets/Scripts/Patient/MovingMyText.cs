using UnityEngine;
using System.Collections;

public class MovingMyText : MonoBehaviour {
    private bool onScreen = true;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

}

public void Movement () {

        if (onScreen == true)
        {
            anim.Play("ChartBack");
            onScreen = false;
        }
        else
        {
            anim.Play("ChartMove");
            onScreen = true;
        }
    }
}
