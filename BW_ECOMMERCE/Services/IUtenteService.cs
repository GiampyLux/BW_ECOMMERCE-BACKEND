using BW_ECOMMERCE.Models;

namespace BW_ECOMMERCE.Services
{
    public interface IUtenteService
    {
        IEnumerable<Utente> GetUtenti();
        Utente GetUtenteById(int id);
        void InsertUtente(Utente utente);
        void UpdateUtente(Utente utente);
        void DeleteUtente(int id);
    }
}
