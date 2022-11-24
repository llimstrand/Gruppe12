using bacit_dotnet.MVC.Entities;
using MySqlConnector;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;
using System.Data;
using System.Data.Common;
namespace bacit_dotnet.MVC.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        /**/
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }
        /**/
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(config.GetConnectionString("MariaDb"));
        }
        /**/
        private MySqlDataReader ReadData(string query, MySqlConnection conn)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            return command.ExecuteReader();
        }
        /**/
        private MySqlDataReader ReadDatawithID(string query, MySqlConnection conn, int id){
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@id",id);
            return command.ExecuteReader();
        }
        /*Viser alle team og teller medlemmer*/
        public  IEnumerable<Team> FetchTeam() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Teams = new List<Team>();
            var reader = ReadData("SELECT team.Team_ID, team.Team_Navn, team.Team_Leder, member.Team_ID, COUNT(member.Emp_Nr) AS Antall_Medlemmer FROM team, member where team.Team_ID = member.Team_ID GROUP BY team.Team_Navn ORDER BY team.Team_Navn", connection);
            while (reader.Read())
            {
                var user = new Team();
                user.Team_ID = reader.GetInt32("Team_ID");
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Team_Leder = reader.GetString("Team_leder");
                user.Antall_Medlemmer = reader.GetInt32("Antall_Medlemmer");
                Teams.Add(user);
            }
            connection.Close();
            return Teams;
        }
       
        /*Lage nytt team*/
        public void SetTeam(TeamsViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into team(Team_ID, Team_Navn, Team_Leder) values (@teamnummer, @teamnavn, @teamleder)";
            WriteTeam(query, connection, model);
            connection.Close();
        } 
         /**/
        private void WriteTeam(string query, MySqlConnection conn, TeamsViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@teamnummer", model.Team_ID);
            command.Parameters.AddWithValue("@teamnavn", model.Team_Navn);
            command.Parameters.AddWithValue("@teamleder", model.Team_Leder);
            command.ExecuteNonQuery(); 
        }
        /*Viser et enkelt team*/
        public IEnumerable<Team> ViewTeams(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
          
            var teams = new List<Team>();
            var reader = ReadDatawithID("select Team_ID, Team_Navn, Team_Leder from team where Team_ID = @id", connection, id);
            while (reader.Read())          
            {
                var user = new Team();
                user.Team_ID = reader.GetInt32("Team_ID");
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Team_Leder = reader.GetString("Team_Leder");
                teams.Add(user);
            }
            connection.Close();
            return teams;
        }
        /*Sletter et enkelt team*/
        public void DeleteTeam(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            DeleteData("Delete from team where Team_ID = @id", connection, id);
            connection.Close();
        }
        /**/
        private void DeleteData(string query, MySqlConnection conn, int id){
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        /*Oppdaterer et enkelt team*/
        public void SetUpTeam(TeamsViewModel model){
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Update team set Team_Navn = @Navn, Team_Leder = @Leder where Team_ID = @id;";
            UpdateTeamData(query, connection, model);
            connection.Close();
        }
        /*Setter inn data*/
        private void UpdateTeamData(string query, MySqlConnection conn, TeamsViewModel model){
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@Navn", model.Team_Navn);
            command.Parameters.AddWithValue("@Leder", model.Team_Leder);
            command.Parameters.AddWithValue("@Id", model.Team_ID);
            command.ExecuteNonQuery();
        }
        /*Henter oppdatert data*/
        public IEnumerable<Team> UpdateTeam(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var teams = new List<Team>();
            var reader = ReadDatawithID("select Team_ID, Team_navn, Team_Leder from team where Team_ID = @id", connection, id);
            while (reader.Read())
            {
                var user = new Team();
                user.Team_ID = reader.GetInt32("Team_ID");
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Team_Leder = reader.GetString("Team_Leder");
                teams.Add(user);
            }
            connection.Close();
            return teams;
        }
       
        /*hente alle ansatte som tilhører et enkelt team*/
        public  IEnumerable<User> FetchEmpByTeamID(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Teams = new List<User>();
            var reader = ReadDatawithID("SELECT member.Emp_Nr, employee.Emp_Navn, employee.Emp_Passord, member.Team_ID FROM employee, member, team where member.Team_ID = @id and member.Emp_Nr = employee.Emp_Nr group by member.Emp_Nr;", connection,id);
            while (reader.Read())
            {
                var user = new User();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_Navn");
                user.Emp_Passord = reader.GetString("Emp_Passord");
                user.Executor_Nr = reader.GetInt32("Emp_Nr");
                Teams.Add(user);
            }
            connection.Close();
            return Teams;
        }
        /*Lage en ny medlem*/
        public void SetMember(TeamsViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "insert into member(Emp_Nr, Team_ID) values (@ansattnummer, @teamid)";
            Console.WriteLine(query);
            WriteMember(query, connection, model);
            connection.Close();
        }
        /**/
        private void WriteMember(string query, MySqlConnection conn, TeamsViewModel model)
        {
            Console.WriteLine(model.Emp_Nr);
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@teamid", model.Team_ID);
            command.ExecuteNonQuery(); 
        }
        /*Se alle medlemmer for det enkelte team*/
         public IEnumerable<Team> ViewMembers(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
          
            var teams = new List<Team>();
            var reader = ReadDatawithID("Select employee.Emp_Navn, member.Emp_Nr, team.Team_ID from member, employee, team where employee.Emp_Nr = member.Emp_Nr and member.Team_ID = team.Team_ID and team.Team_ID = @id", connection, id);
            while (reader.Read())
          
            {
                var user = new Team();
                user.Team_ID = reader.GetInt32("Team_ID");
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_Navn");
                teams.Add(user);
            }
            connection.Close();
            return teams;
        }
        /*Slette et enkelt medlem*/
        public  void DeleteMember(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            DeleteData("Delete from member where Emp_Nr = @id", connection, id);
            connection.Close();
        }
        /*Lage ny ansatt*/
        public void SetUsers(UsersViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "Insert into employee(Emp_Nr, Emp_Navn, Emp_Passord) values (@ansattnummer, @ansattnavn, @ansattpassord)";
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
        /*Vise en enkelt ansatt*/
         public  IEnumerable<User> FetchEmpByID(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<User>();
            var reader = ReadDatawithID("select Emp_Nr, Emp_Navn, Emp_Passord from employee where Emp_Nr = @id", connection, id);
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
        /*?*/
         public  IEnumerable<User> ViewEmp(int id) {
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
        /*Slette enkelt ansatt*/
         public void DeleteEmp(int id) {
           using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
           connection.Open();
           DeleteData("Delete from employee where Emp_nr = @id", connection, id);
           connection.Close();
        }
        /**/
        private void WriteDataUsers(string query, MySqlConnection conn, UsersViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@ansattnavn", model.Emp_Navn);
            command.Parameters.AddWithValue("@ansattpassord", model.Emp_Passord);
            command.ExecuteNonQuery(); 
        }
        /*Oppdatere en ansatt*/
        public void SetUpEmp(UsersViewModel model){
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            Console.WriteLine(model.Emp_Navn);
            var query = "Update employee set Emp_navn = @ansattnavn, Emp_Nr = @ansattnummer, Emp_passord = @ansattpassord where Emp_Nr = @Ansattnummer;";
            UpdateEmpData(query, connection, model);
            Console.WriteLine("UpdateData");
            connection.Close();
        }
        /**/
        private void UpdateEmpData(string query, MySqlConnection conn, UsersViewModel model){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@Ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@Ansattnavn", model.Emp_Navn);
            command.Parameters.AddWithValue("@Ansattpassord", model.Emp_Passord);
            command.ExecuteNonQuery();
        }
        /*Vise den oppdaterte ansatte*/
        public  IEnumerable<User> UpdateEmp(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var users = new List<User>();
            var reader = ReadDatawithID("select Emp_Nr, Emp_navn, Emp_passord from employee where Emp_Nr = @id", connection, id);
            while (reader.Read())
            {
                var user = new User();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_navn");
                user.Emp_Passord = reader.GetString("Emp_passord");
                users.Add(user);
            }
            connection.Close();
            return users;
        }
        /*Vise enkelt proposer på forslag*/
        public  IEnumerable<Proposer> FetchProByID(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<Proposer>();
            var reader = ReadDatawithID("Select proposer.Emp_Nr, employee.Emp_Navn from proposer inner join employee where Sug_ID = @id and proposer.Emp_Nr = employee.Emp_Nr", connection, id);
            while (reader.Read())
            {
                var user = new Proposer();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_PrNavn = reader.GetString("Emp_Navn");
                Console.WriteLine(user.Emp_Nr);
                Users.Add(user);
            }
            connection.Close();
            return Users;
        }
        /*Vise enkelt executor på forslag*/
        public  IEnumerable<User> FetchExByID(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<User>();
            var reader = ReadDatawithID("Select executor.Emp_Nr, employee.Emp_Navn from executor inner join employee where Sug_ID = @id and executor.Emp_Nr = employee.Emp_Nr", connection, id);
            while (reader.Read())
            {
                var user = new User();
                user.Executor_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_ExNavn = reader.GetString("Emp_Navn");
                Users.Add(user);
            }
            connection.Close();
            return Users;
        }
        /*Hvorfor?? Viser alle proposere*/
        public  IEnumerable<Proposer> FetchProposer() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var proposers = new List<Proposer>();
            var reader = ReadData("Select proposer.Emp_Nr, employee.Emp_Navn from proposer inner join employee where proposer.Emp_Nr = employee.Emp_Nr", connection);
            while (reader.Read())
            {

                var propose = new Proposer();
                propose.Emp_Nr = reader.GetInt32("Emp_Nr");
                propose.Emp_PrNavn = reader.GetString("Emp_Navn");            
                proposers.Add(propose);
            }
            connection.Close();
            return proposers;
        }
        /*? Oppdaterer proposer*/
        private void UpdateProposer(string query2, MySqlConnection conn, SuggestionViewModel model){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query2;
            command.Parameters.AddWithValue("@ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@id", model.Sug_ID);
            command.ExecuteNonQuery();
        }
        /*? Oppdaterer executor*/
        private void UpdateExecutor(string query3, MySqlConnection conn, SuggestionViewModel model){
             Console.WriteLine("Model");
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query3;
            command.Parameters.AddWithValue("@executornummer", model.Executor_Nr);
            command.Parameters.AddWithValue("@id", model.Sug_ID);
            command.ExecuteNonQuery();
        }        
        /*Legge til proposer*/
        public void SetProposer(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "insert into proposer(Emp_Nr, Sug_ID) values (@ansattnummer, @forslagsid)";
            Console.WriteLine(query);
            WriteProposer(query, connection, model);
            connection.Close();
        }
        /**/
        private void WriteProposer(string query, MySqlConnection conn, SuggestionViewModel model)
        {
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@ansattnummer", model.Emp_Nr);
            command.Parameters.AddWithValue("@forslagsid", model.Sug_ID);
            command.ExecuteNonQuery(); 
        }
        /*Legge til executor*/
        public void SetExecutor(SuggestionViewModel model)
        {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDB"));
            connection.Open();
            var query = "insert into executor(Emp_Nr, Sug_ID) values (@executornummer, @forslagsid)";
            Console.WriteLine(query);
            WriteExecutor(query, connection, model);
            connection.Close();
        }
        /**/
        private void WriteExecutor(string query, MySqlConnection conn, SuggestionViewModel model)
        {
            Console.WriteLine(model.Executor_Nr);
            using var command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
            command.Parameters.AddWithValue("@executornummer", model.Executor_Nr);
            command.Parameters.AddWithValue("@forslagsid", model.Sug_ID);
            command.ExecuteNonQuery(); 
        }
        /*Viser alle forslag*/
        public  IEnumerable<Suggestion> FetchSug() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions ORDER BY Sug_Timestamp DESC", connection);
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
                user.Sug_Timestamp = date.ToString("g");
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
        /**/
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
            Console.WriteLine(model.Emp_Nr);
            var query2= "Update proposer set Emp_Nr = @ansattnummer where Sug_ID = @id;";
            Console.WriteLine(model.Emp_Nr);
            var query3= "Update executor set Emp_Nr = @executornummer where Sug_ID = @id;";
            UpdateData(query, connection, model);
            UpdateProposer(query2, connection, model);
            UpdateExecutor(query3, connection, model);
            Console.WriteLine("UpdateData");
            connection.Close();
        }
        /*vise det redigerte forslaget*/
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
        /**/
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
        
        /*Sletter et forslag*/
        public  void DeleteSug(int id) {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();
            DeleteData("Delete from executor where Sug_ID = @id", connection,id);
            DeleteData("Delete from proposer where Sug_ID = @id", connection, id);
            DeleteData("Delete from suggestions where Sug_ID = @id", connection, id);
            connection.Close();
        }
        /*Vise alle forslag som har status plan*/
        public  IEnumerable<Suggestion> FetchSugByPlan() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_Status = 'plan' ORDER BY Sug_Timestamp DESC", connection);
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
        /*Vise alle forslag som har status do*/
        public  IEnumerable<Suggestion> FetchSugByDo() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_Status = 'do' ORDER BY Sug_Timestamp DESC", connection);
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
        /*Vise alle forslag som har status study*/
        public  IEnumerable<Suggestion> FetchSugByStudy() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_Status = 'study' ORDER BY Sug_Timestamp DESC", connection);
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
        /*Vise alle forslag som har status act*/
        public  IEnumerable<Suggestion> FetchSugByAct() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Suggestions = new List<Suggestion>();
            var reader = ReadData("select Sug_ID, Sug_Overskrift, Sug_Beskrivelse, Sug_Ansvarlig, Sug_Status, Sug_Frist, Sug_Varighet from suggestions where Sug_Status = 'act' ORDER BY Sug_Timestamp DESC", connection);
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
        /*Statistikk: Vise alle ansatte executor og få opp antall forslag de har utført*/
         public  IEnumerable<User> FetchStatEmpEx() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<User>();
            var reader = ReadData("SELECT executor.Emp_Nr, employee.Emp_Navn, COUNT(executor.Emp_Nr) AS Antall_Forslag FROM executor, employee WHERE executor.Emp_Nr = employee.Emp_Nr GROUP BY executor.Emp_Nr ORDER BY Antall_Forslag DESC", connection);
            while (reader.Read())
            {
                var user = new User();
                user.Executor_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_Navn");
                user.Antall_Forslag = reader.GetInt32("Antall_Forslag");
                Users.Add(user);
            }
            connection.Close();
            return Users;
        }
        /*Statistikk: Vise alle ansatte proposer og få opp antall forslag de har sendt inn*/
         public  IEnumerable<User> FetchStatEmpPr() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Users = new List<User>();
            var reader = ReadData("SELECT proposer.Emp_Nr, employee.Emp_Navn, COUNT(proposer.Emp_Nr) AS Antall_Forslag FROM proposer, employee WHERE proposer.Emp_Nr = employee.Emp_Nr GROUP BY proposer.Emp_Nr ORDER BY Antall_Forslag DESC", connection);
            while (reader.Read())
            {
                var user = new User();
                user.Emp_Nr = reader.GetInt32("Emp_Nr");
                user.Emp_Navn = reader.GetString("Emp_Navn");
                user.Antall_Pr_Forslag = reader.GetInt32("Antall_Forslag");
                Users.Add(user);
            }
            connection.Close();
            return Users;
        }
        /*Statistikk: Vise alle team executor og få opp antall forslag de har utført*/
        public  IEnumerable<Team> FetchStatTeamEx() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Teams = new List<Team>();
            var reader = ReadData("SELECT team.Team_Navn, COUNT(executor.Emp_Nr) AS Antall_Utf_Forslag FROM executor, employee, team, member WHERE executor.Emp_Nr = employee.Emp_Nr AND employee.Emp_Nr = member.Emp_Nr AND member.Team_ID = team.Team_ID GROUP BY team.Team_Navn ORDER BY Antall_Utf_Forslag DESC", connection);
            while (reader.Read())
            {
                var user = new Team();
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Antall_Forslag = reader.GetInt32("Antall_Utf_Forslag");
                Teams.Add(user);
            }
            connection.Close();
            return Teams;
        }
        /*Statistikk: Vise alle team proposer og få opp antall forslag de har sendt inn*/
        public  IEnumerable<Team> FetchStatTeamPr() {
            using var connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
            connection.Open();

            var Teams = new List<Team>();
            var reader = ReadData("SELECT team.Team_Navn, COUNT(proposer.Emp_Nr) AS Antall_Utf_Forslag FROM proposer, employee, team, member WHERE proposer.Emp_Nr = employee.Emp_Nr AND employee.Emp_Nr = member.Emp_Nr AND member.Team_ID = team.Team_ID GROUP BY team.Team_Navn ORDER BY Antall_Utf_Forslag DESC", connection);
            while (reader.Read())
            {
                var user = new Team();
                user.Team_Navn = reader.GetString("Team_Navn");
                user.Antall_Pr_Forslag = reader.GetInt32("Antall_Utf_Forslag");
                Teams.Add(user);
            }
            connection.Close();
            return Teams;
        }   
    }
}
