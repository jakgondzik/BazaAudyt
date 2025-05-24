namespace BazaAudyt.Models
{
    public class StanowiskoPracy
    {
        public int Id { get; set; }
        public string? Wydzial { get; set; }
        public string? Proces {  get; set; }
        public string? Gniazdo {  get; set; }
        public int? NrGniazda { get; set; }
        public string? RodzajStanowiska { get; set; }
        public int? IdLidera {  get; set; }
        public string? Typ { get; set; }
        public string? ObszarLPA { get; set; }
    }
}
