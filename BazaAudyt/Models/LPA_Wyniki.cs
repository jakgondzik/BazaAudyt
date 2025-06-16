namespace BazaAudyt.Models
{
    public class LPA_Wyniki
    {
        public int Id { get; set; }
        public int? Pytanie { get; set; }
        public string? Wynik { get; set; }
        public int? IdAudytu { get; set; }
        public string? Komentarz { get; set; }
        public int? Wartosc { get; set; }
        public string? Uwagi { get; set; }

        public LPA_Pytania? PytanieNavigation { get; set; }
    }
}
