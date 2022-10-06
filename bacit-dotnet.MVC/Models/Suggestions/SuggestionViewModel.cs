using System.ComponentModel.DataAnnotations;

namespace bacit_dotnet.MVC.Models.Suggestions
{
    public class SuggestionViewModel
    {
        [Required]
        [MinLength(7, ErrorMessage ="Skriv en ordentlig tittel!")]
        public string For_Overskrift { get; set; }
        public string For_Beskrivelse { get; set; }
        public string For_Ansvarlig { get; set; }
        public string For_Status { get; set; }
        public string For_Frist { get; set; }
        public string For_Varighet { get; set; }
        public byte[] For_VedleggEn { get; set; }
        public byte[] For_VedleggTo { get; set; }

        public DateTime For_Timestamp {get; set;}

        public SuggestionViewModel(){
            this.For_Timestamp = DateTime.Now;
        }
        
    }

}
