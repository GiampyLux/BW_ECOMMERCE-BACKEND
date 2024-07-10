using BW_ECOMMERCE.Models;
using BW_ECOMMERCE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            _fileService = fileService; // Inietta il servizio per gestire i file
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
