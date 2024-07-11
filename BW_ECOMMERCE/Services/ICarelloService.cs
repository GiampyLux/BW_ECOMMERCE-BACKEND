using BW_ECOMMERCE.Models;

namespace BW_ECOMMERCE.Services
{
    public interface ICarrelloService
    {
        IEnumerable<Carrello> GetCarrelli();
        IEnumerable<CarrelloView> GetCarrelloView();
        CarrelloView GetCarrelloById(int id);
        void InsertCarrello(Carrello carrello);
        void UpdateCarrello(Carrello carrello);
        void DeleteCarrello(int id);
        void CompraCarrello(int Id);
        void ToglidaCarrello(int id);


    }
}