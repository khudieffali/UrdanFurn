using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Order:BaseEntity
    {
        public string CustomerID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerFirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerLastName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string CustomerAddress { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string CustomerPhone { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string CustomerEmail { get; set; }
        [MaxLength(700)]
        public string OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }

    }
}
