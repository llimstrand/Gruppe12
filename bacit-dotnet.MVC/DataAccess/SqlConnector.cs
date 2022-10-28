using bacit_dotnet.MVC.Entities;
using MySqlConnector;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;
namespace bacit_dotnet.MVC.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }
        /*Henter alle team*/
        public  IEnumerable<Team> FetchTeam() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Teams = new List<Team>();
            var reader = ReadData("select Team_ID, Team_Navn, Team_Leder from team", connection);
            while (reader.Read())
            {
                var user = new Team();
                user.Team_ID = reader.GetInt32("Team_ID");
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Team_Leder = reader.GetString("Team_leder");
                Teams.Add(user);
            }
            connection.Close();
            return Teams;


        }
       
        /*Lage nytt team*/
        public void SetTeams(TeamsViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into team(Team_ID, Team_Navn, Team_Leder) values (@teamnummer, @teamnavn, @teamleder)";
            Console.WriteLine(query);
            WriteData(query, connection, model);
            connection.Close();
        }

        private void WriteData(string query, MySqlConnection conn, TeamsViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@teamnummer", model.Team_ID);
            command.Parameters.AddWithValue("@teamnavn", model.Team_Navn);
            command.Parameters.AddWithValue("@teamleder", model.Team_Leder);
            command.ExecuteNonQuery(); 
        }

        /*Lage ny ansatt*/
        public void SetUsers(UsersViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into employee(Emp_Nr, Emp_Navn, Emp_Passord) values (@AnsattNummer, @AnsattNavn, @AnsattPassord)";
            WriteDataUsers(query, connection, model);
            connection.Close();

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
                user.Executor_Nr = reader.GetInt32("Emp_Nr");
                Users.Add(user);
            }
            connection.Close();
            return Users;


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

        /*Henter alle forslag*/
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

         /*Hente et enkelt forslag*/
        public  IEnumerable<Suggestion> SaveSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadDatawithID("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet, Sug_TimeStamp from suggestions where Sug_ID = @id", connection, id);
            while (reader.Read())
            {   
                DateTime date = reader.GetDateTime("Sug_Timestamp");
                var user = new Suggestion();
                user.Sug_ID = reader.GetInt32("Sug_ID");
                user.Sug_Overskrift = reader.GetString("Sug_Overskrift");
                user.Sug_Beskrivelse = reader.GetString("Sug_Beskrivelse");
                user.Sug_Ansvarlig = reader.GetString("Sug_Ansvarlig");
                user.Sug_Status = reader.GetString("Sug_Status");
                user.Sug_Frist = reader.GetString("Sug_Frist");
                user.Sug_Varighet = reader.GetString("Sug_Varighet");
                user.Sug_Timestamp = date.ToString("D");
                Suggestions.Add(user);
            }
            connection.Close();
            return Suggestions;


        }
        
        /*Lage nytt forslag*/
        public void SetSug(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into suggestions(Sug_ID,Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet, Sug_Timestamp) values (@ID, @Overskrift, @Beskrivelse, @Ansvarlig, @Status, @Frist, @Varighet, @Timestamp)";
            Console.WriteLine(query);
            WriteData(query, connection, model);
            connection.Close();

        }

        private void WriteData(string query, MySqlConnection conn, SuggestionViewModel model)
        {
          
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ID",model.Sug_ID);
            command.Parameters.AddWithValue("@Overskrift", model.Sug_Overskrift);
            command.Parameters.AddWithValue("@Beskrivelse", model.Sug_Beskrivelse);
            command.Parameters.AddWithValue("@Ansvarlig", model.Sug_Ansvarlig);
            command.Parameters.AddWithValue("@Status", model.Sug_Status);
            command.Parameters.AddWithValue("@Frist", model.Sug_Frist);
            command.Parameters.AddWithValue("@Varighet", model.Sug_Varighet);
            command.Parameters.AddWithValue("@Timestamp", model.Sug_Timestamp);
            command.ExecuteNonQuery(); 
        }

        /*Oppdaterer forslagsverdier*/
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

        /*sende inn det redigerte forslaget*/
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

        private void DeleteData(string query, MySqlConnection conn, int id){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

        }

        /*Sletter et forslag*/
          public  void DeleteSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            DeleteData("Delete from Suggestions where Sug_ID = @id", connection, id);
            connection.Close();
        }

        public void SetProposer(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "insert into proposer(Emp_Nr, Sug_ID) values (@ansattnummer, @forslagsid)";
            Console.WriteLine(query);
            WriteProposer(query, connection, model);
            connection.Close();

        }

        private void WriteProposer(string query, MySqlConnection conn, SuggestionViewModel model)
        {
          
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@forslagsid", model.Sug_ID);
            command.ExecuteNonQuery(); 
        }

        public void SetExecutor(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "insert into executor(Emp_Nr, Sug_ID) values (@ansattnummer, @forslagsid)";
            Console.WriteLine(query);
            WriteExecutor(query, connection, model);
            connection.Close();

        }

        private void WriteExecutor(string query, MySqlConnection conn, SuggestionViewModel model)
        {
          
            Console.WriteLine(model.Executor_Nr);
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ansattnummer", model.Executor_Nr);
            command.Parameters.AddWithValue("@forslagsid", model.Sug_ID);
            command.ExecuteNonQuery(); 
        }

    }
}
