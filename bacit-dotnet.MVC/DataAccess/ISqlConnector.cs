using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;
using System.Data;
/*Denne filen sier at disse funksjonene fungerer*/
namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<Team> FetchTeam();
        void SetTeam(TeamsViewModel model);
        IEnumerable<Team> ViewTeams(int id);
        void DeleteTeam(int id);
        IEnumerable<Team> UpdateTeam(int id);
        void SetUpTeam(TeamsViewModel model);
        void SetUsers(UsersViewModel model);
        IEnumerable<User> FetchEmp();
        void DeleteEmp(int id);
        IEnumerable<Suggestion> FetchSug();
        IEnumerable<Proposer> FetchProposer();
        IEnumerable<Suggestion> SaveSug(int id);
        IEnumerable<User> ViewEmp(int id);
        void SetSug(SuggestionViewModel model);
         void SetUpSug(SuggestionViewModel model);
        IEnumerable<Suggestion> UpdateSug(int id);
        void DeleteSug(int id);
        IEnumerable<User> FetchEmpByID(int id);
        IEnumerable<Proposer> FetchProByID(int id);
        IEnumerable<User> FetchExByID(int id);
        void SetProposer(SuggestionViewModel model);
        void SetExecutor(SuggestionViewModel model);
        IEnumerable<User> UpdateEmp(int id);
        void SetUpEmp(UsersViewModel model);
        IEnumerable<User> FetchStatEmpEx();
        IEnumerable<User> FetchStatEmpPr();
        IEnumerable<Team> FetchStatTeamEx();
        IEnumerable<Team> FetchStatTeamPr();
        void SetMember(TeamsViewModel model);
        IEnumerable<Team> ViewMembers(int id);
        IEnumerable<Suggestion> FetchSugByPlan();
        IEnumerable<Suggestion> FetchSugByDo();
        IEnumerable<Suggestion> FetchSugByStudy();
        IEnumerable<Suggestion> FetchSugByAct();
        IDbConnection GetDbConnection();
        IEnumerable<User> FetchEmpByTeamID(int id);
        void DeleteMember(int id);

    }
}