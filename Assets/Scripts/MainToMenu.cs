using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainToMenu : MonoBehaviour
{

    public void SceneSwitch()
    {
        SceneManager.LoadSceneAsync("Chart");
    }
}
