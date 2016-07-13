using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToQuestionsMenu : MonoBehaviour {

	// Use this for initialization
	public void BackToQuestionsScene () {
        SceneManager.LoadScene("SaveQuestionAnsweData");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
