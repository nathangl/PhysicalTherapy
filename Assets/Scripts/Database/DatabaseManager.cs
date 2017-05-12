using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseManager : MonoBehaviour
{

    public static bool inDB = false;
    public static bool correctLogin = false;
    private static string connectionString;

    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/UserDB.s3db";
        CreateTable();
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

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists UserInfo (Username TEXT NOT NULL UNIQUE, Password TEXT  NOT NULL, FirstName TEXT  NOT NULL, LastName  TEXT  NOT NULL)");
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists UserRecords (UserName TEXT  NOT NULL PRIMARY KEY, QuestionID TEXT  NOT NULL, Correct BOOLEAN  NOT NULL)");
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists Question (Question_ID INTEGER  NOT NULL PRIMARY KEY, Question TEXT  NOT NULL, Answer TEXT  NOT NULL)");
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private static void InsertUserInfo(string username, string password, string firstName, string lastName, string questionID, string email)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO UserInfo (UserName, Password, FirstName, LastName, Email) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", username, password, firstName, lastName, email);    //INSERT INFO SQL COMMAND
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

    public static void registerUser(string username, string password, string firstName, string lastName, string questionID, string email)
    {
        InsertUserInfo(username, password, firstName, lastName, questionID, email);
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
