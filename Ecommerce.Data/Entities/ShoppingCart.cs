
namespace Ecommerce.Data.Entities
{
    public class ShoppingCart
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public List<Product> Products { get; set; }
    }
}
