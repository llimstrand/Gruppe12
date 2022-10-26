using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;


namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<Suggestion> FetchSug();
        void SetSug(SuggestionViewModel model);
        IEnumerable<Suggestion> UpdateSug(int id);
        void DeleteSug(int id);
        void SetUpSug(SuggestionViewModel model);
        void SetUsers(UsersViewModel model);
        IEnumerable<User> FetchEmp();
        IEnumerable<Suggestion> SaveSug(int id);
        void SetSaveSug(SuggestionViewModel model);
        void SetTeams(TeamsViewModel model);
        IEnumerable<Team> FetchTeam();

    }
}