﻿using bacit_dotnet.MVC.Entities;
using bacit_dotnet.MVC.Models.Suggestions;

namespace bacit_dotnet.MVC.DataAccess
{
    public interface ISqlConnector
    {
        IEnumerable<User> GetUsers();
        void SetSug(SuggestionViewModel model);
    }
}