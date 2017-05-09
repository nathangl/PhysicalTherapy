using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginUser : MonoBehaviour
{

    public InputField username;
    public InputField password;

    public string questionID;

    public void Login()
    {
        DatabaseManager.inDB = true;

        if (username.text != "" && password.text != "")
        {
            string tempUsername = username.text;
            string tempPassword = password.text;

            DatabaseManager.loginUser(tempUsername, tempPassword, questionID);
            if(DatabaseManager.correctLogin == true)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
