using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpScenes : MonoBehaviour {

        public void LoadScene(int level)
        {
            SceneManager.LoadScene(level);
        }
}
