using BW_ECOMMERCE.Models;

namespace BW_ECOMMERCE.Services
{
    public interface ICarrelloService
    {
        IEnumerable<Carrello> GetCarrelli();
        Carrello GetCarrelloById(int id);
        void InsertCarrello(Carrello carrello);
        void UpdateCarrello(Carrello carrello);
        void DeleteCarrello(int id);
    }
}