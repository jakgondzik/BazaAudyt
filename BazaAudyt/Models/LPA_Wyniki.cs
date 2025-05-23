namespace BazaAudyt.Models
{
    public class LPA_Wyniki
    {
        public int Id { get; set; }
        public string Pytanie { get; set; }
        public string Wynik { get; set; }
        public int IdAudytu { get; set; }
        public string Komentarz { get; set; }
        public int Wartość { get; set; }
        public string Uwagi { get; set; }
    }
}
