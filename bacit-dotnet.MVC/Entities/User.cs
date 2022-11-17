using System.ComponentModel.DataAnnotations.Schema;

namespace bacit_dotnet.MVC.Entities
{
    public class User
    {
        public int Emp_Nr { get; set;}
        public int Executor_Nr { get; set; }
        public string Emp_Navn { get; set; }
        public string? Emp_ExNavn { get; set; }
        public string? Emp_PrNavn { get; set; }
        public string Emp_Passord { get; set; }
        public int Antall_Forslag { get; set; }
        public int Antall_Pr_Forslag { get; set; }


    }
   [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Team { get; set; }
        public string? Role { get; set; }
    }
    public class Proposer{
        public int Emp_Nr { get; set;}
        public string? Emp_PrNavn { get; set; }
    }

     public class Suggestion
    {
        public int Sug_ID {get; set;}
       public string Sug_Overskrift { get; set; }
        public string Sug_Beskrivelse { get; set; }
        public string Sug_Ansvarlig { get; set; }
        public string Sug_Status { get; set; }
        public string Sug_Frist { get; set; }
        public string Sug_Varighet { get; set; }
        public byte[] Sug_VedleggEn { get; set; }
        public byte[] Sug_VedleggTo { get; set; }

        public string Sug_Timestamp {get; set;}
    }
    public class Team
    {
        public int Team_ID {get; set;}
        public string Team_Navn { get; set; }
        public string Team_Leder { get; set; }
        public int Emp_Nr { get; set;}
        public int Executor_Nr { get; set; }
        public int Antall_Forslag { get; set; }
        public int Antall_Pr_Forslag { get; set; }
        public string Emp_Navn { get; set; }
        public int Antall_Medlemmer { get; set; }
        
     
    }

}
