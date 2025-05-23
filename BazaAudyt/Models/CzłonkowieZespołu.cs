namespace BazaAudyt.Models
{
    public class CzłonkowieZespołu
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Inicjaly { get; set; }
        public string Telefon { get; set; }
        public bool CzyAdmin { get; set; }
        public string Warstwa { get; set; }
        public bool CzyAudytor { get; set; }
    }
}
