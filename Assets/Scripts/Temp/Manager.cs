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
            menuAnim.SetTrigger("FadeIn");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOut");
            menuOn = false;
        }
    }
    public void BeginDecision()
    {
        if (!menuOn)
        {
            menuAnim.SetTrigger("FadeInDecision");
            menuOn = true;
        }
        else
        {
            menuAnim.SetTrigger("FadeOutDecision");
            menuOn = false;
        }
    }
}