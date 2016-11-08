using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegisterUser : MonoBehaviour {

    public InputField username;
    public InputField password;
    public InputField firstName;
    public InputField lastName;

    public string questionID;

    public void Register()
    {
        if (username.text != "" && password.text != "" && firstName.text != "" && lastName.text != "")
        {
            string tempUsername = username.text;
            string tempPassword = password.text;
            string tempFirstName = firstName.text;
            string tempLastName = lastName.text;

            DatabaseManager.registerUser(tempUsername, tempPassword, tempFirstName, tempLastName, questionID);
        }
    }
}
