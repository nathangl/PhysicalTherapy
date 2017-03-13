using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotControlller : MonoBehaviour {

    public GameObject LShoulder;
    public GameObject RShoulder;
    public GameObject LElbow;
    public GameObject RElbow;
    public GameObject LHand;
    public GameObject RHand;

    bool dropdown = false;

    void Start()
    {
        DisableDots();
    }

    public void EnableDropdownDots()
    {
        LShoulder.SetActive(true);
        RShoulder.SetActive(true);
        dropdown = true;
    }

    public void DisableDots()
    {
        LShoulder.SetActive(false);
        RShoulder.SetActive(false);
        LElbow.SetActive(false);
        RElbow.SetActive(false);
        LHand.SetActive(false);
        RHand.SetActive(false);
    }
}
