using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Back : MonoBehaviour {

	public void BackToMain()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
