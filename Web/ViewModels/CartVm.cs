using Entities;

namespace Web.ViewModels
{
    public class CartVm
    {
        public List<Product> ProductList { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
