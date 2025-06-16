namespace BazaAudyt.Models.ViewModels
{
    public class PytanieZwynikiemViewModel
    {
        public int PytanieId { get; set; }
        public string? PytanieTresc { get; set; }

        public int? WynikId { get; set; }
        public string? Wynik { get; set; }
        public string? Komentarz { get; set; }
        public int? Wartosc { get; set; }
        public string? Uwagi { get; set; }

        public int AudytId { get; set; }
    }


}
