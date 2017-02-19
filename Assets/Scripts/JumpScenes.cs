using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Scenes change by int corresponding to Build Setting Index numbers

public class JumpScenes : MonoBehaviour {

        public void LoadScene(int level)
        {
            SceneManager.LoadScene(level);
        }
        public void Exit()
        {
            Application.Quit();
        }
}
