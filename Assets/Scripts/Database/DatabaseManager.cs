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

    private static string connectionString = "server=csci03.is.uindy.edu;uid=test;" +
"pwd=Test1234!;database=TEST;";

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
        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            //UserInfo Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists UserInfo (UserID INT NOT NULL UNIQUE AUTO_INCREMENT, Username VARCHAR(10)  NOT NULL, Password VARCHAR(10) NOT NULL, FirstName VARCHAR(10)  NOT NULL, LastName  VARCHAR(10)  NOT NULL, Email VARCHAR(20) NOT NULL");
                dbCmd.CommandText = sqlQuery;
                Debug.Log(dbCmd.CommandText);
                dbCmd.ExecuteScalar();
                //conn.Close();
            }
            //ChatLog Table
            using (IDbCommand dbCmd = conn.CreateCommand())
            {
                string sqlQuery = String.Format("CREATE TABLE if not exists ChatLog (LogID INT NOT NULL UNIQUE AUTO_INCREMENT, UserID INT NOT NULL, ChatText VARCHAR(1000)");
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

    private static void InsertUserInfo(string username, string password, string firstName, string lastName, string email)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO UserInfo (Username, Password, FirstName, LastName, Email) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", username, password, firstName, lastName, email);    //INSERT INFO SQL COMMAND
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private static void CheckUser(string username, string password)
    {
        string tempID = null;
        string tempUsername = null;
        string tempPassword = null;
        string tempFirstName = null;
        string tempLastName = null;
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
                        tempID = dbReader.GetString(0);
                        tempUsername = dbReader.GetString(1);
                        tempPassword = dbReader.GetString(2);
                        tempFirstName = dbReader.GetString(3);
                        tempLastName = dbReader.GetString(4);

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
                        UserClass.currentUser.userID = Int32.Parse(tempID);
                        Tracker.LogData(tempFirstName + " " + tempLastName);
                        Login();
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

    private static void InsertUserRecord(int userID, string chatTxt)
    {
        using (IDbConnection dbConnection = new MySql.Data.MySqlClient.MySqlConnection())
        {
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

    public static void registerUser(string username, string password, string firstName, string lastName, string email)
    {
        InsertUserInfo(username, password, firstName, lastName, email);
    }

    public static void loginUser(string username, string password)
    {
        CheckUser(username, password);
    }

    public static void deleteUser(string username)
    {
        DeleteUser(username);
    }

    public static void insetStudentRecord(int userID, string chatTxt)
    {
        InsertUserRecord(userID, chatTxt);
    }
}
