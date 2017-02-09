using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
    public Slider slider;
    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        anim.speed = 0;
        anim.Play("PROMLeftArm", 0, slider.normalizedValue);
    }
}
