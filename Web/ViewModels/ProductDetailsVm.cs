using Entities;

namespace Web.ViewModels
{
    public class ProductDetailsVm
    {
        public Product? Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}
