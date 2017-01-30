using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderAnim : MonoBehaviour {
    public Animator anim;
    private Slider slider;
    //public QSearch qSearch;
    private string animName = "";
    bool flag = false;
    string current, previous = "";
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        animName = gameObject.name;
        slider.onValueChanged.AddListener(delegate { SlideAnimation(); });
    }
    private void SlideAnimation()
    {
        anim.speed = 0;
        anim.Play(animName, 0, slider.normalizedValue);
    }
    void Update()
    {
        /*
        if (slider.normalizedValue == 1 && flag == false)
        {
            qSearch.textArea.text += "Patient: Ouch!\n\n";
            flag = true;
        }
        else if (slider.normalizedValue != 1)
        {
            flag = false;
        }
    }
    */
    }
    }
