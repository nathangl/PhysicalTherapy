using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class SubjectiveExam : MonoBehaviour {

    int index = 0;

    public Text qBox;
    public Text aBox;
    public Text button;

	public Text question1;
	public Text question2;
	public Text question3;
	public Text question4;
	public Text question5;
	public Text question6;
	public Text question7;

    string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
    string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise" };


    public GameObject questionObj;

    void Start()
    {
		
        //button.text = "Continue";
        qBox.text = "Click a question";
        aBox.text = "";

		question1.text = questions [0];
		question2.text = questions [1];
		question3.text = questions [2];
		question4.text = questions [3];
		question5.text = questions [4];
		question6.text = questions [5];
		question7.text = questions [6];
				
    }

    public void NextQuestion()
    {
		/*
        index++;
        if (index < questions.Length)
        {
            qBox.text = questions[index];
            aBox.text = answers[index];
            if (index == (questions.Length - 1))
            {
                button.text = "Reset";
            }
        }
        else
        {
            index = 0;
            qBox.text = questions[index];
            aBox.text = answers[index];
            button.text = "Continue";
        }
        */
		GameObject obj = EventSystem.current.currentSelectedGameObject;
		switch (obj.name)
		{
		case "Question1":
				qBox.text = questions [0];
				aBox.text = answers [0];
				break;
			case "Question2":
				qBox.text = questions [1];
				aBox.text = answers [1];
				break;
			case "Question3":
				qBox.text = questions [2];
				aBox.text = answers [2];
				break;
			case "Question4":
				qBox.text = questions [3];
				aBox.text = answers [3];				
				break;
			case "Question5":
				qBox.text = questions [4];
				aBox.text = answers [4];
				break;
			case "Question6":
				qBox.text = questions [5];
				aBox.text = answers [5];		
				break;
			case "Question7":
				qBox.text = questions [6];
				aBox.text = answers [6];				
				break;

		}
    }
}
