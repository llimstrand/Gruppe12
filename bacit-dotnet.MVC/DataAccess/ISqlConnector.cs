using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;


namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<Team> FetchTeam();
        void SetTeam(TeamsViewModel model);
        IEnumerable<Team> ViewTeams(int id);
        void DeleteTeam(int id);

      
        void SetUsers(UsersViewModel model);
        IEnumerable<User> FetchEmp();
        IEnumerable<Suggestion> FetchSug();
        IEnumerable<Suggestion> SaveSug(int id);
          IEnumerable<User> ViewEmp(int id);
        void SetSug(SuggestionViewModel model);
         void SetUpSug(SuggestionViewModel model);
        IEnumerable<Suggestion> UpdateSug(int id);
        void DeleteSug(int id);
        IEnumerable<User> FetchEmpByID(int id);
        void SetProposer(SuggestionViewModel model);
        void SetExecutor(SuggestionViewModel model);
        IEnumerable<User> UpdateEmp(int id);
        void SetUpEmp(UsersViewModel model);

    }
}