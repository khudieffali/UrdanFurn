using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;


        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Details(int? id)
        {

           if(id == null) return NotFound();
            var product = _productManager.GetById(id.Value);
            if(product == null)return NotFound();
            ProductDetailsVm vm = new()
            {
                Product = product,
              
            };
            return View(vm);
        }
    }
}
