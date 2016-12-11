using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubjectiveExam : MonoBehaviour {

    int index = 0;

    public Text qBox;
    public Text aBox;
    public Text button;

    string[] questions = { "Can you try raising your arm?", "Why are you here?", "What are your goals ?", "Do you have any pain", "Do you live alone?", "What is your home set up?", "How were you managing at home prior to this illness?" };
    string[] answers = { "", "They say I had a stroke", "To go home.", "No", "No, my husband is home but he works full-time.", "A 2 story split-level home, with bedroom on 2 nd floor and laundry in the basement.", "I worked, drove, did the shopping, walked regularly for exercise" };

    public GameObject questionObj;

    void Start()
    {
        button.text = "Continue";
        qBox.text = questions[index];
        aBox.text = answers[index];
    }

    public void NextQuestion()
    {
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
    }
}
