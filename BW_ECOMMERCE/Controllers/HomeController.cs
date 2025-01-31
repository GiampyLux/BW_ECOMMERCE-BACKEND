using BW_ECOMMERCE.Models;
using BW_ECOMMERCE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                ModelState.AddModelError("ImgProdotto", "� necessario caricare un'immagine.");
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
                ModelState.AddModelError("", "Si � verificato un errore durante la creazione del prodotto.");
                return View(prodotto);
            }
        }

        public IActionResult Modifica(int id)
        {
            var prodotto = _prodottoService.GetProdottoById(id);

            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        [HttpPost]
        public IActionResult Modifica(Prodotto prodotto, IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var base64String = _fileService.ConvertToBase64(file);
                    prodotto.ImgProdotto = base64String;
                }

                _prodottoService.UpdateProdotto(prodotto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la modifica del prodotto: {ex.Message}");
                ModelState.AddModelError("", "Si � verificato un errore durante la modifica del prodotto.");
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
            var carrelli = _carrelloService.GetCarrelloView();
            return View(carrelli);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            int userId = 30; // Replace with actual user ID from your user management logic

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
            //return Json(new { success = true, message = "Prodotto aggiunto al carrello con successo" });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RimuoviDalCarrello(int id)
        {
            _carrelloService.ToglidaCarrello(id);
            return RedirectToAction(nameof(Carrello));
        }

        [HttpPost]

        public IActionResult CompraSingolo(int id)
        {
            _carrelloService.CompraCarrello(id);
            return RedirectToAction(nameof(Carrello));
        }

        [HttpPost]

        public IActionResult CompraCarrello(int[] ids)//, IFormCollection form)
        {
            //string[] _ids = form["ids"];
            foreach (var id in ids)
            {
                _carrelloService.CompraCarrello(id);

            }
            return RedirectToAction(nameof(Carrello));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
