namespace bacit_dotnet.MVC.Entities
{
    public class User
    {
        public int Emp_Nr { get; set; }
        public string Emp_Navn { get; set; }
        public string Emp_Passord { get; set; }
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

        public DateTime Sug_Timestamp {get; set;}
    }
    public class Team
    {
        public int Team_ID {get; set;}
       public string Team_Navn { get; set; }
        public string Team_Leder { get; set; }
    }

     class Create
{
    public enum Filter { Plan, Do, Study, Act }
    public string Name { get; set; }
    public int Age { get; set; }
    public Job Porm { get; set; }

    public Create(string name, int age, Job porm)
    {
        Name = name;
        Age = age;
        Porm = porm;
    }

    public override string ToString()
    {
        return string.Format("Name: {0} - Age {1} - {2}", Name, Age, Porm);
    }
}
}
