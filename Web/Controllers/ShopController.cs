using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductManager _productManeger;

        public ShopController(ProductManager productManeger)
        {
            _productManeger = productManeger;
        }

        public IActionResult Index()
        {
            ShopVm vm = new() 
            {
                Products =_productManeger.GetProducts(),
            };

            return View(vm);
        }
    }
}
