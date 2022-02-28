using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;

        public ShopController(ProductManager productManager, CategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }


        public IActionResult Index(string? s,int? id)
        {
            ShopVm vm = new()
            {
                Products = _productManager.GetProducts(),
                SearchProduct = _productManager.SearchProduct(s, id),
                Categories = _categoryManager.GetCategories(),
            };

            return View(vm);
        }
    }
}
