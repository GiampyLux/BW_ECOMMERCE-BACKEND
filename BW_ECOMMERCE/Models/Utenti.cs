using System.ComponentModel.DataAnnotations;

namespace BW_ECOMMERCE.Models
{
    public class Utente
    {

        [Required]
        public int IdUtente { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? Cognome { get; set; }
        
        [Required]
        [StringLength(20)]
        public string? Password { get; set; }
        public string? Img { get; set; }

        [Required]
        [StringLength(10)]
        public string? Username { get; set; } //lunghezza stringa 

        [Required]
        public DateTime DataNascita { get; set; }// controllare sempre formato data
        
        [Required]
        [StringLength(30)]
        public string? Email { get; set; } // inserire controllo formato email
        
        [Required]
        [StringLength(50)]
        public string? Indirizzo { get; set; } // lunghezza stringa
    }
}
