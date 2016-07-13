using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace QuestionAnswer
{
    [Serializable]
    public class QuestionsClass
    {
        int _questionId;
        string _questoin, _answer;
        List<string> _possibleAnswers = new List<string>();

        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; }
        }
        public string Question
        {
            get { return _questoin; }
            set { _questoin = value; }
        }

        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        public List<string> PossibleAnswers
        {
            get { return _possibleAnswers; }
            set { _possibleAnswers = value; }
        }


    }

 
}

