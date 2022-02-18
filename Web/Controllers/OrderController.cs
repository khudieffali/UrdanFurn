using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly UserManager<User> _userManager;
        private readonly OrderManager _orderManager;

        public OrderController(ProductManager productManager, UserManager<User> userManager, OrderManager orderManager)
        {
            _productManager = productManager;
            _userManager = userManager;
            _orderManager = orderManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            var ProductIdList = Request.Cookies["cartProduct"];
            List<Product> ProductList = null;
            CheckOutVm vm = new();
            if(ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds=ProductIdList.Split("-").Select(x=>int.Parse(x)).ToList();
                ProductList = _productManager.GetByIds(ProductIds.Distinct());
                var selectedUser=await _userManager.GetUserAsync(User);
                if(selectedUser != null)
                {
                    vm.ProductIds = ProductIds;
                    vm.ProductList = ProductList;
                    vm.CustomerFirstName = selectedUser.FirstName;
                    vm.CustomerLastName = selectedUser.LastName;
                    vm.CustomerEmail = selectedUser.Email;
                    vm.CustomerAddress = selectedUser.Address;
                    vm.CustomerPhone = selectedUser.Address;
                }
                else
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                return View(vm);
              
                
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutVm checkout )
        {
            var ProductIdList = Request.Cookies["cartProduct"];
            List<Product> ProductList=null;
            CheckOutVm vm = new();
            Order newOrder = new();
            if( ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds = ProductIdList.Split("-").Select(x=>int.Parse(x)).ToList();
                ProductList=_productManager.GetByIds(ProductIds.Distinct());
                var selectedUser=await _userManager.GetUserAsync(User);
                vm.ProductList=ProductList;
                vm.ProductIds=ProductIds;
                if (selectedUser != null)
                {
                    newOrder.CustomerID = selectedUser.Id;
                    newOrder.CustomerFirstName = selectedUser.FirstName;
                    newOrder.CustomerLastName=selectedUser.LastName;
                    newOrder.CustomerAddress= checkout.CustomerAddress;
                    newOrder.CustomerEmail= selectedUser.Email;
                    newOrder.CustomerPhone=checkout.CustomerPhone;
                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.OrderDate = DateTime.Now;
                    newOrder.OrderItems = new List<OrderItem>();
                    newOrder.OrderItems.AddRange(ProductList.Select(x =>
                    new OrderItem()
                    {
                        ProductId=x.Id,
                        ItemPrice=x.Price,
                        Quantity=(ushort)ProductIds.Where(p=>p==x.Id).Count(),
                        OrderId=newOrder.Id,
                    }
                    ));
                    newOrder.TotalAmount = newOrder.OrderItems.Select(c => c.ItemPrice * c.Quantity).Sum();


                }
                _orderManager.AddOrder(newOrder);
                Response.Cookies.Delete("cartProduct");
            }
            return View("Index");
        }
    }
}
