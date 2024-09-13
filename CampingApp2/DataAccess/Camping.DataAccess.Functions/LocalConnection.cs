using MySqlConnector;
using System;
using System.Windows;

public class LocalConnection
{
    private string server;
    private string database;
    private string username;
    private string password;
    private string _connectionString;

    public LocalConnection(string server, string database, string username, string password)
    {
        _connectionString = $"Server={server}; database={database}; UID={username}; password={password};";
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    public void OpenConnection(MySqlConnection connection)
    {
        try
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                Console.WriteLine("Connection opened successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while opening the connection: " + ex.Message);
        }
    }

    public void CloseConnection(MySqlConnection connection)
    {
        try
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                Console.WriteLine("Connection closed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while closing the connection: " + ex.Message);
        }
    }

    public string GetConnectionString()
    {
        return $"Server={server};Database={database};Uid={username};Pwd={password};";
    }

}
