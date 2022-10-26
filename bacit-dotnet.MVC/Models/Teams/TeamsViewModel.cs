using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Teams
{
    public class TeamsViewModel
    {
        public string? Team_ID {get ; set;}
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public string Team_Navn { get; set; }
        public string Team_Leder { get; set; }
    }

}