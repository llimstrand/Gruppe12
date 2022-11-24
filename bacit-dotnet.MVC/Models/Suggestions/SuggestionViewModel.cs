using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Suggestions //importeringslinje
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
        public Object Vedlegg_En { get; set; }
        public Object Vedlegg_To { get; set; }

        public DateTime Sug_Timestamp {get; set;}

        /*lager et randomt ID nummer og oppretter data*/
        public SuggestionViewModel(){
            Random random = new Random();
            int randomNumber = random.Next();
            this.Sug_ID = randomNumber;
            this.Sug_Timestamp = DateTime.Now;
        }
        
    }

}
