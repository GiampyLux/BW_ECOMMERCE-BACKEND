using System.ComponentModel.DataAnnotations;

namespace BW_ECOMMERCE.Models
{
    public class Carrello
    {

        [Required]
        public int IdCarrello { get; set; }

        [Required]
        public int IdUtenteFK { get; set; }

        [Required]
        public int IdProdottoFK { get; set; }

        [Required]
        public bool Confermato { get; set; }

        [Required]
        public bool Presente {  get; set; }

        [Required]
        public int Qnty { get; set; }
        // queste entitÃ ci permetto di accedere direttamente agli oggetti utente e prodotto associati senza usare nuove query

        [Required]
        public Utente Utente { get; set; }

        [Required]
        public Prodotto Prodotto { get; set; }

    }
}
