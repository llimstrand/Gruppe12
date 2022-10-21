using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Users
{
    public class UsersViewModel
    {

        
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public int Emp_Nr{get ; set;}
        public string Emp_Navn { get; set; }
        public string Emp_Passord { get; set; }
    }

}