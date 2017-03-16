using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	private CanvasGroup cGroup;
    private Animator menuAnim;
    [HideInInspector] public bool menuOn = true;

    void Awake()
    {
		cGroup = gameObject.GetComponent<CanvasGroup> ();
		menuAnim = GetComponentInChildren<Animator> ();
        if (cGroup)
        {
            cGroup.alpha = 0;
        }
		menuOn = false;
		OnOff (menuOn);
    }

    public void BeginMenu(bool isDropdown)
    {
        if (!menuOn && !isDropdown)
        {
            //gameObject.SetActive(true);
            menuAnim.SetTrigger("FadeIn");
            menuOn = true;
			OnOff (menuOn);
        }
        else
        {
            menuAnim.SetTrigger("FadeOut");
            menuOn = false;
			OnOff (menuOn);
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
			OnOff (menuOn);
        }
        else
        {
            menuAnim.SetTrigger("FadeOutDecision");
            menuOn = false;
			OnOff (menuOn);
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
			OnOff (menuOn);
        }
        else
        {
            menuAnim.SetTrigger("FadeOutQuestions");
            menuOn = false;
			OnOff (menuOn);
            //Invoke("DisableUI", 1);
        }
    }
    void DisableUI()
    {
        gameObject.SetActive(false);
    }

	public void OnOff(bool input)
	{
		if (input == true) {
			cGroup.alpha = 1f;
			cGroup.interactable = true;
			cGroup.blocksRaycasts = true;
		} else if(cGroup){
			//cGroup.alpha = 0f;
			cGroup.interactable = false;
			cGroup.blocksRaycasts = false;
		}
	}

    void Start()
    {

    }

  
}