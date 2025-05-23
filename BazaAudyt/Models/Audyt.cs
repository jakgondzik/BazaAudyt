namespace BazaAudyt.Models
{
    public class Audyt
    {
        public int Id { get; set; }
        public string Audytor { get; set; }
        public string Towarzyszacy { get; set; }
        public DateTime Data { get; set; }
        public string Stanowisko { get; set; }
        public DateTime DataPlanowana { get; set; }
        public string ObszarAudytu { get; set; }
        public DateTime DataZamkniecia {  get; set; }
        public string Pozycja { get; set; }
        public string Lider {  get; set; }
        public string Wydzial {  get; set; }
        public int Brygada { get; set; } //można zmienić na byte
        public string Audytowany { get; set; }
        public string Komentarz {  get; set; }

    }
}
