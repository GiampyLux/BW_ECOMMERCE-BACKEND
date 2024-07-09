using BW_ECOMMERCE.Models;
using System.Collections.Generic;

namespace BW_ECOMMERCE.Services
{
    public interface IProdottoService
    {
        IEnumerable<Prodotto> GetProdotti();
        Prodotto GetProdottoById(int id);
        void InsertProdotto(Prodotto prodotto);
        void UpdateProdotto(Prodotto prodotto);
        void DeleteProdotto(int id);
    }
}