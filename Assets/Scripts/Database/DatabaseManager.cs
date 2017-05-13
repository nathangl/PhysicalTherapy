using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

public class DatabaseManager : MonoBehaviour
{
    MySql.Data.MySqlClient.MySqlConnection conn;
    public static bool inDB = false;
    public static bool correctLogin = false;
    //private static string connectionString;

    /*private static string connectionString =
            @"Data Source=csci03.is.uindy.edu;" +
            "Database=TEST;" +
            "UserID=test;" +
            "Password=Test1234!;";
    //"Pooling=false";*/

    private static string connectionString = "server=csci03.is.uindy.edu;uid=test;" +
"pwd=Test1234!;database=TEST;";

    void Start()
    {
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists UserInfo (Username VARCHAR(20) NOT NULL UNIQUE, Password VARCHAR(9)  NOT NULL, FirstName VARCHAR(10)  NOT NULL, LastName  VARCHAR(10)  NOT NULL)");
                dbCmd.CommandText = sqlQuery;
                Debug.Log(dbCmd.CommandText);
                dbCmd.ExecuteScalar();
                conn.Close();
            }
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Debug.Log(ex.Message);
        }
        //connectionString = "URI=file:" + Application.dataPath + "/UserDB.s3db";
        //CreateTable();
    }

    void Update()
    {
        if (correctLogin == true)
        {
            //Set text to appear if login is correct
        }
        //else  //login incorrect
        if (inDB == true)
        {
            //Set text for returning user
        }
        //else  //new user
    }

    private static void InsertUserInfo(string username, string password, string firstName, string lastName, string questionID)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO UserInfo (UserName, Password, FirstName, LastName) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\")", username, password, firstName, lastName);    //INSERT INFO SQL COMMAND
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }

        //Possibly load the next question user is on
    }

    private static void CheckUser(string username, string password, string questionID)
    {
        string tempUsername = null;
        string tempPassword = null;
        string tempFirstName = null;
        string tempLastName = null;
        //string tempQuestionID = null;
        bool foundLogin = false;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM UserInfo";
                dbCmd.CommandText = sqlQuery;
                using (IDataReader dbReader = dbCmd.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        tempUsername = dbReader.GetString(0);
                        tempPassword = dbReader.GetString(1);
                        tempFirstName = dbReader.GetString(2);
                        tempLastName = dbReader.GetString(3);
                        //tempQuestionID = dbReader.GetString(0);

                        if (tempUsername == username && tempPassword == password)
                        {
                            foundLogin = true;
                            break;
                        }
                    }

                    if (foundLogin == true)
                    {
                        //UserCLass storage
                        UserClass.currentUser.username = tempUsername;
                        UserClass.currentUser.firstName = tempFirstName;
                        UserClass.currentUser.lastName = tempLastName;
                        Tracker.LogData(tempFirstName + " " + tempLastName);
                        Login(questionID);
                    }
                    else if ((tempUsername == username && tempPassword != password) || (tempUsername != username && tempPassword == password))
                        LoginError();
                    else Register();
                    dbConnection.Close();
                    dbReader.Close();
                }
            }
        }
    }

    static void Login(string questionID)
    {
        //Progress to next
        correctLogin = true;
    }

    static void LoginError()
    {
        correctLogin = false;
    }

    static void Register()
    {
        inDB = false;
    }

    static private void DeleteUser(string username)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM UserInfo WHERE username = \"{0}\"", username);    //DELETE FROM (table) WHERE (username) = \"{0}\"
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private static void InsertUserRecord(string questionID, bool correct)
    {
        string username = UserClass.currentUser.username; // UserClass.player.username

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSET INTO UserRecords (Username, QuestionID, Correct) VALUES(\"{0}\",\"{1}\",\"{2}\")", username, questionID, correct); //INSERT info follow by values
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public static void registerUser(string username, string password, string firstName, string lastName, string questionID)
    {
        InsertUserInfo(username, password, firstName, lastName, questionID);
    }

    public static void loginUser(string username, string password, string questionID)
    {
        CheckUser(username, password, questionID);
    }

    public static void deleteUser(string username)
    {
        DeleteUser(username);
    }

    public static void insetStudentRecord(string questionID, bool correct)
    {
        InsertUserRecord(questionID, correct);
    }
}
