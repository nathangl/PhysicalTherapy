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

    private static string connectionString = null # taken out to make public

    bool created = true;

    void Start()
    {
        CreateTables();
    }

    void Update()
    {
        /*if (correctLogin == true)
        {
            //Set text to appear if login is correct
        }
        //else  //login incorrect
        if (inDB == true)
        {
            //Set text for returning user
        }
        //else  //new user*/
    }

    private void CreateTables()
    {
        if (created)
        {
            return;
        }
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            //UserInfo Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE IF NOT EXISTS UserInfo (UserID INT NOT NULL AUTO_INCREMENT, Email VARCHAR(255) NOT NULL, Password VARCHAR(255) NOT NULL, FirstName VARCHAR(255) NOT NULL, LastName VARCHAR(255) NOT NULL, PRIMARY KEY (UserID))");
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

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            //ChatLog Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists ChatLog (LogID INT NOT NULL UNIQUE AUTO_INCREMENT, UserID INT NOT NULL, ChatText VARCHAR(1000))");
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
    }

    private static void InsertUserInfo(string email, string password, string firstname, string lastname)
    {
        MySql.Data.MySqlClient.MySqlConnection conn;

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            //UserInfo Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO UserInfo (Email, Password, FirstName, LastName) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\")", email, password, firstname, lastname);
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
    }

    private static void CheckUser(string email, string password)
    {
        string tempID = null;
        string tempPassword = null;
        string tempFirstName = null;
        string tempLastName = null;
        string tempEmail = null;
        bool foundLogin = false;
        MySql.Data.MySqlClient.MySqlConnection conn;

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            //UserInfo Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT * FROM UserInfo");
                dbCmd.CommandText = sqlQuery;
                Debug.Log(dbCmd.CommandText);
                using (IDataReader dbReader = dbCmd.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        tempID = dbReader.GetString(0);
                        tempEmail = dbReader.GetString(1);
                        tempPassword = dbReader.GetString(2);
                        tempFirstName = dbReader.GetString(3);
                        tempLastName = dbReader.GetString(4);

                        if (tempEmail == email && tempPassword == password)
                        {
                            foundLogin = true;
                            break;
                        }
                    }

                    if (foundLogin == true)
                    {
                        //UserCLass storage
                        UserClass.currentUser.firstName = tempFirstName;
                        UserClass.currentUser.lastName = tempLastName;
                        UserClass.currentUser.userID = Int32.Parse(tempID);
                        UserClass.currentUser.email = tempEmail;
                        Tracker.LogData(tempFirstName + " " + tempLastName);
                        Login();
                    }
                    else if ((tempEmail == email && tempPassword != password) || (tempEmail != email && tempPassword == password))
                        LoginError();
                    else Register();
                    conn.Close();
                    dbReader.Close();
                }
            }
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Debug.Log(ex.Message);
        }

        /*using (MySql.Data.MySqlClient.MySqlConnection dbConnection = new MySql.Data.MySqlClient.MySqlConnection())
        {
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM UserInfo";
                dbCmd.CommandText = sqlQuery;
                using (IDataReader dbReader = dbCmd.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        tempID = dbReader.GetString(0);
                        tempEmail = dbReader.GetString(1);
                        tempPassword = dbReader.GetString(2);
                        tempFirstName = dbReader.GetString(3);
                        tempLastName = dbReader.GetString(4);

                        if (tempEmail == email && tempPassword == password)
                        {
                            foundLogin = true;
                            break;
                        }
                    }

                    if (foundLogin == true)
                    {
                        //UserCLass storage
                        UserClass.currentUser.firstName = tempFirstName;
                        UserClass.currentUser.lastName = tempLastName;
                        UserClass.currentUser.userID = Int32.Parse(tempID);
                        UserClass.currentUser.email = tempEmail;
                        Tracker.LogData(tempFirstName + " " + tempLastName);
                        Login();
                    }
                    else if ((tempEmail == email && tempPassword != password) || (tempEmail != email && tempPassword == password))
                        LoginError();
                    else Register();
                    dbConnection.Close();
                    dbReader.Close();
                }
            }
        }*/
    }

    static void Login()
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

    static private void DeleteUser(string email)
    {
        using (MySql.Data.MySqlClient.MySqlConnection dbConnection = new MySql.Data.MySqlClient.MySqlConnection())
        {
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM UserInfo WHERE Email = \"{0}\"", email);    //DELETE FROM (table) WHERE (email) = \"{0}\"
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private static void InsertUserRecord(int userID, string chatTxt)
    {
        using (MySql.Data.MySqlClient.MySqlConnection dbConnection = new MySql.Data.MySqlClient.MySqlConnection())
        {
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO ChatLog (UserID, ChatText) VALUES(\"{0}\",\"{1}\")", userID, chatTxt); //INSERT info follow by values
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private static string GetLogSample(int userID, int logID)
    {
        string logText = "default";
        using (MySql.Data.MySqlClient.MySqlConnection dbConnection = new MySql.Data.MySqlClient.MySqlConnection())
        {
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT ChatText FROM ChatLog WHERE UserID = " + userID + " AND LogId = " + logID);
                Debug.Log(sqlQuery);
                dbCmd.CommandText = sqlQuery;
                using (IDataReader dbReader = dbCmd.ExecuteReader())
                {
                    if (dbReader.Read())
                    {
                        logText = dbReader.GetString(0);
                        dbReader.Close();
                    }
                }
                dbConnection.Close();
            }
        }
        return logText;
    }

    public static void registerUser(string email, string password, string firstName, string lastName)
    {
        InsertUserInfo(email, password, firstName, lastName);
    }

    public static void loginUser(string email, string password)
    {
        CheckUser(email, password);
    }

    public static void deleteUser(string email)
    {
        DeleteUser(email);
    }

    public static void insertStudentRecord(int userID, string chatTxt)
    {
        InsertUserRecord(userID, chatTxt);
    }

    public static string getLogSample(int userID, int logID)
    {
        string log = GetLogSample(userID, logID);
        return log;
    }
}
