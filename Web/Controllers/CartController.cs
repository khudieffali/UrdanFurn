using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductManager _productManager;

        public CartController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            var ProductIdList = Request.Cookies["cartProduct"];
            List<Product> productList = null;
            CartVm vm = new();
            if(ProductIdList!=null && ProductIdList != "")
            {
                List<int> ProductIds= ProductIdList.Split("-").Select(x=> int.Parse(x)).ToList();
                productList = _productManager.GetByIds(ProductIds.Distinct());
                vm.ProductIds = ProductIds;
                vm.ProductList = productList;

            }
            return View(vm);
        }
    }
}
