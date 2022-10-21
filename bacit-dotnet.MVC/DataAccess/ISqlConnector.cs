using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;


namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<User> GetEmployee();
        IEnumerable<Suggestion> FetchSug();
        void SetSug(SuggestionViewModel model);
         IEnumerable<Suggestion> UpdateSug(int id);
         void SetUpSug(SuggestionViewModel model);
         void SetUsers(UsersViewModel model);
    }
}