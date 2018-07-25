using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace SmallBall
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void LogInBtn_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            try
            {
                SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Joshua\\source\\repos\\ConsoleApp1\\ConsoleApp1\\SmallBallDB.mdf;Integrated Security=True;Connect Timeout=30");
                sql.Open();
                string s = "SELECT * FROM users WHERE Id='" + UserName.Text + "' AND Password='" + Password.Text + "';";
                SqlCommand command = new SqlCommand(s, sql);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Server.Transfer("MainMenu.aspx");
                }
                else
                {
                    Password.Text = "";
                    ErrorLabel.Text = "Invalid Username or Password";
                    ErrorLabel.Visible = true;
                }
            }
            catch(SqlException exception)
            {
                ErrorLabel.Text = exception.Message;
                ErrorLabel.Visible = true;
            }
        }
    }

    //class SQL_Connect
    //{
    //    private MySqlConnection connection;
    //    private string server;
    //    private string database;
    //    private string username;
    //    private string password;

    //    public SQL_Connect()
    //    {
    //        Initialize();
    //    }

    //    private void Initialize()
    //    {
    //        server = "localhost";
    //        database = "smallball";
    //        username = "root";
    //        password = "root";
    //        string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + username + ";PASSWORD=" + password + ";";
    //        connection = new MySqlConnection(connectionString);
    //        test();
    //    }

    //    private bool OpenConnection()
    //    {
    //        try
    //        {
    //            connection.Open();
    //            return true;
    //        }
    //        catch (MySqlException exception)
    //        {
    //            switch (exception.Number)
    //            {
    //                case 0:
    //                    System.Diagnostics.Debug.WriteLine("Cannot connect to server");
    //                    break;
    //                case 1045:
    //                    System.Diagnostics.Debug.WriteLine("Invalid username/password");
    //                    break;
    //            }
    //            return false;
    //        }
    //    }

    //    private bool CloseConnection()
    //    {
    //        try
    //        {
    //            connection.Close();
    //            return true;
    //        }
    //        catch (MySqlException exception)
    //        {
    //            System.Diagnostics.Debug.WriteLine(exception.Message);
    //            return false;
    //        }
    //    }

    //    public bool test()
    //    {
    //        return OpenConnection() && CloseConnection();
    //    }

    //    public string RandomTeamName()
    //    {
    //        string query = "SELECT Name from Teams ORDER BY RAND() LIMIT 1";
    //        if (OpenConnection())
    //        {
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            MySqlDataReader dataReader = command.ExecuteReader();
    //            dataReader.Read();
    //            var data = dataReader["Name"];
    //            CloseConnection();
    //            dataReader.Close();
    //            return (string)data;
    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }

    //    public City RandomCity()
    //    {
    //        string query = "SELECT * from Cities ORDER BY RAND() LIMIT 1";
    //        City result = new City();

    //        if (OpenConnection())
    //        {
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            MySqlDataReader dataReader = command.ExecuteReader();
    //            dataReader.Read();
    //            var c = dataReader["City"];
    //            dataReader.Read();
    //            var st = dataReader["State"];
    //            var div = dataReader["Division"];
    //            CloseConnection();
    //            dataReader.Close();
    //            result = new City((string)c, "state", (string)div);
    //            return result;
    //        }
    //        else
    //        {
    //            return result;
    //        }
    //    }

    //    public string[] populateTeamNames()
    //    {
    //        string[] teams = new string[579];
    //        string query = "SELECT * FROM Teams ORDER BY Name";
    //        if (OpenConnection())
    //        {
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            MySqlDataReader dataReader = command.ExecuteReader();
    //            for (int i = 0; i < 579; i++)
    //            {
    //                teams[i] = (string)dataReader["name"];
    //            }
    //            CloseConnection();
    //            dataReader.Close();
    //            return teams;
    //        }
    //        else
    //        {
    //            return teams;
    //        }
    //    }

    //    public City[] populateCities()
    //    {
    //        City[] cities = new City[256];
    //        string query = "SELECT * FROM Cities ORDER BY State";
    //        if (OpenConnection())
    //        {
    //            MySqlCommand command = new MySqlCommand(query, connection);
    //            MySqlDataReader dataReader = command.ExecuteReader();
    //            for (int i = 0; i < 256; i++)
    //            {
    //                var c = dataReader["City"];
    //                var st = dataReader["State"];
    //                var div = dataReader["Division"];
    //                cities[i] = new City((string)c, (string)st, (string)div);
    //            }
    //            CloseConnection();
    //            dataReader.Close();
    //            return cities;
    //        }
    //        else
    //        {
    //            return cities;
    //        }
    //    }


    //}
}