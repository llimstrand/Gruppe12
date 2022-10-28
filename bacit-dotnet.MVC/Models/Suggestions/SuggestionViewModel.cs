using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Suggestions
{
    public class SuggestionViewModel
    {

        public Int32 Sug_ID{get ; set;}
        public string? Emp_Nr{get; set;}
        public string? Executor_Nr{get; set;}
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public string Sug_Overskrift { get; set; }

        public string Sug_Beskrivelse { get; set; }
        public string Sug_Ansvarlig { get; set; }
        public string Sug_Status { get; set; }
        public string Sug_Frist { get; set; }
        public string Sug_Varighet { get; set; }
        public byte[] Sug_VedleggEn { get; set; }
        public byte[] Sug_VedleggTo { get; set; }

        public DateTime Sug_Timestamp {get; set;}

        public SuggestionViewModel(){
            Random random = new Random();
            int randomNumber = random.Next();
            this.Sug_ID = randomNumber;
            this.Sug_Timestamp = DateTime.Now;
        }
        
    }

}
