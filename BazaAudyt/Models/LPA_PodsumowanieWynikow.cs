namespace BazaAudyt.Models
{
    public class LPA_PodsumowanieWynikow
    {
        public int Id { get; set; }
        public int? IdCzesci { get; set; }
        public DateTime? DataWykonania { get; set; }
        public int? IdAudytowanego { get; set; }
        public int? IdAudytu {  get; set; }
        public string? Komentarz {  get; set; }
        public string? Lider {  get; set; }
        public string? Audytowany { get; set; }
        public bool? Rozpoczety {  get; set; }
    }
}
