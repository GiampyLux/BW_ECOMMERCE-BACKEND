using BW_ECOMMERCE.Models;
using BW_ECOMMERCE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BW_ECOMMERCE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdottoService _prodottoService;
        private readonly IUtenteService _utenteService;
        private readonly ICarrelloService _carrelloService;
        private readonly IFileService _fileService;

        public HomeController(ILogger<HomeController> logger, IProdottoService prodottoService, IUtenteService utenteService, ICarrelloService carrelloService, IFileService fileService)
        {
            _logger = logger;
            _prodottoService = prodottoService;
            _utenteService = utenteService;
            _carrelloService = carrelloService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View(_prodottoService.GetProdotti().OrderByDescending(a => a.DataIns));
        }

        public IActionResult Dettagli(int id)
        {
            var prodotto = _prodottoService.GetProdottoById(id);
            return View(prodotto);
        }

        public IActionResult Crea()
        {
            return View(new Prodotto());
        }

        [HttpPost]
        public IActionResult Crea(Prodotto prodotto, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("ImgProdotto", "È necessario caricare un'immagine.");
                return View(prodotto);
            }

            try
            {
                var base64String = _fileService.ConvertToBase64(file);
                prodotto.ImgProdotto = base64String;

                _prodottoService.InsertProdotto(prodotto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la creazione del prodotto: {ex.Message}");
                ModelState.AddModelError("", "Si è verificato un errore durante la creazione del prodotto.");
                return View(prodotto);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _prodottoService.DeleteProdotto(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Carrello()
        {
            var carrelli = _carrelloService.GetCarrelli().Where(c => c.Presente);
            return View(carrelli);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            int userId = 1; // Replace with actual user ID from your user management logic

            Carrello carrello = new Carrello
            {
                IdUtenteFK = userId,
                IdProdottoFK = productId,
                Confermato = false,
                Presente = true,
                Qnty = quantity
            };

            _carrelloService.InsertCarrello(carrello);

            TempData["Success"] = "Prodotto aggiunto al carrello con successo";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RimuoviDalCarrello(int id)
        {
            _carrelloService.DeleteCarrello(id);
            return RedirectToAction(nameof(Carrello));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
