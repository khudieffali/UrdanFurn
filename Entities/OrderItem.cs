namespace Entities
{
    public class OrderItem:BaseEntity
    {
        public decimal ItemPrice { get; set; }
        public ushort Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int  OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
