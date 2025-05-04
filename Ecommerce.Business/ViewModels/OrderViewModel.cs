using Ecommerce.Data.Enums;

namespace Ecommerce.Business.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public ShoppingCartViewModel Cart { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentViewModel PaymentMethod { get; set; }
        public OrderViewModel()
        {
            Cart = new ShoppingCartViewModel();
            OrderDate = DateTime.Now; // Set to current date and time
            Status = OrderStatus.Pending; // Default to Pending
            PaymentMethod = new PaymentViewModel();
        }
    }
}
