using Entities;

namespace Web.ViewModels
{
    public class CheckOutVm
    {
        public List<Product> ProductList { get; set; }
        public List<int> ProductIds { get; set; }
        public string CustomerID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
