using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Scenes change by int corresponding to Build Setting Index numbers

public class JumpScenes : MonoBehaviour {

        //Give scene index, loads that scene
        public void LoadScene(int level)
        {
            SceneManager.LoadScene(level);
        }

        //Closes application
        public void Exit()
        {
            Application.Quit();
        }
}
