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

        public HomeController(ILogger<HomeController> logger, IProdottoService prodottoService, IUtenteService utenteService, ICarrelloService carrelloService)
        {
            _logger = logger;
            _utenteService = utenteService;
            _prodottoService = prodottoService;
            _carrelloService = carrelloService;
        }

        public IActionResult Index()
        {
            return View(_prodottoService.GetProdotti().OrderByDescending(a => a.DataIns));
        }

        public IActionResult Details(int id)
        {
            var prodotto = _prodottoService.GetProdottoById(id);
            return View(prodotto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
