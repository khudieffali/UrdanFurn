using Microsoft.AspNetCore.Mvc;
using Services;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductManager _productManager;
        private readonly BlogManager _blogManager;

        public HomeController(ILogger<HomeController> logger, ProductManager productManager, BlogManager blogManager)
        {
            _logger = logger;
            _productManager = productManager;
            _blogManager = blogManager;
        }

        public IActionResult Index()
        {

            HomeVm vm = new()
            {
                ProductList = _productManager.GetProducts(),
                Sliders = _productManager.GetSliders(),
                BlogList=_blogManager.GetBlogs(),
                FeaturedProducts=_productManager.FetauredProducts(),
            };
           
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}