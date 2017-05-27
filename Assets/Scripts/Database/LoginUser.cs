using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginUser : MonoBehaviour
{

    public InputField email;
    public InputField password;

    public void Login()
    {
        DatabaseManager.inDB = true;

        if (email.text != "" && password.text != "")
        {
            string tempEmail = email.text;
            string tempPassword = password.text;

            DatabaseManager.loginUser(tempEmail, tempPassword);
            if(DatabaseManager.correctLogin == true)
            {
                Debug.Log(UserClass.currentUser.email + " is now logged in!");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
