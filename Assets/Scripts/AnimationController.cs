using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    Animator patientAnim;

    void Awake()
    {
        patientAnim = GetComponent<Animator>();
    }

    public void PlayPatientAnim(string pAnim)
    {
        if (pAnim == "leftShoulder")
        {
            Debug.Log("Animation Accessed");
            patientAnim.SetTrigger("LeftShoulder");
        }
    }

    void OnMouseDown()
    {
        patientAnim.SetTrigger("LeftShoulder");
    }
}
