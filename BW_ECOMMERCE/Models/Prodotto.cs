using System.ComponentModel.DataAnnotations;

namespace BW_ECOMMERCE.Models
{
    public class Prodotto
    {

        [Required]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeProdotto { get; set; } // lunghezza stringa

        [Required]
        public decimal Prezzo { get; set; }

        [Required]
        [StringLength(50)]
        public string Desc { get; set; } // lunghezza stinga

        [Required]
        public string Descrizione { get; set; } // non necessario required

        public string ImgProdotto { get; set; } // capire se vogliamo fare il base64 o inserire direttamente url
        [Required]
        public DateTime DataIns { get; set; }// controllare formato della data 

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CodProd { get; set; }
    }
}
