using Entities;

namespace Web.ViewModels
{
    public class ShopVm
    {
        public List<Product> Products { get; set; }
        public List<Product> SearchProduct { get; set; }
        public List<Category> Categories { get; set; }
    }
}
