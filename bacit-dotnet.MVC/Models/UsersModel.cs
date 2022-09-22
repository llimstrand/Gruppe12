using bacit_dotnet.MVC.Entities;

namespace bacit_dotnet.MVC.Models
{
    public class UsersModel
    {
       // public IEnumerable<User> Users { get; set; }
        public string Name {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}
        public string EmployeeNumber {get; set;}
        public string Team {get; set;}
        public string SelectRole {get; set;}
        public List<string> AvailableRoles {get; set;}
        public string ValidationErrorMessage {get; set;}

    }
}
