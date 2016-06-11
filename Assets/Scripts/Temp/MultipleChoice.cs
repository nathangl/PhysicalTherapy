using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;






public class MultipleChoice : MonoBehaviour {


    public Text qBox;
    public Text choice1;
    public Text choice2;
    public Text choice3;
    public Text choice4;

    private int numberCorrect = 0;
    private int numberIncorrect = 0;
    private int answer = 2;

    public GameObject questions;

    void Start () {
        this.qBox.text = "What is the color of the grass?";
        this.choice1.text = "Blue";
        this.choice2.text = "Green";
        this.choice3.text = "White";
        this.choice4.text = "I don't know";


	}
    
    public void TestAnswer(int choice)
    {
        if (choice == answer)
            numberCorrect++;
        else
            numberIncorrect++;
        Debug.Log(numberCorrect);
        Debug.Log(numberIncorrect);
        questions.SetActive(false);
    }

    void Update () {
	
	}
}
