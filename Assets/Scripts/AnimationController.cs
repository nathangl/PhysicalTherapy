using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationController : MonoBehaviour {

    Animator patientAnim;

    void Awake()
    {
        patientAnim = GetComponent<Animator>();
    }

    public void PlayPatientAnim(Dropdown dropdown)
    {
        if (dropdown.value == 1)        //AROM
        {
            Debug.Log("AROM Animation Accessed");
            patientAnim.SetTrigger("AROMLeftArm");
			dropdown.value = 0;
        }

        else if (dropdown.value == 2)       //PROM
        {
            Debug.Log("PROM Animation Accessed");
            patientAnim.SetTrigger("PROMLeftArm");
			dropdown.value = 0;
        }

        else
            Debug.Log("Error at animationcontroller");
    }
}
