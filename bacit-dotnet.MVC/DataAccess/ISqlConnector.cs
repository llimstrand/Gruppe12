﻿using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;
using bacit_dotnet.MVC.Models.Users;
using bacit_dotnet.MVC.Models.Teams;


namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<Team> FetchTeam();
        void SetTeams(TeamsViewModel model);
        void SetUsers(UsersViewModel model);
        IEnumerable<User> FetchEmp();
        IEnumerable<Suggestion> FetchSug();
        IEnumerable<Suggestion> SaveSug(int id);
        void SetSug(SuggestionViewModel model);
         void SetUpSug(SuggestionViewModel model);
        IEnumerable<Suggestion> UpdateSug(int id);
        void DeleteSug(int id);

    }
}