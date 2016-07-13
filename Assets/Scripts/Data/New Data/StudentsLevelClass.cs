using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace StudentsLevelClass
{
    //can save data to a file
    [Serializable]
    class LevelsResults
    {
        public static int _highScore;
    }
    [Serializable]
    class Level
    {
        // 1 sucess, 0 fail
        private int _questionId, _levelScore, _success;
        private int _hintId, _StudentId;
        
        public int QuestionId
        {
            get; set;
        }
        public int LevelScore
        {
            get; set;
        }
        public int Success
        {
            get; set;
        }
        public int HintId
        {
            get; set;
        }
        public int StudentId
        {
            get; set;
        }


    }
    //can save data to a file
    [Serializable]
    class Student : IEnumerable<Level>
    {
        private string _fName, _lName;
        private int _StudentId;
        private List<Level> _sLevels = new List<Level>();



        public string FName
        {
            get; set;
        }
        public string LName
        {
            get; set;
        }
        public int StudentId
        {
            get; set;
        }
        public List<Level> GetSetLevels
        {
            get { return _sLevels; }
            set { _sLevels = value; }
        }
        IEnumerator<Level> IEnumerable<Level>.GetEnumerator()
        {
            return this.GetSetLevels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetSetLevels.GetEnumerator();
        }


    }
}
/*
public void Print()
{
   Debug.Log("ID:  " + this.StudentId);
   Debug.Log("First Name: " + this.FName);
   Debug.Log("Last Name:" + this.LName);
   Debug.Log("Owns the following cars: ");
   foreach (Level l in this.GetSetLevels)
   {
       Debug.Log("Current Level: " + l.CurrentLevel +
           l.LevelScore + l.Success);
   }
}*/





