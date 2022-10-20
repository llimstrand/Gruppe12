using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Suggestions
{
    public class SuggestionViewModel
    {

        public string? Sug_ID{get ; set;}
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
            this.Sug_Timestamp = DateTime.Now;
        }
        
    }

}
