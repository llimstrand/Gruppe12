using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Users
{
    public class UsersViewModel
    {
        public int Emp_Nr {get ; set;}
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public string Emp_Navn { get; set; }
        public string Emp_Passord { get; set; }
        public string? Executor_Nr{get; set;}
        public int Antall_Forslag { get; set; }

    }

}