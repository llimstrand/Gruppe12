﻿using bacit_dotnet.MVC.Entities;
using MySqlConnector;
using bacit_dotnet.MVC.Models.Suggestions;
namespace bacit_dotnet.MVC.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }

        public IEnumerable<User> GetUsers()
        {

            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var reader = ReadData("Select id, name, email, phone from users;", connection);

            var users = new List<User>();
            while (reader.Read())
            {
                var user = new User();
                user.Id = reader.GetInt32("id");
                user.Name = reader.GetString(1);
                user.Email = reader.GetString(2);
                user.Phone = reader.GetString(3);
                users.Add(user);
            }
            connection.Close();
            return users;
        }

        private MySqlDataReader ReadData(string query, MySqlConnection conn)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader();
        }

        public void SetSug(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into suggestions(Title, Name, Team, Description, TimeStamp) values (@Tittel, @Navn, @Team, @Beskrivelse, @Dato)";
            Console.WriteLine(query);
            WriteData(query, connection, model);
            connection.Close();

        }

        private void WriteData(string query, MySqlConnection conn, SuggestionViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@Tittel", model.Title);
            command.Parameters.AddWithValue("@Navn", model.Name);
            command.Parameters.AddWithValue("@Team", model.Team);
            command.Parameters.AddWithValue("@Beskrivelse", model.Description);
            command.Parameters.AddWithValue("@Dato", model.TimeStamp);
            command.ExecuteNonQuery(); 
        }
    }
}
