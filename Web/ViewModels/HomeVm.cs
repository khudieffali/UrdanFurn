using Entities;
using Web.Helpers;

namespace Web.ViewModels
{
    public class HomeVm
    {
        public List<Product> Sliders { get; set; }
        public List<Product> ProductList { get; set; }
        public List<Blog> BlogList { get; set; }
    }
}
