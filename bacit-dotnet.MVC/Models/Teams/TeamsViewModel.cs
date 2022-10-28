using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Teams
{
    public class TeamsViewModel
    {
        public int Team_ID {get ; set;}
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public string Team_Navn { get; set; }
        public string Team_Leder { get; set; }
        public string Emp_Nr { get; set; }

        public TeamsViewModel(){
            Random random = new Random();
            int randomNumber = random.Next();
            this.Team_ID = randomNumber;
        }
    }

}