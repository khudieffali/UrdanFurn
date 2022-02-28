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
                RelatedProducts=_productManager.RelatedProducts(product.CategoryId,id.Value),
            };
            return View(vm);
        }
        public IActionResult Search(string? s, int? categoryId)
        {
            ProductSearchVm vm = new()
            {
                Products = _productManager.SearchProduct(s, categoryId),
            };
            return View(vm);
        }
    }
}
