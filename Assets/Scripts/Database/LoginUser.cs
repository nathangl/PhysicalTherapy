using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginUser : MonoBehaviour
{

    public InputField username;
    public InputField password;

    public string questionID;

    public void Login()
    {
        DatabaseManager.correctLogin = true;
        DatabaseManager.inDB = true;

        if (username.text != "" && password.text != "")
        {
            string tempUsername = username.text;
            string tempPassword = password.text;

            DatabaseManager.loginUser(tempUsername, tempPassword, questionID);
        }
    }
}
