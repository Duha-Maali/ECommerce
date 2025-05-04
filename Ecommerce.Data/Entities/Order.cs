using Ecommerce.Data.Enums;

namespace Ecommerce.Data.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public ShoppingCart Cart { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Payment PaymentMethod { get; set; }
    }
}
