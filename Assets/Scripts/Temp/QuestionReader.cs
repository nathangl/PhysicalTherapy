using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;



public class QuestionReader : MonoBehaviour
{

    public TextAsset questionFile;
    public string[] questionLines;
    private List<string> questionList;

    public TextAsset answerFile;
    public string[] answerLines;
    private List<string> answerList;

    public Text qBox;
    public Text aBox;
    public Text buttonTxt;

    public int index;

    void Start()
    {

        questionList = new List<string>();
        if (questionFile != null)
        {
        questionLines = (questionFile.text.Split('\n'));
        }
        for (int i = 0; i < questionLines.Length; i++)
        {
        questionList.Add(questionLines[i]);
        }

        answerList = new List<string>();
        if (answerFile != null)
        {
            answerLines = (answerFile.text.Split('\n'));
        }
        for (int i = 0; i < answerLines.Length; i++)
        {
            answerList.Add(answerLines[i]);
        }


    }

    void Update()
    {
        if (index == questionLines.Length)
        {
            index = 0;
        }
        this.qBox.text = questionList[index];
        this.aBox.text = answerList[index];
        if (index == questionLines.Length-1)
        {
            this.buttonTxt.text = "Reset";
        }
        if (index <questionLines.Length-1)
        {
            this.buttonTxt.text = "Continue";
        }
    }


    public void Continue()
    {
        index++;
    }
}
