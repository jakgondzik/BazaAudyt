using System.ComponentModel.DataAnnotations;

namespace BazaAudyt.Models
{
    public class Konto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is requried!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is requried!")]
        public string Password { get; set; }
    }
}
