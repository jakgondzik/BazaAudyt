namespace BazaAudyt.Models
{
    public class LPA_PodsumowanieWyników
    {
        public int Id { get; set; }
        public int IdCzęści { get; set; }
        public DateTime DataWykonania { get; set; }
        public int IdAudytowanego { get; set; }
        public int IdAudytu {  get; set; }
        public string Komentarz {  get; set; }
        public string Lider {  get; set; }
        public string Audytowany { get; set; }
        public bool Rozpoczęty {  get; set; }
    }
}
