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
    /*public GameObject LHip;
    public GameObject RHip;*/
    public GameObject LKnee;
    public GameObject RKnee;
    public GameObject LAnkle;
    public GameObject RAnkle;

    public bool dropdown = false;
    public bool hand = false;

    void Start()
    {
        DisableDots();
    }

    public void EnableDropdownDots()
    {
        LShoulder.SetActive(true);
        RShoulder.SetActive(true);
        LElbow.SetActive(true);
        RElbow.SetActive(true);
        LHand.SetActive(true);
        RHand.SetActive(true);
        //LHip.SetActive(true);
        //RHip.SetActive(true);
        LAnkle.SetActive(true);
        RAnkle.SetActive(true);
        LKnee.SetActive(true);
        RKnee.SetActive(true);
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
        /*LHip.SetActive(false);
        RHip.SetActive(false);*/
        LAnkle.SetActive(false);
        RAnkle.SetActive(false);
        LKnee.SetActive(false);
        RKnee.SetActive(false);
    }

    public void EnableUpperHandDots()
    {
        LShoulder.SetActive(true);
        RShoulder.SetActive(true);
        LElbow.SetActive(true);
        RElbow.SetActive(true);
        LHand.SetActive(true);
        RHand.SetActive(true);
        hand = true;
    }

    public void EnableLowerHandDots()
    {
        LAnkle.SetActive(true);
        RAnkle.SetActive(true);
        LKnee.SetActive(true);
        RKnee.SetActive(true);
        hand = true;
    }
}
