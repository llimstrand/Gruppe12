using bacit_dotnet.MVC.Entities;
using MySqlConnector;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
namespace bacit_dotnet.MVC.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }

            /*Henter alle ansatte*/
        public  IEnumerable<User> FetchEmp() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<User>();
            var reader = ReadData("select Emp_Nr, Emp_Navn, Emp_Passord from employee", connection);
            while (reader.Read())
            {
                var user = new User();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_Navn");
                user.Emp_Passord = reader.GetString("Emp_Passord");
                Users.Add(user);
            }
            connection.Close();
            return Users;


        }
        public IEnumerable<User> GetEmployee()
        {

            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var reader = ReadData("Select Emp_Nr, Emp_Navn, Emp_Passord from employee;", connection);

            var users = new List<User>();
            while (reader.Read())
            {
                var user = new User();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString(1);
                user.Emp_Passord = reader.GetString(2);
                users.Add(user);
            }
            connection.Close();
            return users;
        }

        public IEnumerable<Suggestion> GetSuggestions()
        {

            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var reader = ReadData("Select Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions;", connection);

            var suggestions = new List<Suggestion>();
            while (reader.Read())
            {
                var suggestion = new Suggestion();
                suggestion.Sug_Overskrift = reader.GetString(1);
                suggestion.Sug_Beskrivelse = reader.GetString(2);
                suggestion.Sug_Ansvarlig = reader.GetString(3);
                suggestion.Sug_Status = reader.GetString(4);
                suggestion.Sug_Frist = reader.GetString(5);
                suggestion.Sug_Varighet = reader.GetString(6);
                suggestions.Add(suggestion);
            }
            connection.Close();
            return suggestions;
        }

        private MySqlDataReader ReadData(string query, MySqlConnection conn)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader();
        }
        private MySqlDataReader ReadDatawithID(string query, MySqlConnection conn, int id){
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@id",id);
            return command.ExecuteReader();
        }

        public void SetSug(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into suggestions(Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet, Sug_Timestamp) values (@Overskrift, @Beskrivelse, @Ansvarlig, @Status, @Frist, @Varighet, @Timestamp)";
            Console.WriteLine(query);
            WriteData(query, connection, model);
            connection.Close();

        }
        public void SetSaveSug(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into suggestions(Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet, Sug_Timestamp) values (@Overskrift, @Beskrivelse, @Ansvarlig, @Status, @Frist, @Varighet, @Timestamp)";
            Console.WriteLine(query);
            WriteData(query, connection, model);
            connection.Close();

        }
        private void WriteData(string query, MySqlConnection conn, SuggestionViewModel model)
        {
          
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@Overskrift", model.Sug_Overskrift);
            command.Parameters.AddWithValue("@Beskrivelse", model.Sug_Beskrivelse);
            command.Parameters.AddWithValue("@Ansvarlig", model.Sug_Ansvarlig);
            command.Parameters.AddWithValue("@Status", model.Sug_Status);
            command.Parameters.AddWithValue("@Frist", model.Sug_Frist);
            command.Parameters.AddWithValue("@Varighet", model.Sug_Varighet);
            command.Parameters.AddWithValue("@Timestamp", model.Sug_Timestamp);
            command.ExecuteNonQuery(); 
        }

        public void SetUpSug(SuggestionViewModel model){
               using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            Console.WriteLine(model.Sug_Overskrift);
            var query = "Update suggestions set Sug_Overskrift = @Overskrift, Sug_Beskrivelse = @Beskrivelse, Sug_Ansvarlig = @Ansvarlig, Sug_Status = @Status, Sug_Frist = @Frist, Sug_Varighet = @Varighet where Sug_ID = @id;";
            UpdateData(query, connection, model);
            Console.WriteLine("UpdateData");
            connection.Close();
        }

        private void UpdateData(string query, MySqlConnection conn, SuggestionViewModel model){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@Overskrift", model.Sug_Overskrift);
            command.Parameters.AddWithValue("@Beskrivelse", model.Sug_Beskrivelse);
            command.Parameters.AddWithValue("@Ansvarlig", model.Sug_Ansvarlig);
            command.Parameters.AddWithValue("@Status", model.Sug_Status);
            command.Parameters.AddWithValue("@Frist", model.Sug_Frist);
            command.Parameters.AddWithValue("@Varighet", model.Sug_Varighet);
            command.Parameters.AddWithValue("@id", model.Sug_ID);
            command.ExecuteNonQuery();

        }


        private void DeleteData(string query, MySqlConnection conn, int id){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

        }
   

        public  IEnumerable<Suggestion> FetchSug() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions", connection);
            while (reader.Read())
            {
                var user = new Suggestion();
                user.Sug_ID = reader.GetInt32("Sug_ID");
                user.Sug_Overskrift = reader.GetString("Sug_Overskrift");
                user.Sug_Beskrivelse = reader.GetString("Sug_Beskrivelse");
                user.Sug_Ansvarlig = reader.GetString("Sug_Ansvarlig");
                user.Sug_Status = reader.GetString("Sug_Status");
                user.Sug_Frist = reader.GetString("Sug_Frist");
                user.Sug_Varighet = reader.GetString("Sug_Varighet");
                Suggestions.Add(user);
            }
            connection.Close();
            return Suggestions;

        }
        

         public  IEnumerable<Suggestion> UpdateSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadDatawithID("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_ID = @id", connection, id);
            while (reader.Read())
            {
                var user = new Suggestion();
                user.Sug_ID = reader.GetInt32("Sug_ID");
                user.Sug_Overskrift = reader.GetString("Sug_Overskrift");
                user.Sug_Beskrivelse = reader.GetString("Sug_Beskrivelse");
                user.Sug_Ansvarlig = reader.GetString("Sug_Ansvarlig");
                user.Sug_Status = reader.GetString("Sug_Status");
                user.Sug_Frist = reader.GetString("Sug_Frist");
                user.Sug_Varighet = reader.GetString("Sug_Varighet");
                Suggestions.Add(user);
            }
            connection.Close();
            return Suggestions;


        }
        public  IEnumerable<Suggestion> SaveSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadDatawithID("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_ID = @id", connection, id);
            while (reader.Read())
            {
                var user = new Suggestion();
                user.Sug_ID = reader.GetInt32("Sug_ID");
                user.Sug_Overskrift = reader.GetString("Sug_Overskrift");
                user.Sug_Beskrivelse = reader.GetString("Sug_Beskrivelse");
                user.Sug_Ansvarlig = reader.GetString("Sug_Ansvarlig");
                user.Sug_Status = reader.GetString("Sug_Status");
                user.Sug_Frist = reader.GetString("Sug_Frist");
                user.Sug_Varighet = reader.GetString("Sug_Varighet");
                Suggestions.Add(user);
            }
            connection.Close();
            return Suggestions;


        }

        public void SetUsers(UsersViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into employee(Emp_Nr, Emp_Navn, Emp_Passord) values (@AnsattNummer, @AnsattNavn, @AnsattPassord)";
            WriteDataUsers(query, connection, model);
            connection.Close();

        }

        private void WriteDataUsers(string query, MySqlConnection conn, UsersViewModel model)
        {
          
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@AnsattNummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@AnsattNavn", model.Emp_Navn);
            command.Parameters.AddWithValue("@AnsattPassord", model.Emp_Passord);
            command.ExecuteNonQuery(); 
        }
          public  void DeleteSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            DeleteData("Delete from Suggestions where Sug_ID = @id", connection, id);
            connection.Close();
        }

    }
}
