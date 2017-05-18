using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class RegisterUser : MonoBehaviour {

    public InputField username;
    public InputField password;
    public InputField firstName;
    public InputField lastName;
    public InputField emailAddress;
    public Text errorTxt;

    public void Register()
    {
        if (username.text != "" && password.text != "" && firstName.text != "" && lastName.text != "")
        {
            string tempUsername = username.text;
            string tempPassword = password.text;
            string tempFirstName = firstName.text;
            string tempLastName = lastName.text;
            string tempEmail = emailAddress.text;
            if (IsValidEmailAddress(tempEmail))
            {
                DatabaseManager.registerUser(tempUsername, tempPassword, tempFirstName, tempLastName, tempEmail);
                SceneManager.LoadScene("Login");
            }
            else
            {
                errorTxt.text = "ERROR: Email invalid";
            }
        }
    }

    public static bool IsValidEmailAddress(string emailaddress)
    {
        try
        {
            Regex rx = new Regex(
        @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(emailaddress);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
