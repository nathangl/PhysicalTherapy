using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	public void SceneSwitch()
    {
        SceneManager.LoadSceneAsync("Take1");
    }
}
