namespace BazaAudyt.Models
{
    public class LPA_Pytania
    {
        public int Id { get; set; }
        public string? Pytanie { get; set; }
        public string Obszar { get; set; }
        public int? Nr {  get; set; }
        public bool Aktywne { get; set; }
        public string? Norma { get; set; }
        public int? Waga { get; set; }
    }
}
