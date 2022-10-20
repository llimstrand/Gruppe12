using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;

namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Suggestion> FetchSug();
        void SetSug(SuggestionViewModel model);
         IEnumerable<Suggestion> UpdateSug(int id);
         void SetUpSug(SuggestionViewModel model);
    }
}