using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NotificationService;

namespace Camping_Data
{
    public class DBFunctions
    {
        public string connectionString = "Server=localhost;Port=3306;Database=CampingDB;Uid=kbs;Pwd=camping;";
        private NotificationService.MainWindow notif;

        public DBFunctions()
        {
            notif = new NotificationService.MainWindow();
        }

        public bool IsConnectionAvailable()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Connection failed: {ex.Message}");
                return false;
            }
        }

        public void InsertData(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddRange(parameters);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) added.");

                    // Close connection
                    connection.Close();
                }
            }
        }

        public void UpdatePrice(int newPrice, int placeID)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE place SET price = @newPrice WHERE placeID = @placeID;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newPrice", newPrice);
                        command.Parameters.AddWithValue("@placeID", placeID);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();
                        notif.ShowInfo($"{rowsAffected} row(s) updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"An error occurred: {ex.Message}");
            }
        }

        public List<int> GetReservationsByUserID(int userID)
        {
            List<int> reservationIDs = new List<int>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT reservationID FROM reservations WHERE userID = @userID AND startDate > @currDate;";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@currDate", DateTime.Now);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int reservationID = reader.GetInt32("reservationID");
                                reservationIDs.Add(reservationID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Database connection failed: {ex.Message}");
            }

            return reservationIDs;
        }

        public List<int> GetPlaceIDsByUserID(int userID)
        {
            List<int> placeIDs = new List<int>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT placeID FROM reservations WHERE userID = @userID;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int placeID = reader.GetInt32("placeID");
                            placeIDs.Add(placeID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Error: {ex.Message}");
            }

            return placeIDs;
        }

        public int GetPlaceIDByReservationID(int reservationID)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string ReserveringDeleteQuery = "SELECT placeID FROM reservations WHERE reservationID = @reservationID ";

                    using (var command = new MySqlCommand(ReserveringDeleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@reservationID", reservationID);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int placeID))
                        {
                            return placeID;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Database connection failed: {ex.Message}");
                return -1;
            }
        }

        public void InsertReservation(int placeID, DateTime? dateStart, DateTime? dateEnd, int aantalPersonen, int userID, bool isBlock)
        {
            if (dateStart == null)
            {
                notif.ShowError("Please select a valid start date.");
                return;
            }
            if (dateEnd == null)
            {
                notif.ShowError("Please select a valid end date.");
                return;
            }

            DateTime startDate = dateStart.Value;
            DateTime endDate = dateEnd.Value;

            string query = "INSERT INTO reservations (placeID, startDate, endDate, personAmount, userID, isBlock) VALUES (@placeID, @startDate, @endDate, @personAmount, @userID, @isBlock)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@placeID", placeID);
                        cmd.Parameters.AddWithValue("@startDate", startDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@personAmount", aantalPersonen);
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@isBlock", isBlock ? 1 : 0);

                        cmd.ExecuteNonQuery();
                        notif.ShowInfo("Reservation inserted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"An error occurred: {ex.Message}");
            }
        }

        public void InsertReservation(int placeID, DateTime? dateStart, DateTime? dateEnd, int aantalPersonen, int userID)
        {
            InsertReservation(placeID, dateStart, dateEnd, aantalPersonen, userID, false);
        }

        public bool isAvailable(int placeID, DateTime startDate, DateTime endDate)
        {
            DateTime currDate = DateTime.Now;
            string query = @" SELECT COUNT(*) FROM reservations 
                              WHERE placeID = @placeID
                              AND @endDate > startDate
                              AND @startDate < endDate";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@placeID", placeID);
                        cmd.Parameters.AddWithValue("@startDate", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@currDate", currDate.ToString("yyyy-MM-dd HH:mm:ss"));

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool isBlockedOnID(int plekID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT isBlocked FROM place WHERE placeID = @placeID;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@placeID", plekID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isBlockedOnID = reader.GetBoolean("isBlocked");
                            return isBlockedOnID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Error: {ex.Message}");
            }
            return false;
        }

        public string GetDescription(int placeID)
        {
            string description = "You are not connected to the Database";
            string water = "Not Available";
            string electric = "Not Available";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string querySurface = "SELECT IFNULL(surface, '') AS Surface FROM place WHERE placeID = @plekID;";
                    MySqlCommand commandSurface = new MySqlCommand(querySurface, connection);
                    commandSurface.Parameters.AddWithValue("@plekID", placeID);
                    string surface = commandSurface.ExecuteScalar()?.ToString() ?? "";

                    string queryWater = "SELECT IFNULL(water, '') AS Water FROM place WHERE placeID = @plekID;";
                    MySqlCommand commandWater = new MySqlCommand(queryWater, connection);
                    commandWater.Parameters.AddWithValue("@plekID", placeID);
                    water = (commandWater.ExecuteScalar()?.ToString()) == "0" ? "Not Available" : "Available";

                    string queryElectric = "SELECT IFNULL(electric, '') AS Electric FROM place WHERE placeID = @plekID;";
                    MySqlCommand commandElectric = new MySqlCommand(queryElectric, connection);
                    commandElectric.Parameters.AddWithValue("@plekID", placeID);
                    electric = (commandElectric.ExecuteScalar()?.ToString()) == "0" ? "Not Available" : "Available";

                    string queryPrice = "SELECT IFNULL(price, '') AS Price FROM place WHERE placeID = @plekID;";
                    MySqlCommand commandPrice = new MySqlCommand(queryPrice, connection);
                    commandPrice.Parameters.AddWithValue("@plekID", placeID);
                    string price = commandPrice.ExecuteScalar()?.ToString() ?? "";

                    description = $"Surface: {surface}m²\n" +
                                   $"Water: {water}\n" +
                                   $"Electricity: {electric}\n" +
                                   $"Price: €{price},- /day";
                }
            }
            catch (Exception ex)
            {
                description = "You are not connected to the database";
                notif.ShowError($"Error retrieving the description: {ex.Message}");
            }
            return description;
        }

        public string GetReservationDescription(int reservationID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT placeID, startDate, endDate FROM reservations WHERE reservationID = @reservationID";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationID", reservationID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int placeID = reader.GetInt32("placeID");
                                DateTime startDate = reader.GetDateTime("startDate");
                                DateTime endDate = reader.GetDateTime("endDate");

                                return $"Place: {placeID}, from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}";
                            }
                            else
                            {
                                return $"No reservation found with ID {reservationID}.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError("Database connection failed");
                return $"An error occurred while retrieving the reservation: {ex.Message}";
            }
        }

        public string GetEmail(int userID)
        {
            string _email = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT email FROM user WHERE userID = @userID;";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _email = reader["email"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Error: {ex.Message}");
            }

            return _email;
        }

        public void DeleteReservation(int userID, int placeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string ReserveringDeleteQuery = "DELETE FROM reservations WHERE userID = @userID AND placeID = @placeID AND startDate = @startDate AND endDate = @endDate;";

                    using (var command = new MySqlCommand(ReserveringDeleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        command.Parameters.AddWithValue("@placeID", placeID);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        command.ExecuteNonQuery();
                        notif.ShowInfo("Reservation deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"An error occurred: {ex.Message}");
            }
        }

        public void DeleteReservation(int reservationID)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string ReserveringDeleteQuery = "DELETE FROM reservations WHERE reservationID = @reservationID;";

                    using (var command = new MySqlCommand(ReserveringDeleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@reservationID", reservationID);

                        command.ExecuteNonQuery();
                        notif.ShowInfo("Reservation deleted!");
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"Database connection failed: {ex.Message}");
            }
        }

        public object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    notif.ShowError($"There has been an issue while trying to connect to the database: {ex.Message}");
                    return null;
                }
            }
        }

        public byte[] GetImageFromDatabase(int placeID)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT images FROM place WHERE PlaceID = @PlaceID";

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PlaceID", placeID);

                        using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                return reader["images"] as byte[];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notif.ShowError($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}
