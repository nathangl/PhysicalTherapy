using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    private Animator menuAnim;
    private bool menuOn = true;
    void Awake()
    {
        menuAnim = GetComponent<Animator>();
    }
    public void BeginMenu()
    {
        if (!menuOn)
        {
            //gameObject.SetActive(true);
            menuAnim.SetTrigger("FadeIn");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOut");
            menuOn = false;
            //Invoke("DisableUI", 1);
        }
    }
    public void BeginDecision()
    {
        if (!menuOn)
        {
            //gameObject.SetActive(true);
            menuAnim.SetTrigger("FadeInDecision");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOutDecision");
            menuOn = false;
            //Invoke("DisableUI", 1);
        }
    }
    public void BeginQuestions()
    {
        if (!menuOn)
        {
            //gameObject.SetActive(true);
            menuAnim.SetTrigger("FadeInQuestions");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOutQuestions");
            menuOn = false;
            //Invoke("DisableUI", 1);
        }
    }
    void DisableUI()
    {
        gameObject.SetActive(false);
    }
  
}